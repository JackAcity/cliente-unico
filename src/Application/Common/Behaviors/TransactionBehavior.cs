using Application.Common.Exceptions;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(
            IUnitOfWork unitOfWork,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct)
        {
            await _unitOfWork.BeginTransactionAsync(ct);

            try
            {
                var response = await next();

                if (response.IsError)
                {
                    _logger.LogWarning(
                        "Rollback por errores en {Request}: {@Errors}",
                        typeof(TRequest).Name,
                        response.Errors);
                    await _unitOfWork.RollbackTransactionAsync(ct);
                    return response;
                }

                await _unitOfWork.CommitTransactionAsync(ct);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Rollback por excepción en {Request}",
                    typeof(TRequest).Name);

                await _unitOfWork.RollbackTransactionAsync(ct);

                if (ex is DomainErrorException ie)
                    return (TResponse)(dynamic)ie.Errors;

                var error = Error.Failure(
                    "Transaction.Failure",
                    ex.Message);
                return (TResponse)(dynamic)error;
            }
        }
    }
}

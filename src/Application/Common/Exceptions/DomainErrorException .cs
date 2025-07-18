using ErrorOr;

namespace Application.Common.Exceptions
{
    public class DomainErrorException : Exception
    {
        public List<Error> Errors { get; }

        public DomainErrorException(Error error)
              : this(new List<Error> { error }) { }

        public DomainErrorException(IEnumerable<Error> errors)
        {
            Errors = errors.ToList();
        }
    }

}

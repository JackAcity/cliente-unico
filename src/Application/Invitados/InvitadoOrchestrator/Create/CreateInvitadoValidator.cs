using Application.Invitados.Correos.Create;
using Application.Invitados.DatosPincipales.Create;
using Application.Invitados.Origen.OrigenAdicionales;
using Application.Invitados.Telefono;
using FluentValidation;

namespace Application.Invitados.InvitadoOrchestrator.Create;

public class CreateInvitadoOrchestratorCommandValidator : AbstractValidator<CreateInvitadoOrchestratorCommand>
{
    public CreateInvitadoOrchestratorCommandValidator()
    {
        RuleFor(x => x.Principal)
            .NotNull().WithMessage("Principal is required.")
            .SetValidator(new CreateInvitadoPrincipalCommandValidator());

        RuleForEach(x => x.Telefonos)
            .SetValidator(new CreateTelefonoCommandValidator());

        RuleForEach(x => x.Correos)
            .SetValidator(new CreateCorreoCommandValidator());

        RuleForEach(x => x.Documentos)
            .SetValidator(new CreateDocumentoCommandValidator());

        RuleForEach(x => x.Preferencias)
            .SetValidator(new CreatePreferenciaCommandValidator());

        RuleForEach(x => x.Direcciones)
            .SetValidator(new CreateDireccionCommandValidator());

        RuleFor(x => x.EtiquetasIds)
            .Must(list => list == null || list.All(id => id > 0))
            .WithMessage("EtiquetasIds must contain only positive values.");

        RuleFor(x => x.Notas)
            .Must(list => list == null || list.All(nota => !string.IsNullOrWhiteSpace(nota)))
            .WithMessage("Notas cannot contain empty strings.");
    }
}

public class CreateDocumentoCommandValidator : AbstractValidator<CreateDocumentoDto>
{
    public CreateDocumentoCommandValidator()
    {
        RuleFor(x => x.TipoDocumentoCodigo)
            .NotEmpty().WithMessage("TipoDocumentoCodigo is required.");

        RuleFor(x => x.DocumentoIdentidad)
            .NotEmpty().WithMessage("DocumentoIdentidad is required.")
            .MaximumLength(20).WithMessage("DocumentoIdentidad must not exceed 20 characters.");
    }
}

public class CreatePreferenciaCommandValidator : AbstractValidator<CreatePreferenciaDto>
{
    public CreatePreferenciaCommandValidator()
    {
        RuleFor(x => x.CategoriaId)
            .GreaterThan(0).WithMessage("CategoriaId must be greater than zero.");

        RuleFor(x => x.Valor)
            .NotEmpty().WithMessage("Valor is required.")
            .MaximumLength(100).WithMessage("Valor must not exceed 100 characters.");
    }
}
public class CreateOrigenAdicionalCommandValidator : AbstractValidator<CreateOrigenAdicionalCommand>
{
    public CreateOrigenAdicionalCommandValidator()
    {
        RuleFor(x => x.CampoPersonalizadoId)
            .GreaterThan(0).WithMessage("CampoPersonalizadoId must be greater than zero.");

        RuleFor(x => x.Valor)
            .NotEmpty().WithMessage("Valor is required.")
            .MaximumLength(100).WithMessage("Valor must not exceed 100 characters.");
    }
}

public class CreateDireccionCommandValidator : AbstractValidator<CreateDireccionDto>
{
    public CreateDireccionCommandValidator()
    {
        RuleFor(x => x.TipoContactoCodigo)
            .NotEmpty().WithMessage("TipoContactoCodigo is required.");

        RuleFor(x => x.Direccion)
            .NotEmpty().WithMessage("Direccion is required.")
            .MaximumLength(200).WithMessage("Direccion must not exceed 200 characters.");

        RuleFor(x => x.Referencia)
            .MaximumLength(200).WithMessage("Referencia must not exceed 200 characters.");

        RuleFor(x => x.Piso)
            .GreaterThanOrEqualTo(0).WithMessage("Piso must be 0 or greater.");

        RuleFor(x => x.Departamento)
            .GreaterThanOrEqualTo(0).WithMessage("Departamento must be 0 or greater.");
    }
}


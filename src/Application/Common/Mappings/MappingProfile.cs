using Application.Invitados.Correos.Create;
using Application.Invitados.Direccion.Create;
using Application.Invitados.Documento.Create;
using Application.Invitados.InvitadoOrchestrator.Create;
using Application.Invitados.InvitadoPincipal.Create;
using Application.Invitados.Preferencial;
using Application.Invitados.Telefono;
using AutoMapper;

namespace Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateDatosPrincipalesDto, CreateDatosPrincipalesCommand>();

        CreateMap<CreateCorreoDto, CreateCorreoCommand>();

        CreateMap<CreateTelefonoDto, CreateTelefonoCommand>();

        CreateMap<CreateDocumentoDto, CreateDocumentoCommand>();

        CreateMap<CreatePreferenciaDto, CreatePreferenciaCommand>();

        CreateMap<CreateDireccionDto, CreateDireccionCommand>();

    }
}

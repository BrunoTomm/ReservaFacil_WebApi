using AutoMapper;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Map;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Evento, EventoViewModel>().ReverseMap();
        CreateMap<Reserva, ReservaViewModel>().ReverseMap();
    }   
}

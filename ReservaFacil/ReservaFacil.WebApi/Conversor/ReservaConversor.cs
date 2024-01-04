using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.WebApi.Interface;

namespace ReservaFacil.WebApi.Conversor;

public class ReservaConversor : IReservaConversor
{
    public Reserva ConverterParaEntidade(ReservaViewModel reservaViewModel, int idReserva)
    {
        return new Reserva(idReserva, reservaViewModel.UsuarioId, reservaViewModel.EventoId, reservaViewModel.DataReserva);
    }
}

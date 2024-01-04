using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.WebApi.Interface
{
    public interface IReservaConversor
    {
        Reserva ConverterParaEntidade(ReservaViewModel reservaViewModel, int idReserva);
    }
}

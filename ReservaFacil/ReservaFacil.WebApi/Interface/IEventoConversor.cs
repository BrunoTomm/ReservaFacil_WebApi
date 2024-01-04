using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.WebApi.Interface
{
    public interface IEventoConversor
    {
        Evento ConverterParaEntidade(EventoViewModel eventoViewModel, int idEvento);
    }
}

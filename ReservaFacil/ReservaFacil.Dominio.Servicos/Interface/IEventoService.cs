using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Servicos.Interface
{
    public interface IEventoService
    {
        Task<EventoViewModel> CriarEventoAsync(Evento evento);
        Task<List<EventoViewModel>> ObterTodosEventosAsync();
        Task<EventoViewModel> ObterEventoPorIdAsync(int id);
        Task ExcluirEventoPorIdAsync(int id, Guid idUsuario);
        Task<EventoViewModel> AtualizaEventoAsync(Evento evento);

    }
}

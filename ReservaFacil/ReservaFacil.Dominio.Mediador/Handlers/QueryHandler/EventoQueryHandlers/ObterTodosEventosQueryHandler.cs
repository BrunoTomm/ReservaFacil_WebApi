using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Querys.EventoQuerys;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Handlers.QueryHandler.EventoQuerys;

public class ObterTodosEventosQueryHandler : IRequestHandler<ObterTodosEventosQuery, List<EventoViewModel>>
{
    private readonly IEventoService _eventoService;

    public ObterTodosEventosQueryHandler(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    public Task<List<EventoViewModel>> Handle(ObterTodosEventosQuery request, CancellationToken cancellationToken)
    {
        return _eventoService.ObterTodosEventosAsync();
    }
}

using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Querys.EventoQuerys;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Handlers.QueryHandler.EventoQuerys;

public class ObterEventoPorIdQueryHandler : IRequestHandler<ObterEventoPorIdQuery, EventoViewModel>
{
    private readonly IEventoService _eventoService;

    public ObterEventoPorIdQueryHandler(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    public Task<EventoViewModel> Handle(ObterEventoPorIdQuery request, CancellationToken cancellationToken)
    {
        return _eventoService.ObterEventoPorIdAsync(request.IdEvento);
    }
}

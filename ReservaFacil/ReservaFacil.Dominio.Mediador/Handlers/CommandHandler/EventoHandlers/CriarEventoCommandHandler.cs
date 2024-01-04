using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Handlers.CommandHandler.EventoHandlers;

public class CriarEventoCommandHandler : IRequestHandler<CriarEventoCommand, EventoViewModel>
{
    private readonly IEventoService _eventoService;

    public CriarEventoCommandHandler(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    public async Task<EventoViewModel> Handle(CriarEventoCommand request, CancellationToken cancellationToken)
    {
        var eventoCriado = await _eventoService.CriarEventoAsync(request.evento);

        return eventoCriado;
    }
}

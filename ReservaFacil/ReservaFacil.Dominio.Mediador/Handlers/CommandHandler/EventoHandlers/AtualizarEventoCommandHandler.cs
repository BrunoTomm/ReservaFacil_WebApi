using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Handlers.CommandHandler.EventoHandlers;

public class AtualizarEventoCommandHandler : IRequestHandler<AtualizarEventoCommand, EventoViewModel>
{
    private readonly IEventoService _eventoService;

    public AtualizarEventoCommandHandler(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    public async Task<EventoViewModel> Handle(AtualizarEventoCommand request, CancellationToken cancellationToken)
    {
        var eventoAtualizado = await _eventoService.AtualizaEventoAsync(request.evento);

        return eventoAtualizado;
    }
}

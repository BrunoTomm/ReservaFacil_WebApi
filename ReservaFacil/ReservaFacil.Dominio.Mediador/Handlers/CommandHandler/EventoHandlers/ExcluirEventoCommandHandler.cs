using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Handlers.CommandHandler.EventoHandlers;

public class ExcluirEventoCommandHandler : IRequestHandler<ExcluirEventoCommand>
{
    private readonly IEventoService _eventoService;

    public ExcluirEventoCommandHandler(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    public async Task<Unit> Handle(ExcluirEventoCommand request, CancellationToken cancellationToken)
    {
        await _eventoService.ExcluirEventoPorIdAsync(request.IdEvento, request.IdUsuario);

        return Unit.Value;
    }
}

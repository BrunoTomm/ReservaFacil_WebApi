using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Servicos.Interface;

namespace ReservaFacil.Dominio.Mediador.Handlers.CommandHandler.EventoHandlers;

public class CancelarReservaCommandHandler : IRequestHandler<CancelarReservaCommand>
{
    private readonly IReservaService _reservaService;

    public CancelarReservaCommandHandler(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    public async Task<Unit> Handle(CancelarReservaCommand request, CancellationToken cancellationToken)
    {
        await _reservaService.CancelarReservaPorIdAsync(request.IdReserva, request.IdUsuario);

        return Unit.Value;
    }
}

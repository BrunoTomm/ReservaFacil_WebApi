using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Handlers.CommandHandler.EventoHandlers;

public class AtualizarReservaCommandHandler : IRequestHandler<AtualizarReservaCommand, ReservaViewModel>
{
    private readonly IReservaService _reservaService;

    public AtualizarReservaCommandHandler(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    public async Task<ReservaViewModel> Handle(AtualizarReservaCommand request, CancellationToken cancellationToken)
    {
        var reservaAtualizada = await _reservaService.AtualizarReservaAsync(request.Reserva);

        return reservaAtualizada;
    }
}

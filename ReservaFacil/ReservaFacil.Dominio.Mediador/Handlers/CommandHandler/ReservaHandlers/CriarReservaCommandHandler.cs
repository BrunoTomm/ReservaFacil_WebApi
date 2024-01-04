using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Handlers.CommandHandler.EventoHandlers;

public class CriarReservaCommandHandler : IRequestHandler<CriarReservaCommand, ReservaViewModel>
{
    private readonly IReservaService _reservaRepository;

    public CriarReservaCommandHandler(IReservaService reservaRepository)
    {
        _reservaRepository = reservaRepository;
    }

    public async Task<ReservaViewModel> Handle(CriarReservaCommand request, CancellationToken cancellationToken)
    {
        var reservaCriada = await _reservaRepository.CriarReservaAsync(request.Reserva);

        return reservaCriada;
    }
}

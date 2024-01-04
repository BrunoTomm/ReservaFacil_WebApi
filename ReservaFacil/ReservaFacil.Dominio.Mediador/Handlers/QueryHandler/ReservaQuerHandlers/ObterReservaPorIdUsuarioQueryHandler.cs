using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Querys.EventoQuerys;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Handlers.QueryHandler.EventoQuerys;

public class ObterReservaPorIdUsuarioQueryHandler : IRequestHandler<ObterReservaPorIdUsuarioQuery, List<ReservaViewModel>>
{
    private readonly IReservaService _reservaService;

    public ObterReservaPorIdUsuarioQueryHandler(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    public Task<List<ReservaViewModel>> Handle(ObterReservaPorIdUsuarioQuery request, CancellationToken cancellationToken)
    {
        return _reservaService.ObterReservasPorUsuarioAsync(request.IdUsuario);
    }
}

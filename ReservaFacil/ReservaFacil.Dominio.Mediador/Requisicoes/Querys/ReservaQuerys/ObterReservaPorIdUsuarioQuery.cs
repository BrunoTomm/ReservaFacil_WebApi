using MediatR;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Requisicoes.Querys.EventoQuerys
{
    public record ObterReservaPorIdUsuarioQuery(Guid IdUsuario) : IRequest<List<ReservaViewModel>>;
}

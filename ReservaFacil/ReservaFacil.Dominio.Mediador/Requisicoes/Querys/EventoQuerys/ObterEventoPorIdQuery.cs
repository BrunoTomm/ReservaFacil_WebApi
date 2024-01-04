using MediatR;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Requisicoes.Querys.EventoQuerys
{
    public record ObterEventoPorIdQuery(int IdEvento) : IRequest<EventoViewModel>;
}

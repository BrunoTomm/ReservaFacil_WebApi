using MediatR;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;

public record CriarEventoCommand(Evento evento) : IRequest<EventoViewModel>;


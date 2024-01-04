using MediatR;
using ReservaFacil.Dominio.Modelos;

namespace ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;

public record ExcluirEventoCommand(int IdEvento, Guid IdUsuario) : IRequest;


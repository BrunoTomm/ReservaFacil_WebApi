using MediatR;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaFacil.Dominio.Mediador.Requisicoes.Commands.UsuarioCommands
{
    public record AutenticarCommand(Usuario Usuario) : IRequest<Usuario>;
}

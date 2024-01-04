using MediatR;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Mediador.Requisicoes.Commands.UsuarioCommands;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Mediador.Handlers.CommandHandler.EventoHandlers;

public class AutenticarCommandHandler : IRequestHandler<AutenticarCommand, Usuario>
{
    private readonly IUsuarioService _usuarioService;

    public AutenticarCommandHandler(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task<Usuario> Handle(AutenticarCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioService.BuscaUsuarioAsync(request.Usuario);

        return usuario;
    }
}

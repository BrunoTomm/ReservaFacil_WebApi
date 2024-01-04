using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Mediador.Requisicoes.Commands.UsuarioCommands;
using ReservaFacil.Dominio.Servicos.Servicos;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.WebApi.Interface;
using System.Threading;

namespace ReservaFacil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioConversor _conversor;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public LoginController(
            IUsuarioConversor conversor,
            IMediator mediator,
            IConfiguration configuration)
        {
            _conversor = conversor;
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticar([FromBody] UsuarioViewModel usuarioViewModel, CancellationToken cancellationToken)
        {
            var entidade = _conversor.ConverterParaEntidade(usuarioViewModel);

            var comando = new AutenticarCommand(entidade);

            var usuario = await _mediator.Send(comando, cancellationToken);

            if (usuario != null)
            {
                var token = TokenService.GerarToken(usuario, _configuration);

                return new
                {
                    usuario = usuario,
                    token = token
                };
            }

            return Unauthorized();
        }
    }
}

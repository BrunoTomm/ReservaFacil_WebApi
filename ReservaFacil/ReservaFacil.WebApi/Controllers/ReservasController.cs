using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservaFacil.Dominio.Exceptions;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Mediador.Requisicoes.Querys.EventoQuerys;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.Validador;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.WebApi.Interface;

namespace ReservaFacil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaConversor _conversor;
        private readonly IMediator _mediator;

        public ReservasController(IReservaConversor conversor, IMediator mediator)
        {
            _conversor = conversor;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CriarReserva")]
        public async Task<IActionResult> CriarReservaAsync([FromBody] ReservaViewModel novaReservaVM, CancellationToken cancellationToken)
        {
            try
            {
                var entidade = _conversor.ConverterParaEntidade(novaReservaVM, 0);

                var validationResult = ValidadorReserva(entidade);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(error => new { error.PropertyName, error.ErrorMessage });

                    return BadRequest(errors); 
                }

                var comando = new CriarReservaCommand(entidade);

                var reservaCriada = await _mediator.Send(comando, cancellationToken);

                return StatusCode(201, reservaCriada);
            }
            catch (ReservaFacilException ex)
            {
                return StatusCode(400, $"Ocorreu um erro ao criar o evento: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao criar a reserva: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ObterReservasPorUsuario/{usuarioId}")]
        public async Task<IActionResult> ObterReservasPorUsuarioAsync(Guid usuarioId, CancellationToken cancellationToken)
        {
            try
            {
                var query = new ObterReservaPorIdUsuarioQuery(usuarioId);

                var reservas = await _mediator.Send(query, cancellationToken);

                if (reservas is null || reservas.Count < 1) return NotFound($"Nenhuma reserva encontrada com o respectivo idUsuario {usuarioId}");

                return Ok(reservas);
            }
            catch (ReservaFacilException ex)
            {
                return StatusCode(400, $"Ocorreu um erro ao criar o evento: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao obter as reservas do usuarioId = {usuarioId}: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarReserva/{idReserva}")]
        public async Task<IActionResult> AtualizarReservaAsync(int idReserva, [FromBody] ReservaViewModel reservaAtualizadaVM, CancellationToken cancellationToken)
        {
            try
            {
                var entidade = _conversor.ConverterParaEntidade(reservaAtualizadaVM, idReserva);

                var validationResult = ValidadorReserva(entidade);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(error => new { error.PropertyName, error.ErrorMessage });

                    return BadRequest(errors);
                }
                var comando = new AtualizarReservaCommand(entidade);

                var reservaAtualizada = await _mediator.Send(comando, cancellationToken);

                if (reservaAtualizada is null) return NotFound($"Nenhuma reserva encontrada com idReserva {idReserva}");

                return Ok(reservaAtualizada);
            }
            catch (ReservaFacilException ex)
            {
                return StatusCode(400, $"Ocorreu um erro ao criar o evento: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar a reserva com id = {idReserva}: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("CancelarReserva/{idReserva}")]
        public async Task<IActionResult> CancelarReservaAsync(int idReserva, [FromBody] IdUsuarioViewModel idUsuarioViewModel, CancellationToken cancellationToken)
        {
            try
            {
                if (idUsuarioViewModel.IdUsuario == Guid.Empty) return BadRequest("É necessário um usuário válido para cancelar uma reserva.");

                var comando = new CancelarReservaCommand(idReserva, idUsuarioViewModel.IdUsuario);

                await _mediator.Send(comando, cancellationToken);

                return NoContent();
            }
            catch (ReservaFacilException ex)
            {
                return StatusCode(400, $"Ocorreu um erro ao criar o evento: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao cancelar a reserva com id = {idReserva}: {ex.Message}");
            }
        }

        internal FluentValidation.Results.ValidationResult ValidadorReserva(Reserva reserva)
        {
            ReservaValidador validator = new ReservaValidador();

            return validator.Validate(reserva);
        }

    }
}

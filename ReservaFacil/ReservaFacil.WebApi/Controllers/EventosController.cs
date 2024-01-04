using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservaFacil.Dominio.Exceptions;
using ReservaFacil.Dominio.Mediador.Requisicoes.Comandos.EventoCommand;
using ReservaFacil.Dominio.Mediador.Requisicoes.Querys.EventoQuerys;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.WebApi.Interface;

namespace ReservaFacil.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EventosController : ControllerBase
{
    private readonly IEventoConversor _conversor;
    private readonly IMediator _mediator;

    public EventosController(IEventoConversor conversor, IMediator mediator)
    {
        _conversor = conversor;
        _mediator = mediator;
    }

    [HttpPost]
    [Route("CriarEvento")]
    public async Task<IActionResult> CriarEventoAsync([FromBody] EventoViewModel novoEvento, CancellationToken cancellationToken)
    {
        try
        {
            var entidade = _conversor.ConverterParaEntidade(novoEvento, 0);
            var comando = new CriarEventoCommand(entidade);

            var eventoCriado = await _mediator.Send(comando, cancellationToken);

            return StatusCode(201, eventoCriado);
        }
        catch (ReservaFacilException ex)
        {
            return StatusCode(400, $"Ocorreu um erro ao criar o evento: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro ao criar o evento: {ex.Message}");
        }

    }

    [HttpGet]
    [Route("ObterTodosEventos")]
    public async Task<IActionResult> ObterTodosEventosAsync()
    {
        try
        {
            var query = new ObterTodosEventosQuery();

            var eventos = await _mediator.Send(query);

            return Ok(eventos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro ao obter todos os eventos: {ex.Message}");
        }
    }


    [HttpGet]
    [Route("ObterEventoPorId/{idEvento}")]
    public async Task<IActionResult> ObterEventoPorIdAsync(int idEvento, CancellationToken cancellationToken)
    {
        try
        {
            var query = new ObterEventoPorIdQuery(idEvento);

            var evento = await _mediator.Send(query, cancellationToken);

            return Ok(evento);
        }
        catch (ReservaFacilException ex)
        {
            return StatusCode(400, $"Ocorreu um erro ao criar o evento: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro ao obter o evento com id = {idEvento}: {ex.Message}");
        }
    }

    [HttpPut]
    [Route("AtualizarEvento/{idEvento}")]
    public async Task<IActionResult> AtualizarEventoAsync(int idEvento, [FromBody] EventoViewModel eventoAtualizadoVM, CancellationToken cancellationToken)
    {
        try
        {
            var entidade = _conversor.ConverterParaEntidade(eventoAtualizadoVM, idEvento);
            var comando = new AtualizarEventoCommand(entidade);

            var eventoAtualizado = await _mediator.Send(comando, cancellationToken);

            if(eventoAtualizado is null)
            {
                return NotFound($"Evento com id = {idEvento} não encontrado");
            }

            return Ok(eventoAtualizado);
        }
        catch (ReservaFacilException ex)
        {
            return StatusCode(400, $"Ocorreu um erro ao criar o evento: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro ao atualizar o evento com id = {idEvento}: {ex.Message}");
        }
    }

    [HttpDelete]
    [Route("ExcluirEvento/{id}")]
    public async Task<IActionResult> ExcluirEventoAsync(int id,[FromBody] IdUsuarioViewModel idUsuarioViewModel, CancellationToken cancellationToken)
    {
        try
        {
            if (idUsuarioViewModel.IdUsuario == Guid.Empty) return BadRequest("Id do usuário não pode ser vazio.");

            var comando = new ExcluirEventoCommand(id, idUsuarioViewModel.IdUsuario);

            await _mediator.Send(comando, cancellationToken);

            return NoContent();
        }
        catch (ReservaFacilException ex)
        {
            return StatusCode(400, $"Ocorreu um erro ao criar o evento: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro ao excluir o evento com id = {id}: {ex.Message}");
        }
    }

}

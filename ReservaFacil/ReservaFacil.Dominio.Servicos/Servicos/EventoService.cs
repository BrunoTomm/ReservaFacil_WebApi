using AutoMapper;
using ReservaFacil.Dominio.Exceptions;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.Infra.Interface;

namespace ReservaFacil.Dominio.Servicos.Servicos;

public class EventoService : IEventoService
{
    private readonly IRepositorioBase _repositorioBase;
    private readonly IMapper _mapper;

    public EventoService(IRepositorioBase repositorioBase, IMapper mapper)
    {
        _repositorioBase = repositorioBase;
        _mapper = mapper;
    }

    public async Task<EventoViewModel> CriarEventoAsync(Evento evento)
    {
        if (evento.DataEvento < DateTime.Now)  throw new ReservaFacilException("Data do evento não pode ser menor que a data atual.");

        var usuario = await _repositorioBase.ObterPorIdAsync<Usuario>(evento.UsuarioId);

        if (usuario is null) throw new ReservaFacilException("É necessário um usuário válido para efetivar um evento.");

        var eventoPersistido = await _repositorioBase.AdicionarAsync(evento);

        var eventoVM = _mapper.Map<EventoViewModel>(eventoPersistido);

        return eventoVM;
    }

    public async Task<EventoViewModel> AtualizaEventoAsync(Evento eventoParaAtualizar)
    {
        var evento = await _repositorioBase.ObterPorIdAsync<Evento>(eventoParaAtualizar.Id);

        if (evento is null) throw new ReservaFacilException("É necessário um evento válido.");

        if (evento.UsuarioId != eventoParaAtualizar.UsuarioId) throw new ReservaFacilException("Usuário não tem permissão para atualizar este evento.");

        evento.AtualizarEvento(eventoParaAtualizar.NomeEvento,
                               eventoParaAtualizar.DataEvento,
                               eventoParaAtualizar.DescricaoEvento,
                               evento.QuantidadeReservada,
                               eventoParaAtualizar.QuantidadeLimite);

        var eventoAtualizado = await _repositorioBase.AtualizarAsync(evento);

        var eventoVM = _mapper.Map<EventoViewModel>(eventoAtualizado);

        return eventoVM;
    }

    public async Task ExcluirEventoPorIdAsync(int id, Guid idUsuario)
    {
        var evento = await _repositorioBase.ObterPorIdAsync<Evento>(id);

        if (evento is null)
            throw new ReservaFacilException($"Evento com id = {id}, não encontrado.");

        if (evento.UsuarioId != idUsuario) throw new ReservaFacilException("Usuário não tem permissão para atualizar este evento.");

        await _repositorioBase.ExcluirAsync(evento);
    }

    public async Task<EventoViewModel> ObterEventoPorIdAsync(int id)
    {
        var evento = await _repositorioBase.ObterPorIdAsync<Evento>(id);

        var eventoVM = _mapper.Map<EventoViewModel>(evento);

        return eventoVM;
    }

    public async Task<List<EventoViewModel>> ObterTodosEventosAsync()
    {
        var eventos = await _repositorioBase.ObterTodosAsync<Evento>();

        var eventosViewModel = _mapper.Map<List<EventoViewModel>>(eventos);

        return eventosViewModel;
    }
}

using AutoMapper;
using ReservaFacil.Dominio.Exceptions;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.Infra.Interface;

namespace ReservaFacil.Dominio.Servicos.Servicos
{
    public class ReservaService : IReservaService
    {
        private readonly IRepositorioBase _repositorioBase;
        private readonly IMapper _mapper;

        public ReservaService(IRepositorioBase repositorioBase, IMapper mapper)
        {
            _repositorioBase = repositorioBase;
            _mapper = mapper;
        }

        public async Task<ReservaViewModel> CriarReservaAsync(Reserva reserva)
        {

            var usuario = await _repositorioBase.ObterPorIdAsync<Usuario>(reserva.UsuarioId);
            var evento = await _repositorioBase.ObterPorIdAsync<Evento>(reserva.EventoId);

            if (usuario is null) 
                throw new ReservaFacilException("É necessário um usuário válido para efetivar uma reserva.");
            else if (evento is null) 
                throw new ReservaFacilException("É necessário um evento válido para efetivar uma reserva.");
            else if (evento.DataEvento > reserva.DataReserva)
                throw new ReservaFacilException("Data da reserva não pode ser menor que a data do evento.");
            else if (evento.QuantidadeReservada >= evento.QuantidadeLimite)
                throw new ReservaFacilException("Quantidade de reservas não pode ser maior que a quantidade máxima.");

            var reservaPersistida = await _repositorioBase.AdicionarAsync(reserva);

            evento.AtualizarEvento(evento.NomeEvento,
                                   evento.DataEvento,
                                   evento.DescricaoEvento,
                                   evento.QuantidadeReservada + 1,
                                   evento.QuantidadeLimite);

            await _repositorioBase.AtualizarAsync(evento);

            var reservaVM = _mapper.Map<ReservaViewModel>(reservaPersistida);

            return reservaVM;
        }

        public async Task<ReservaViewModel> AtualizarReservaAsync(Reserva reservaParaAtualizar)
        {
            var reserva = await _repositorioBase.ObterPorIdAsync<Reserva>(reservaParaAtualizar.Id);

            if (reserva is null) return null;

            if(reserva.UsuarioId != reservaParaAtualizar.UsuarioId)
                throw new ReservaFacilException("Usuário não tem permissão para atualizar este evento.");

            reserva.AtualizarReserva(reservaParaAtualizar.EventoId, reservaParaAtualizar.DataReserva);

            var reservaAtualizada = await _repositorioBase.AtualizarAsync(reserva);

            var reservaVM = _mapper.Map<ReservaViewModel>(reservaAtualizada);

            return reservaVM;
        }

        public async Task CancelarReservaPorIdAsync(int id, Guid idUsuario)
        {
            var reserva = await _repositorioBase.ObterPorIdAsync<Reserva>(id);

            if(reserva is null)
                throw new ReservaFacilException($"Reserva com id = {id}, não encontrada.");

            if (reserva.UsuarioId != idUsuario)
                throw new ReservaFacilException("Usuário não tem permissão para atualizar este evento.");

            if (reserva != null)
            {
                reserva.CancelarReserva();

                await _repositorioBase.AtualizarAsync(reserva);
            }
            else
                throw new ReservaFacilException($"Reserva com id = {id}, não encontrada.");
        }

        public async Task<List<ReservaViewModel>> ObterReservasPorUsuarioAsync(Guid usuarioId)
        {
            var reservas = await _repositorioBase.ObterReservasPorUsuarioAsync(usuarioId);

            if (reservas is null) return null;

            var reservasViewModel = _mapper.Map<List<ReservaViewModel>>(reservas);

            return reservasViewModel;
        }
    }
}

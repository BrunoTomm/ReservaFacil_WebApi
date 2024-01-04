using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Servicos.Interface
{
    public interface IReservaService
    {
        Task<ReservaViewModel> CriarReservaAsync(Reserva reserva);
        Task<List<ReservaViewModel>> ObterReservasPorUsuarioAsync(Guid usuarioId);
        Task<ReservaViewModel> AtualizarReservaAsync(Reserva reserva);
        Task CancelarReservaPorIdAsync(int id, Guid idUsuario);
    }
}

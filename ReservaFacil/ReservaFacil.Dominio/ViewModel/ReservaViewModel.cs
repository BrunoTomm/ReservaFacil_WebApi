using ReservaFacil.Dominio.Modelos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaFacil.Dominio.ViewModel;

public class ReservaViewModel
{
    [Required]
    public Guid UsuarioId { get; set; }

    [Required]
    public int EventoId { get; set; }

    [Required]
    public DateTime DataReserva { get; set; }
}

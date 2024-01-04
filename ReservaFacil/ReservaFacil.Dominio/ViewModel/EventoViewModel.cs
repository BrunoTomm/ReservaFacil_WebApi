using System.ComponentModel.DataAnnotations;

namespace ReservaFacil.Dominio.ViewModel;

public class EventoViewModel
{
    [Required]
    [StringLength(100)]
    public string NomeEvento { get; set; }

    [Required]
    public DateTime DataEvento { get; set; }

    [Required]
    public Guid UsuarioId { get; set; }

    [MaxLength(500)]
    public string DescricaoEvento { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A QuantidadeLimite deve ser maior que zero.")]
    public int QuantidadeLimite { get; set; }
}

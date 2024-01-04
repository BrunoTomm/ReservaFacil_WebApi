using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReservaFacil.Dominio.Modelos;

public class Reserva
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public Guid UsuarioId { get; private set; }

    [ForeignKey("Evento")]
    public int EventoId { get; private set; }

    [DataType(DataType.DateTime)]
    public DateTime DataReserva { get; private set; }

    public bool ReservaAtiva { get; private set; }

    [InverseProperty("Reservas")]
    public virtual Evento Evento { get; private set; }

    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; private set; }

    public Reserva()
    {           
    }


    public Reserva(int idReserva, Guid usuarioId, int eventoId, DateTime dataReserva)
    {
        Id = idReserva;
        UsuarioId = usuarioId;
        EventoId = eventoId;
        DataReserva = dataReserva;
        ReservaAtiva = true;
    }

    public void CancelarReserva()
    {
        ReservaAtiva = false;
    }

    public void AtualizarReserva(int eventoId, DateTime dataReserva)
    {
        EventoId = eventoId;
        DataReserva = dataReserva;
    }

    public void ConverterParaEntidade(int idReserva, Guid usuarioId, int eventoId, DateTime dataReserva)
    {
        Id = idReserva;
        UsuarioId = usuarioId;
        EventoId = eventoId;
        DataReserva = dataReserva;
        ReservaAtiva = true;
    }

}

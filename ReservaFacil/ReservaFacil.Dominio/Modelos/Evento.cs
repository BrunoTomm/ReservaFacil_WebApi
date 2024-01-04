using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReservaFacil.Dominio.Modelos;

public class Evento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [StringLength(100)]
    public string NomeEvento { get; private set; }
    public string DescricaoEvento { get; private set; }
    public int QuantidadeLimite { get; private set; }
    public int QuantidadeReservada { get; private set; }
    public DateTime DataEvento { get; private set; }
    public Guid UsuarioId { get; private set; }


    [ForeignKey("UsuarioId")]
    public Usuario Usuario { get; private set; }

    [InverseProperty("Evento")]
    public ICollection<Reserva> Reservas { get; private set; }

    

    public static Evento ConverterParaEntidade(
        int idEvento,
        string nomeEvento,
        string descricaoEvento,
        DateTime dataEvento,
        Guid usuarioId,
        int quantidadeLimite)
    {
        return new Evento
        {
            Id = idEvento,
            NomeEvento = nomeEvento,
            DescricaoEvento = descricaoEvento,
            DataEvento = dataEvento,
            UsuarioId = usuarioId,
            QuantidadeLimite = quantidadeLimite
        };
    }

    public void AtualizarEvento(
        string nomeEvento,
        DateTime dataEvento,
        string descricaoEvento,
        int quantidadeReservada,
        int quantidadeLimite)
    {
        NomeEvento = nomeEvento;
        DataEvento = dataEvento;
        DescricaoEvento = descricaoEvento;
        QuantidadeReservada = quantidadeReservada;
        QuantidadeLimite = quantidadeLimite;
    }

    public void AtualizarDataEvento(DateTime dataEvento)
    {
        DataEvento = dataEvento;
    }

    public void AtualizaIdParaTeste(int id)
    {
        Id = id;
    }
}



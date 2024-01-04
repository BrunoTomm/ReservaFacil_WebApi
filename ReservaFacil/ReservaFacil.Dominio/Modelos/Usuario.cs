using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaFacil.Dominio.Modelos;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UsuarioId { get; private set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; private set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; private set; }

    public static Usuario ConverterParaEntidade(string nome, string email)
    {
        return new Usuario { Nome = nome, Email = email };
    }

    public void Atualizar(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public void ConverterParaEntidade(Guid usuarioId, string nome, string email)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Email = email;
    }
}

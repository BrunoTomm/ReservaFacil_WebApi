using ReservaFacil.Dominio.Modelos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaFacil.Dominio.ViewModel;

public class UsuarioViewModel
{
    public string Nome { get; set; }

    public string Email { get; set; }
}

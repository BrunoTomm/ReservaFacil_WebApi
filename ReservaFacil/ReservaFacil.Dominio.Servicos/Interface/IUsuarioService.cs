using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.Dominio.Servicos.Interface
{
    public interface IUsuarioService
    {
        Task<Usuario> BuscaUsuarioAsync(Usuario usuario);
    }
}

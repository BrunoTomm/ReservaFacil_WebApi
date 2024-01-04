using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;

namespace ReservaFacil.WebApi.Interface
{
    public interface IUsuarioConversor
    {
        Usuario ConverterParaEntidade(UsuarioViewModel usuarioViewModel);
    }
}

using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.WebApi.Interface;

namespace ReservaFacil.WebApi.Conversor;

public class UsuarioConversor : IUsuarioConversor
{
    public Usuario ConverterParaEntidade(UsuarioViewModel usuarioViewModel)
    {
        return Usuario.ConverterParaEntidade(usuarioViewModel.Nome, usuarioViewModel.Email);
    }
}

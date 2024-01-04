using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReservaFacil.Dominio.Exceptions;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.Infra.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReservaFacil.Dominio.Servicos.Servicos
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepositorioBase _repositorioBase;
        private readonly IMapper _mapper;

        public UsuarioService(IRepositorioBase repositorioBase, IMapper mapper)
        {
            _repositorioBase = repositorioBase;
            _mapper = mapper;
        }
        public async Task<Usuario> BuscaUsuarioAsync(Usuario usuario)
        {
            var usuarioAuth = await _repositorioBase.ObterUsuarioPorNomeEmailAsync(usuario.Nome, usuario.Email);

            if (usuarioAuth is null)
                throw new ReservaFacilException($"Usuário ou Email inválidos.");

            return usuarioAuth;
        }
    }
}

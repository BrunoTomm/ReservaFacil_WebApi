using ReservaFacil.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReservaFacil.Infra.Interface;

public interface IRepositorioBase
{
    Task<T> AdicionarAsync<T>(T entidade) where T : class;
    Task<T> AtualizarAsync<T>(T entidade) where T : class;
    Task ExcluirAsync<T>(T entidade) where T : class;
    Task<T> ObterPorIdAsync<T>(int id) where T : class;
    Task<T> ObterPorIdAsync<T>(Guid id) where T : class;
    Task<List<T>> ObterTodosAsync<T>() where T : class;
    Task<List<T>> BuscarAsync<T>(Expression<Func<T, bool>> predicado) where T : class;
    Task<List<Reserva>> ObterReservasPorUsuarioAsync(Guid usuarioId);
    Task<Usuario> ObterUsuarioPorNomeEmailAsync(string nome, string email);
}

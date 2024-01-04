using Microsoft.EntityFrameworkCore;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Infra.Contextos;
using ReservaFacil.Infra.Interface;
using System.Linq.Expressions;

namespace ReservaFacil.Infra.Repositorios;

public class RepositorioBase : IRepositorioBase
{
    private readonly ContextoReservaFacil _contexto;

    public RepositorioBase(ContextoReservaFacil contexto)
    {
        _contexto = contexto;
    }

    public async Task<T> AdicionarAsync<T>(T entidade) where T : class
    {
        await _contexto.Set<T>().AddAsync(entidade);
        await _contexto.SaveChangesAsync();
        return entidade;
    }

    public async Task<T> AtualizarAsync<T>(T entidade) where T : class
    {
        _contexto.Set<T>().Update(entidade);
        await _contexto.SaveChangesAsync();
        return entidade;
    }

    public async Task<List<T>> BuscarAsync<T>(Expression<Func<T, bool>> predicado) where T : class
    {
        return await _contexto.Set<T>().Where(predicado).ToListAsync();
    }

    public async Task ExcluirAsync<T>(T entidade) where T : class
    {
        _contexto.Set<T>().Remove(entidade);
        await _contexto.SaveChangesAsync();
    }

    public async Task<T> ObterPorIdAsync<T>(int id) where T : class
    {
        return await _contexto.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> ObterTodosAsync<T>() where T : class
    {
        return await _contexto.Set<T>().ToListAsync();
    }

    public async Task<List<Reserva>> ObterReservasPorUsuarioAsync(Guid usuarioId)
    {
        return await _contexto.Set<Reserva>()
            .Where(reserva => reserva.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<T> ObterPorIdAsync<T>(Guid id) where T : class
    {
        return await _contexto.Set<T>().FindAsync(id);
    }

    public async Task<Usuario> ObterUsuarioPorNomeEmailAsync(string nome, string email)
    {
        return await _contexto.Set<Usuario>()
            .FirstOrDefaultAsync(usuario => usuario.Nome == nome && usuario.Email == email);
    }

}

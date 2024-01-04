using Microsoft.EntityFrameworkCore;
using ReservaFacil.Dominio.Modelos;

namespace ReservaFacil.Infra.Contextos;

public class ContextoReservaFacil : DbContext
{
    public ContextoReservaFacil(DbContextOptions<ContextoReservaFacil> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
}

using AutoMapper;
using Moq;
using ReservaFacil.Dominio.Exceptions;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.Servicos.Servicos;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.Infra.Interface;

namespace ReservaFacil.Testes.Unitarios.Servicos;

public class ReservaServiceTeste
{
    [Fact]
    public async Task CriarReservaAsync_DeveCriarReservaCorretamente()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var reservaService = new ReservaService(repositorioMock.Object, mapperMock.Object);

        var usuarioId = Guid.NewGuid();
        var eventoId = 1;

        var usuario = new Usuario();
        usuario.ConverterParaEntidade(usuarioId, "Teste", "");

        var evento = Evento.ConverterParaEntidade(eventoId, "Teste", "Teste", DateTime.Now.AddDays(10), usuarioId, 10);

        repositorioMock.Setup(repo => repo.ObterPorIdAsync<Usuario>(usuarioId))
                       .ReturnsAsync(usuario);

        repositorioMock.Setup(repo => repo.ObterPorIdAsync<Evento>(eventoId))
                       .ReturnsAsync(evento);

        var reserva = new Reserva();
        reserva.ConverterParaEntidade(1, usuarioId, eventoId, DateTime.Now.AddDays(15));

        var reservaPersistida = reserva;

        repositorioMock.Setup(repo => repo.AdicionarAsync(reserva))
                       .ReturnsAsync(reservaPersistida);

        mapperMock.Setup(m => m.Map<ReservaViewModel>(It.IsAny<Reserva>()))
                        .Returns<Reserva>(reserva => new ReservaViewModel
                        {
                            DataReserva = reserva.DataReserva,
                            EventoId = reserva.EventoId,
                        }); ;

        // Act
        var resultado = await reservaService.CriarReservaAsync(reserva);

        // Assert
        Assert.NotNull(resultado);
        Assert.IsType<ReservaViewModel>(resultado);
    }

    [Fact]
    public async Task CriarReservaAsync_DeveLancarExcecao_QuandoUsuarioNaoExistir()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var reservaService = new ReservaService(repositorioMock.Object, mapperMock.Object);

        var reserva = new Reserva();

        reserva.ConverterParaEntidade(1, Guid.NewGuid(), 1, DateTime.Now.AddDays(1));

        repositorioMock.Setup(repo => repo.ObterPorIdAsync<Usuario>(reserva.UsuarioId))
                       .ReturnsAsync((Usuario)null);

        // Act & Assert
        await Assert.ThrowsAsync<ReservaFacilException>(async () => await reservaService.CriarReservaAsync(reserva));
    }

    [Fact]
    public async Task CriarReservaAsync_DeveLancarExcecao_QuandoEventoNaoExistir()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var reservaService = new ReservaService(repositorioMock.Object, mapperMock.Object);

        var reserva = new Reserva();
        reserva.ConverterParaEntidade(1, Guid.NewGuid(), 1, DateTime.Now.AddDays(1));

        repositorioMock.Setup(repo => repo.ObterPorIdAsync<Evento>(reserva.EventoId))
                       .ReturnsAsync((Evento)null);

        // Act & Assert
        await Assert.ThrowsAsync<ReservaFacilException>(async () => await reservaService.CriarReservaAsync(reserva));
    }

    [Fact]
    public async Task AtualizarReservaAsync_DeveRetornarNull_QuandoReservaNaoExistir()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var reservaService = new ReservaService(repositorioMock.Object, mapperMock.Object);

        var reservaParaAtualizar = new Reserva();
        reservaParaAtualizar.ConverterParaEntidade(1, Guid.NewGuid(), 1, DateTime.Now.AddDays(1));

        repositorioMock.Setup(repo => repo.ObterPorIdAsync<Reserva>(reservaParaAtualizar.Id))
                       .ReturnsAsync((Reserva)null);

        // Act
        var resultado = await reservaService.AtualizarReservaAsync(reservaParaAtualizar);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public async Task CancelarReservaPorIdAsync_DeveLancarExcecao_QuandoReservaNaoExistir()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var reservaService = new ReservaService(repositorioMock.Object, mapperMock.Object);

        var idReserva = 1;
        var idUsuario = Guid.NewGuid();

        repositorioMock.Setup(repo => repo.ObterPorIdAsync<Reserva>(idReserva))
                       .ReturnsAsync((Reserva)null);

        // Act & Assert
        await Assert.ThrowsAsync<ReservaFacilException>(async () => await reservaService.CancelarReservaPorIdAsync(idReserva, idUsuario));
    }

    [Fact]
    public async Task ObterReservasPorUsuarioAsync_DeveRetornarNull_QuandoReservasNaoExistirem()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var reservaService = new ReservaService(repositorioMock.Object, mapperMock.Object);

        var usuarioId = Guid.NewGuid();

        repositorioMock.Setup(repo => repo.ObterReservasPorUsuarioAsync(usuarioId))
                       .ReturnsAsync((List<Reserva>)null);

        // Act
        var resultado = await reservaService.ObterReservasPorUsuarioAsync(usuarioId);

        // Assert
        Assert.Null(resultado);
    }

}

using AutoMapper;
using Moq;
using ReservaFacil.Dominio.Exceptions;
using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.Servicos.Servicos;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.Infra.Interface;

namespace ReservaFacil.Testes.Unitarios.Eventos;

public class EventoServicoTeste
{
    [Fact]
    public async Task CriarEvento_DeveRetornarEventoViewModel_QuandoDadosValidos()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var eventoService = new EventoService(repositorioMock.Object, mapperMock.Object);

        var evento = new Evento();
        evento.AtualizarDataEvento(DateTime.Now.AddDays(1));

        repositorioMock.Setup(repo => repo.ObterPorIdAsync<Usuario>(evento.UsuarioId))
                       .ReturnsAsync(new Usuario());

        mapperMock.Setup(m => m.Map<EventoViewModel>(It.IsAny<Reserva>()))
                        .Returns<Reserva>(reserva => new EventoViewModel
                        {
                            DataEvento = evento.DataEvento,
                            DescricaoEvento = evento.DescricaoEvento,
                            NomeEvento = evento.NomeEvento,
                            QuantidadeLimite = evento.QuantidadeLimite,
                        }); ;

        // Act
        var resultado = await eventoService.CriarEventoAsync(evento);

        // Assert
        Assert.NotNull(resultado);
        Assert.IsType<EventoViewModel>(resultado);
    }

    [Fact]
    public async Task CriarEvento_DeveLancarExcecao_QuandoDataDoEventoForMenorQueDataAtual()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var eventoService = new EventoService(repositorioMock.Object, mapperMock.Object);

        var evento = new Evento();

        evento.AtualizarDataEvento(DateTime.Now.AddDays(-1));

        // Act & Assert
        await Assert.ThrowsAsync<ReservaFacilException>(async () => await eventoService.CriarEventoAsync(evento));
    }

    [Fact]
    public async Task AtualizaEvento_DeveRetornarEventoViewModel_QuandoDadosValidos()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var eventoService = new EventoService(repositorioMock.Object, mapperMock.Object);

        var evento = new Evento();
        evento.AtualizaIdParaTeste(1);
        var eventoParaAtualizar = new Evento();
        eventoParaAtualizar.AtualizaIdParaTeste(1);

        repositorioMock.Setup(repo => repo.ObterPorIdAsync<Evento>(eventoParaAtualizar.Id))
                       .ReturnsAsync(eventoParaAtualizar);

        mapperMock.Setup(m => m.Map<EventoViewModel>(It.IsAny<Reserva>()))
            .Returns<Reserva>(reserva => new EventoViewModel
            {
                DataEvento = evento.DataEvento,
                DescricaoEvento = evento.DescricaoEvento,
                NomeEvento = evento.NomeEvento,
                QuantidadeLimite = evento.QuantidadeLimite,
            }); ;

        // Act
        var resultado = await eventoService.AtualizaEventoAsync(eventoParaAtualizar);

        // Assert
        Assert.NotNull(resultado);
        Assert.IsType<EventoViewModel>(resultado);
    }

    [Fact]
    public async Task AtualizaEvento_DeveLancarExcecao_QuandoEventoNaoExistir()
    {
        // Arrange
        var repositorioMock = new Mock<IRepositorioBase>();
        var mapperMock = new Mock<IMapper>();

        var eventoService = new EventoService(repositorioMock.Object, mapperMock.Object);

        var eventoParaAtualizar = new Evento();

        eventoParaAtualizar.AtualizaIdParaTeste(1);

        repositorioMock.Setup(repo => repo.ObterPorIdAsync<Evento>(eventoParaAtualizar.Id))
                       .ReturnsAsync((Evento)null);

        // Act & Assert
        var excecao = await Assert.ThrowsAsync<ReservaFacilException>(async () => await eventoService.AtualizaEventoAsync(eventoParaAtualizar));

        Assert.Equal("É necessário um evento válido.", excecao.Message);
    }

}

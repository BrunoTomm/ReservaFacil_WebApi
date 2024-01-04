using ReservaFacil.Dominio.Modelos;
using ReservaFacil.Dominio.ViewModel;
using ReservaFacil.WebApi.Interface;

namespace ReservaFacil.WebApi.Conversor;

public class EventoConversor : IEventoConversor
{
    public Evento ConverterParaEntidade(EventoViewModel eventoViewModel, int idEvento)
    {
        return Evento.ConverterParaEntidade(idEvento,
                                            eventoViewModel.NomeEvento,
                                            eventoViewModel.DescricaoEvento,
                                            eventoViewModel.DataEvento,
                                            eventoViewModel.UsuarioId,
                                            eventoViewModel.QuantidadeLimite);
    }
}

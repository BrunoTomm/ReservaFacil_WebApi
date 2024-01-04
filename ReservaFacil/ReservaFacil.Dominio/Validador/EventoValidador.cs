using FluentValidation;
using ReservaFacil.Dominio.Modelos;

namespace ReservaFacil.Dominio.Validador
{
    public class EventoValidador : AbstractValidator<Evento>
    {
        public EventoValidador()
        {
            RuleFor(x => x.NomeEvento).NotEmpty().WithMessage("Nome do evento não pode ser vazio");
        }
    }
}

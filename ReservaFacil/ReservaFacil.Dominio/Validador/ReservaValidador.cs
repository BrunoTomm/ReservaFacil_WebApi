using FluentValidation;
using ReservaFacil.Dominio.Modelos;
using System;

namespace ReservaFacil.Dominio.Validador
{
    public class ReservaValidador : AbstractValidator<Reserva>
    {
        public ReservaValidador()
        {
            RuleFor(reserva => reserva.DataReserva)
                .GreaterThan(DateTime.Now).WithMessage("A data da reserva deve ser maior que a data atual.");

            RuleFor(reserva => reserva.EventoId)
                .NotNull().WithMessage("A reserva deve estar associada a um evento.");
        }
    }
}

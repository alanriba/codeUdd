using System;
using System.Linq;
using CodeUdd.Data.Command;
using CodeUdd.Data.Model;
using MediatR;

namespace CodeUdd.Data.Handler
{
    public class RegistrarPersonaCommandHandler : IRequestHandler<RegistrarPersonaCommand, int>
    {
        private readonly PersonaContext _context;

        public RegistrarPersonaCommandHandler(PersonaContext dbContext)
        {
            _context = dbContext;
        }

        public int Handle(RegistrarPersonaCommand persona)
        {
            var entityPersona = new Persona
            {
                Id = _context.Set<Persona>().Count() + 1,
                Nombre = persona.Nombre,
                Fecha = (DateTime)persona?.Fecha,
                EsFeriado = (bool)persona?.EsFeriado
            };

            _context.Persona.Add(entityPersona);
            _context?.SaveChangesAsync();
            return entityPersona.Id;
        }
    }
}

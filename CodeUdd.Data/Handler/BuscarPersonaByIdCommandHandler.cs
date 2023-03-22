using System;
using CodeUdd.Data.Command;
using CodeUdd.Data.Model;
using MediatR;

namespace CodeUdd.Data.Handler
{
    public class BuscarPersonaByIdCommandHandler: IRequestHandler<BuscarPersonaByIdCommand, Persona>
    {
        private readonly PersonaContext _context;

        public BuscarPersonaByIdCommandHandler(PersonaContext dbContext)
        {
            _context = dbContext;
        }

        public Persona Handle(BuscarPersonaByIdCommand idPersona)
        {
            return _context.Persona.Find(idPersona);
        }
    }
}

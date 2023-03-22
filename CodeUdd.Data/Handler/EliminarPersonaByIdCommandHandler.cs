using System;
using MediatR;
using CodeUdd.Data.Command;
namespace CodeUdd.Data.Handler
{
    public class EliminarPersonaByIdCommandHandler: IRequestHandler<EliminarPersonaByIdCommand, bool>
    {
        private readonly PersonaContext _context;

        public EliminarPersonaByIdCommandHandler(PersonaContext dbContext)
        {
            _context = dbContext;
        }

        public bool Handle(EliminarPersonaByIdCommand persona)
        {
            _context?.Persona?.Remove(_context?.Persona?.Find(persona.Id));
            _context?.SaveChangesAsync();

            return true;
        }
    }
}

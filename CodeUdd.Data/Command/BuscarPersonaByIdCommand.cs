using CodeUdd.Data.Model;
using MediatR;

namespace CodeUdd.Data.Command
{
    public class BuscarPersonaByIdCommand: IRequest<Persona>
    {
        public int Id { get; set; }
    }
}

using System;
using MediatR;

namespace CodeUdd.Data.Command
{
    public class EliminarPersonaByIdCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}

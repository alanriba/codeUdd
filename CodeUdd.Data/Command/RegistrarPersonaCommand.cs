using System;
using CodeUdd.Data.Model;
using MediatR;

namespace CodeUdd.Data.Command
{
    public class RegistrarPersonaCommand : IRequest<int>
    {
        public string? Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? EsFeriado { get; set; }
    }
}

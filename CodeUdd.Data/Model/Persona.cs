using System;
namespace CodeUdd.Data.Model
{
    public class Persona
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsFeriado { get; set; }

        public static implicit operator int(Persona v)
        {
            throw new NotImplementedException();
        }
    }
}

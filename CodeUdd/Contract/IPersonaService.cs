using System.Threading.Tasks;
using CodeUdd.Dto;

namespace CodeUdd.Contract
{
    public interface IPersonaService
    {
        public Task<PersonaDto> GetPersona(int id);
        public Task<int> SavePersona(PersonaDto persona);
        public Task<bool> DeletePersona(int id);
    }
}

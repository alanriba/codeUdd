using System.Threading.Tasks;
using AutoMapper;
using CodeUdd.Contract;
using CodeUdd.Data.Command;
using CodeUdd.Dto;
using CodeUdd.Exceptions;
using MediatR;

namespace PruebaUdd.Business
{
    public class PersonaService : IPersonaService
    {
        private readonly IMediator _mediator;
        private readonly IFeriadoService _feriadoService;
        private readonly IMapper _mapper;

        public PersonaService(IMediator mediator, IFeriadoService feriadoService, IMapper mapper)
        {
            _mediator = mediator;
            _feriadoService = feriadoService;
            _mapper = mapper;
        }

        public async Task<bool> DeletePersona(int id)
        {
            var status = await _mediator.Send(new EliminarPersonaByIdCommand { Id = id });

            if (!status)
            {
                throw new ApiException("Error al momento de eliminar el registro id {}", id);
            }
            return status;
        }

        
        public async Task<PersonaDto> GetPersona(int id)
        {
            var persona = await _mediator.Send(new BuscarPersonaByIdCommand { Id = id });

            if (persona == null)
            {
                throw new NotFoundException("No se encontro información respecto al Id: {}", id);
            }
            return _mapper.Map<PersonaDto>(persona);
        }

        public async Task<int> SavePersona(PersonaDto persona)
        {
            var esFeriado = await _feriadoService.VerificarFeriado(persona.Fecha);
            
            var command = new RegistrarPersonaCommand
            {
                Nombre = persona.Nombre,
                Fecha = persona.Fecha,
                EsFeriado = esFeriado

            };

            var result = await _mediator.Send(command);


            return result ;
        }
    }
}

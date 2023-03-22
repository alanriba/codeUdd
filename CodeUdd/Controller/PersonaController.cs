using CodeUdd.Contract;
using CodeUdd.Dto;
using CodeUdd.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PruebaUdd.Controller
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        private readonly IFeriadoService _feriadoService;
        public PersonaController(IPersonaService personaBussiness, IFeriadoService feriadoService)
        {
            _personaService = personaBussiness;
            _feriadoService = feriadoService;
        }
             
  
        /// <summary>
        /// Operation to obtain details of person by Id
        /// </summary>
        /// <returns>obtain details of person by Id 200 OK, 404 Not Found datos no encontrados, 400 BadRequest o 500 error interno.</returns>
        /// <response code="200">Person find</response>
        /// <response code="400">Wrong service request.</response>
        /// <response code="404">Not Found person</response>
        /// <response code="500">Internal Error</response>
        /// 
        [HttpGet("find")]
        [ProducesResponseType(typeof(PersonaDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PersonaDto>> GetPersona([FromQuery] int param)
        {
            return await _personaService.GetPersona(param);
        }

        /// <summary>
        /// Operation to save person
        /// </summary>
        /// <returns>Save Person 201 CREATED, 404 Not Found datos no encontrados, 400 BadRequest o 500 error interno.</returns>
        /// <response code="200">Person created</response>
        /// <response code="400">Wrong service request.</response>
        /// <response code="404">Not Found person</response>
        /// <response code="500">Internal Error</response>
        /// 
        [HttpPost("save")]
        [ProducesResponseType(typeof(PersonaDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<int>> SavePersona([FromBody] PersonaDto param)
        {
            return await _personaService.SavePersona(param);
        }

        /// <summary>
        /// Operation to update person
        /// </summary>
        /// <returns>Save Person 201 UPDATE, 404 Not Found datos no encontrados, 400 BadRequest o 500 error interno.</returns>
        /// <response code="201">Person update</response>
        /// <response code="400">Wrong service request.</response>
        /// <response code="404">Not Found person </response>
        /// <response code="500">Internal Error</response>
        /// 
        [HttpDelete("delete")]
        [ProducesResponseType(typeof(PersonaDto), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<bool>> DeletePerson([FromQuery] int param)
        {
            return await _personaService.DeletePersona(param);
        }

    }
}

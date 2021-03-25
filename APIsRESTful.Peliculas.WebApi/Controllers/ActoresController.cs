using APIsRESTful.Peliculas.DTO;
using APIsRESTful.Peliculas.IApplication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActoresController : Controller
    {
        private readonly IActoresApplication _actoresApplication;
        private IHttpContextAccessor _httpContextAccessor;


        public ActoresController(IActoresApplication actoresApplication, IHttpContextAccessor httpContextAccessor)
        {
            _actoresApplication = actoresApplication;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _actoresApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }

        [HttpGet("GetAllPaginado")]
        public async Task<IActionResult> GetAllPaginado([FromQuery] PaginacionDTO paginacionDTO)
        {
            var response = await _actoresApplication.GetAllPaginado(paginacionDTO);
            _httpContextAccessor.HttpContext.Response.Headers.Add("cantidadPaginas", response.cantidadPaginas.ToString());

            return Ok(response);
        }

        


        [HttpGet("GetAsync/{Id}", Name = "obtenerActor")]
        public async Task<IActionResult> GetAsync(string Id)
        {

            if (string.IsNullOrEmpty(Id))
                return BadRequest();

            var response = await _actoresApplication.GetAsync(Convert.ToInt32(Id));
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromForm] ActorCreateDTO actorCreateDTO)
        {

            if (actorCreateDTO == null)
                return BadRequest();

            var response = await _actoresApplication.InsertAsync(actorCreateDTO);
            if (response.IsSuccess)
                return new CreatedAtRouteResult("obtenerActor", new { Id = response.Data.Id }, response.Data);

            return BadRequest(response.Message);

        }


        [HttpPut("UpdateAsync/{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id, [FromForm] ActorCreateDTO actorCreateDTO)
        {

            if (actorCreateDTO == null)
                return BadRequest();

            var response = await _actoresApplication.UpdateAsync(Id, actorCreateDTO);
            if (response.IsSuccess)
                return new CreatedAtRouteResult("obtenerActor", new { Id = response.Data.Id }, response.Data);

            return BadRequest(response.Message);

        }

        [HttpPatch("PatchAsync/{Id}")]
        public async Task<IActionResult> PatchAsync(int Id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument )
        {

            if (patchDocument == null)
                return BadRequest();
            
            var response = await _actoresApplication.PatchAsync(Id, patchDocument);
            if (response.IsSuccess)
                return new CreatedAtRouteResult("obtenerActor", new { Id = response.Data.Id }, response.Data);

            return BadRequest(response.Message);

        }

        [HttpDelete("DeleteAsync/{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {

            var response = await _actoresApplication.DeleteAsync(Id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }
    }
}

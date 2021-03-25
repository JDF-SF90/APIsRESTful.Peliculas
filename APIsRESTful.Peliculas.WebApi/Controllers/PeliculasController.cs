using APIsRESTful.Peliculas.DTO;
using APIsRESTful.Peliculas.IApplication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : Controller
    {
        private readonly IPeliculasApplication _peliculasApplication;
        private IHttpContextAccessor _httpContextAccessor;


        public PeliculasController(IPeliculasApplication peliculasApplication, IHttpContextAccessor httpContextAccessor)
        {
            _peliculasApplication = peliculasApplication;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _peliculasApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }

        [HttpGet("Filtro")]
        public async Task<IActionResult> Filtro([FromQuery] FiltroPeliculasDTO filtroPeliculasDTO)
        {
            var response = await _peliculasApplication.Filtro(filtroPeliculasDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }

        [HttpGet("GetAllPaginado")]
        public async Task<IActionResult> GetAllPaginado([FromQuery] PaginacionDTO paginacionDTO)
        {
            var response = await _peliculasApplication.GetAllPaginado(paginacionDTO);
            _httpContextAccessor.HttpContext.Response.Headers.Add("cantidadPaginas", response.cantidadPaginas.ToString());

            return Ok(response);
        }




        [HttpGet("GetAsync/{Id}", Name = "obtenerPelicula")]
        public async Task<IActionResult> GetAsync(string Id)
        {

            if (string.IsNullOrEmpty(Id))
                return BadRequest();

            var response = await _peliculasApplication.GetAsync(Convert.ToInt32(Id));
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromForm] PeliculaCreateDTO peliculaCreateDTO)
        {

            if (peliculaCreateDTO == null)
                return BadRequest();

            var response = await _peliculasApplication.InsertAsync(peliculaCreateDTO);
            if (response.IsSuccess)
                return new CreatedAtRouteResult("obtenerPelicula", new { Id = response.Data.Id }, response.Data);

            return BadRequest(response.Message);

        }


        [HttpPut("UpdateAsync/{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id, [FromForm] PeliculaCreateDTO peliculaCreateDTO)
        {

            if (peliculaCreateDTO == null)
                return BadRequest();

            var response = await _peliculasApplication.UpdateAsync(Id, peliculaCreateDTO);
            if (response.IsSuccess)
                return new CreatedAtRouteResult("obtenerPelicula", new { Id = response.Data.Id }, response.Data);

            return BadRequest(response.Message);

        }

        [HttpPatch("PatchAsync/{Id}")]
        public async Task<IActionResult> PatchAsync(int Id, [FromBody] JsonPatchDocument<PeliculaPatchDTO> patchDocument)
        {

            if (patchDocument == null)
                return BadRequest();

            var response = await _peliculasApplication.PatchAsync(Id, patchDocument);
            if (response.IsSuccess)
                return new CreatedAtRouteResult("obtenerPelicula", new { Id = response.Data.Id }, response.Data);

            return BadRequest(response.Message);

        }

        [HttpDelete("DeleteAsync/{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {

            var response = await _peliculasApplication.DeleteAsync(Id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }
    }
}

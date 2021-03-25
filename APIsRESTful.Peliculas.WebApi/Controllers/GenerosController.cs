using APIsRESTful.Peliculas.DTO;
using APIsRESTful.Peliculas.IApplication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GenerosController : Controller
    {
        private readonly IGenerosApplication _generosApplication;

        public GenerosController(IGenerosApplication generosApplication)
        {
            _generosApplication = generosApplication;
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _generosApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }


        [HttpGet("GetAsync/{Id}", Name = "obtenerGenero")]
        public async Task<IActionResult> GetAsync(string Id)
        {
           
            if (string.IsNullOrEmpty(Id))
                return BadRequest();

            var response = await _generosApplication.GetAsync(Convert.ToInt32(Id));
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] GeneroCreateDTO generoCreateDTO)
        {

            if (generoCreateDTO == null)
                return BadRequest();

            var response = await _generosApplication.InsertAsync(generoCreateDTO);
            if (response.IsSuccess)
                return new CreatedAtRouteResult("obtenergenero", new { Id = response.Data.Id}, response.Data); 

            return BadRequest(response.Message);

        }


        [HttpPut("UpdateAsync/{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id, [FromBody] GeneroCreateDTO generoCreateDTO)
        {

            if (generoCreateDTO == null)
                return BadRequest();

            var response = await _generosApplication.UpdateAsync(Id, generoCreateDTO);
            if (response.IsSuccess)
                return new CreatedAtRouteResult("obtenergenero", new { Id = response.Data.Id }, response.Data);

            return BadRequest(response.Message);

        }

        [HttpDelete("DeleteAsync/{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {

            var response = await _generosApplication.DeleteAsync(Id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);

        }

    }
}

using APIsRESTful.Peliculas.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;
using APIsRESTful.Peliculas.Entities;

namespace APIsRESTful.Peliculas.IApplication
{
    public interface IActoresApplication
    {
        Task<Response<IEnumerable<ActorDTO>>> GetAllAsync();

        Task<Response<ActorDTO>> GetAsync(int Id);

        Task<Response<ActorDTO>> InsertAsync(ActorCreateDTO actor);

        Task<Response<ActorDTO>> UpdateAsync(int Id, ActorCreateDTO actor);

        Task<Response<bool>> DeleteAsync(int Id);


        Task<Response<ActorDTO>> PatchAsync(int Id, JsonPatchDocument<ActorPatchDTO> patchDocument);

        Task<Response<List<ActorDTO>>> GetAllPaginado(PaginacionDTO paginacionDTO);
    }
}

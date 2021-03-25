using APIsRESTful.Peliculas.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.IApplication
{
    public interface IPeliculasApplication
    {
        Task<Response<IEnumerable<PeliculaDTO>>> GetAllAsync();

        Task<Response<PeliculaDetallesDTO>> GetAsync(int Id);

        Task<Response<PeliculaDTO>> InsertAsync(PeliculaCreateDTO pelicula);

        Task<Response<PeliculaDTO>> UpdateAsync(int Id, PeliculaCreateDTO pelicula);

        Task<Response<bool>> DeleteAsync(int Id);


        Task<Response<PeliculaDTO>> PatchAsync(int Id, JsonPatchDocument<PeliculaPatchDTO> patchDocument);

        Task<Response<List<PeliculaDTO>>> GetAllPaginado(PaginacionDTO paginacionDTO);

        Task<Response<List<PeliculaDTO>>> Filtro(FiltroPeliculasDTO filtroPeliculasDTO);
    }
}

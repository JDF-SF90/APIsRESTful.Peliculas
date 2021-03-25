using APIsRESTful.Peliculas.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.IApplication
{
    public interface IGenerosApplication
    {
        Task<Response<IEnumerable<GeneroDTO>>> GetAllAsync();

        Task<Response<GeneroDTO>> GetAsync(int Id);

        Task<Response<GeneroDTO>> InsertAsync(GeneroCreateDTO genero);

        Task<Response<GeneroDTO>> UpdateAsync(int Id, GeneroCreateDTO genero);

        Task<Response<bool>> DeleteAsync(int Id);

    }
}

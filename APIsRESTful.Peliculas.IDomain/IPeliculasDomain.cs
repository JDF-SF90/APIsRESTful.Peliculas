using APIsRESTful.Peliculas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.IDomain
{
    public interface IPeliculasDomain
    {
        Task<IEnumerable<Pelicula>> GetAllAsync();

        Task<Pelicula> GetAsync(int Id);

        Task<Pelicula> InsertAsync(Pelicula pelicula);

        Task<Pelicula> UpdateAsync(Pelicula pelicula);

        Task<bool> DeleteAsync(int Id);

        Task<Pelicula> ExistAsync(int Id);

        IQueryable<Pelicula> GetAllAsQueryable();

    }
}

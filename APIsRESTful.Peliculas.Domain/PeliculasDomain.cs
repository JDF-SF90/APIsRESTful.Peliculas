using APIsRESTful.Peliculas.Entities;
using APIsRESTful.Peliculas.IDataAccess;
using APIsRESTful.Peliculas.IDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.Domain
{
    public class PeliculasDomain : IPeliculasDomain
    {
        private readonly IPeliculasRepository _peliculasRepository;

        public PeliculasDomain(IPeliculasRepository peliculasRepository)
        {
            _peliculasRepository = peliculasRepository;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            return await _peliculasRepository.DeleteAsync(Id);
        }

        public async Task<Pelicula> ExistAsync(int Id)
        {
            return await _peliculasRepository.ExistAsync(Id);
        }

        public async Task<IEnumerable<Pelicula>> GetAllAsync()
        {
            return await _peliculasRepository.GetAllAsync();
        }

        public async Task<Pelicula> GetAsync(int Id)
        {
            return await _peliculasRepository.GetAsync(Id);
        }

        public async Task<Pelicula> InsertAsync(Pelicula pelicula)
        {
            return await _peliculasRepository.InsertAsync(pelicula);
        }

        public async Task<Pelicula> UpdateAsync(Pelicula pelicula)
        {
            return await _peliculasRepository.UpdateAsync(pelicula);
        }

        public IQueryable<Pelicula> GetAllAsQueryable()
        {
            return _peliculasRepository.GetAllAsQueryable();
        }

    }
}

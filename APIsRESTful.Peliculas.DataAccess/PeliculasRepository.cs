using APIsRESTful.Peliculas.Entities;
using APIsRESTful.Peliculas.IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.DataAccess
{
    public class PeliculasRepository : IPeliculasRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PeliculasRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var exist = await _applicationDbContext.Actores.AnyAsync(x => x.Id == Id);

            if (!exist)
                return false;

            _applicationDbContext.Remove(new Pelicula() { Id = Id });
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Pelicula> ExistAsync(int Id)
        {
            return await _applicationDbContext.Peliculas
                .Include(x => x.PeliculasActores)
                .Include(x => x.PeliculasGeneros)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Pelicula>> GetAllAsync()
        {
            return await _applicationDbContext.Peliculas.ToListAsync();
        }

        public IQueryable<Pelicula> GetAllAsQueryable()
        {
            return _applicationDbContext.Peliculas.AsQueryable();
        }

        public async Task<Pelicula> GetAsync(int Id)
        {
            return await _applicationDbContext.Peliculas
                .Include(x => x.PeliculasActores).ThenInclude(x => x.Actor)
                .Include(x => x.PeliculasGeneros).ThenInclude(x => x.Genero)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Pelicula> InsertAsync(Pelicula pelicula)
        {
            _applicationDbContext.Add(pelicula);
            await _applicationDbContext.SaveChangesAsync();
            return pelicula;
        }

        public async Task<Pelicula> UpdateAsync(Pelicula pelicula)
        {

            _applicationDbContext.Entry(pelicula).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
            return pelicula;
        }
    }
}

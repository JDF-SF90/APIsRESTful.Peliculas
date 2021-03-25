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
    public class GenerosRepository : IGenerosRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenerosRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var exist = await _applicationDbContext.Generos.AnyAsync(x => x.Id == Id);

            if (!exist)
                return false;

            _applicationDbContext.Remove(new Genero() { Id = Id });
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Genero>> GetAllAsync()
        {
            return await _applicationDbContext.Generos.ToListAsync();
        }

        public async Task<Genero> GetAsync(int Id)
        {
            return await _applicationDbContext.Generos.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Genero> InsertAsync(Genero genero)
        {
            _applicationDbContext.Add(genero);
            await _applicationDbContext.SaveChangesAsync();
            return genero; 
        }

        public async Task<Genero> UpdateAsync(Genero genero)
        {

            _applicationDbContext.Entry(genero).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
            return genero;
        }
    }
}

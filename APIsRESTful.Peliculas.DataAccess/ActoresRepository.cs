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
    public class ActoresRepository : IActoresRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ActoresRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var exist = await _applicationDbContext.Actores.AnyAsync(x => x.Id == Id);

            if (!exist)
                return false;

            _applicationDbContext.Remove(new Actor() { Id = Id });
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Actor> ExistAsync(int Id)
        {
            return await _applicationDbContext.Actores.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await _applicationDbContext.Actores.ToListAsync();
        }

        public IQueryable<Actor> GetAllAsQueryable()
        {
            return _applicationDbContext.Actores.AsQueryable();
        }

        public async Task<Actor> GetAsync(int Id)
        {
            return await _applicationDbContext.Actores.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Actor> InsertAsync(Actor actor)
        {
            _applicationDbContext.Add(actor);
            await _applicationDbContext.SaveChangesAsync();
            return actor;
        }

        public async Task<Actor> UpdateAsync(Actor actor)
        {

            _applicationDbContext.Entry(actor).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
            return actor;
        }
    }
}

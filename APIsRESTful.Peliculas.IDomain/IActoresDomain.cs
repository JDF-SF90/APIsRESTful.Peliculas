using APIsRESTful.Peliculas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APIsRESTful.Peliculas.IDomain
{
    public interface IActoresDomain
    {
        Task<IEnumerable<Actor>> GetAllAsync();

        Task<Actor> GetAsync(int Id);

        Task<Actor> InsertAsync(Actor actor);

        Task<Actor> UpdateAsync(Actor actor);

        Task<bool> DeleteAsync(int Id);

        Task<Actor> ExistAsync(int Id);

        IQueryable<Actor> GetAllAsQueryable();

    }
}

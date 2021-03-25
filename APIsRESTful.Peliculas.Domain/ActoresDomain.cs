using APIsRESTful.Peliculas.DTO;
using APIsRESTful.Peliculas.Entities;
using APIsRESTful.Peliculas.IDataAccess;
using APIsRESTful.Peliculas.IDomain;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.Domain
{
    public class ActoresDomain : IActoresDomain
    {
        private readonly IActoresRepository _actoresRepository;

        public ActoresDomain(IActoresRepository actoresRepository)
        {
            _actoresRepository = actoresRepository;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            return await _actoresRepository.DeleteAsync(Id);
        }

        public async Task<Actor> ExistAsync(int Id)
        {
            return await _actoresRepository.ExistAsync(Id);
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await _actoresRepository.GetAllAsync();
        }

        public async Task<Actor> GetAsync(int Id)
        {
            return await _actoresRepository.GetAsync(Id);
        }

        public async Task<Actor> InsertAsync(Actor actor)
        {
            return await _actoresRepository.InsertAsync(actor);
        }

        public async Task<Actor> UpdateAsync(Actor actor)
        {
            return await _actoresRepository.UpdateAsync(actor);
        }

       public IQueryable<Actor> GetAllAsQueryable()
        {
            return _actoresRepository.GetAllAsQueryable();
        }
    }
}

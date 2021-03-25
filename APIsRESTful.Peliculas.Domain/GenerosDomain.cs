using APIsRESTful.Peliculas.Entities;
using APIsRESTful.Peliculas.IDataAccess;
using APIsRESTful.Peliculas.IDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.Domain
{
    public class GenerosDomain : IGenerosDomain
    {
        private readonly IGenerosRepository _generosRepository;

        public GenerosDomain(IGenerosRepository generosRepository)
        {
            _generosRepository = generosRepository;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            return await _generosRepository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<Genero>> GetAllAsync()
        {
            return await _generosRepository.GetAllAsync();
        }

        public async Task<Genero> GetAsync(int Id)
        {
            return await _generosRepository.GetAsync(Id);
        }

        public async Task<Genero> InsertAsync(Genero genero)
        {
            return await _generosRepository.InsertAsync(genero);
        }

        public async Task<Genero> UpdateAsync(Genero genero)
        {
            return await _generosRepository.UpdateAsync(genero);
        }
    }
}

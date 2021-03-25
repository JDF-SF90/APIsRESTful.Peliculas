using APIsRESTful.Peliculas.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.IDataAccess
{
    public interface IGenerosRepository
    {

        Task<IEnumerable<Genero>> GetAllAsync();

        Task<Genero> GetAsync(int Id);

        Task<Genero> InsertAsync(Genero genero);

        Task<Genero> UpdateAsync(Genero genero);

        Task<bool> DeleteAsync(int Id);
    }
}

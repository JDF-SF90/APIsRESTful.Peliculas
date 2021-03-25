using APIsRESTful.Peliculas.DTO;
using APIsRESTful.Peliculas.Entities;
using APIsRESTful.Peliculas.IApplication;
using APIsRESTful.Peliculas.IDomain;
using APIsRESTful.Peliculas.IInfrastructure;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;

namespace APIsRESTful.Peliculas.Application
{
    public class PeliculasApplication : IPeliculasApplication
    {
        private readonly IPeliculasDomain _peliculasDomain;
        private readonly IMapper _mapper;
        private readonly string _contenedor = "peliculas";
        private readonly IAlmacenadorArhivos _almacenadorArhivos;
        private readonly ILogger<PeliculasApplication> _logger;

        double cantidadPaginas;

        public PeliculasApplication(IPeliculasDomain peliculasDomain, IMapper mapper, IAlmacenadorArhivos almacenadorArhivos,
            ILogger<PeliculasApplication> logger)
        {
            _peliculasDomain = peliculasDomain;
            _mapper = mapper;
            _almacenadorArhivos = almacenadorArhivos;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<PeliculaDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<PeliculaDTO>>();
            try
            {
                var peliculas = await _peliculasDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<PeliculaDTO>>(peliculas);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }



        public async Task<Response<PeliculaDetallesDTO>> GetAsync(int Id)
        {
            var response = new Response<PeliculaDetallesDTO>();
            try
            {
                var pelicula = await _peliculasDomain.GetAsync(Id);
                response.Data = _mapper.Map<PeliculaDetallesDTO>(pelicula);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<PeliculaDTO>> InsertAsync(PeliculaCreateDTO peliculaCreateDCreateDTO)
        {

            var response = new Response<PeliculaDTO>();
            try
            {

                var entidad = _mapper.Map<Pelicula>(peliculaCreateDCreateDTO);

                if (peliculaCreateDCreateDTO.Poster != null)
                {

                    using (var memoryStream = new MemoryStream())
                    {
                        await peliculaCreateDCreateDTO.Poster.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(peliculaCreateDCreateDTO.Poster.FileName);
                        entidad.Poster = await _almacenadorArhivos.GuardarArchivo(contenido, extension, _contenedor, peliculaCreateDCreateDTO.Poster.ContentType);
                    }
                }




                entidad = await _peliculasDomain.InsertAsync(entidad);
                response.Data = _mapper.Map<PeliculaDTO>(entidad);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!";
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<Response<PeliculaDTO>> UpdateAsync(int Id, PeliculaCreateDTO peliculaCreateCreateDTO)
        {

            var response = new Response<PeliculaDTO>();
            try
            {

                var peliculaDB = await _peliculasDomain.ExistAsync(Id);

                if (peliculaDB != null)
                {

                    peliculaDB = _mapper.Map(peliculaCreateCreateDTO, peliculaDB);

                    if (peliculaCreateCreateDTO.Poster != null)
                    {

                        using (var memoryStream = new MemoryStream())
                        {
                            await peliculaCreateCreateDTO.Poster.CopyToAsync(memoryStream);
                            var contenido = memoryStream.ToArray();
                            var extension = Path.GetExtension(peliculaCreateCreateDTO.Poster.FileName);
                            peliculaDB.Poster = await _almacenadorArhivos.EditarArchivo(contenido, extension, _contenedor, peliculaDB.Poster, peliculaCreateCreateDTO.Poster.ContentType);
                        }
                    }


                    peliculaDB = await _peliculasDomain.UpdateAsync(peliculaDB);
                    response.Data = _mapper.Map<PeliculaDTO>(peliculaDB);
                    if (response.Data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Registro Actualizado!!!";
                    }
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Registro no existe!!!";

                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<Response<bool>> DeleteAsync(int Id)
        {

            var response = new Response<bool>();
            try
            {
                var peliculaDB = await _peliculasDomain.ExistAsync(Id);

                await _almacenadorArhivos.BorrarArchivo(peliculaDB.Poster, _contenedor);

                response.Data = await _peliculasDomain.DeleteAsync(Id);
                if (response.Data)
                {

                    response.IsSuccess = true;
                    response.Message = "Registro Eliminado!!!";

                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<Response<PeliculaDTO>> PatchAsync(int Id, JsonPatchDocument<PeliculaPatchDTO> patchDocument)
        {
            var response = new Response<PeliculaDTO>();
            try
            {
                var peliculaDB = await _peliculasDomain.ExistAsync(Id);

                if (peliculaDB != null)
                {

                    var entidadDTO = _mapper.Map<PeliculaPatchDTO>(peliculaDB);

                    _mapper.Map(entidadDTO, peliculaDB);

                    peliculaDB = await _peliculasDomain.UpdateAsync(peliculaDB);
                    response.Data = _mapper.Map<PeliculaDTO>(peliculaDB);
                    if (response.Data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Registro Actualizado!!!";
                    }


                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Registro no existe!!!";

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<PeliculaDTO>>> GetAllPaginado(PaginacionDTO paginacionDTO)
        {
            //return _actoresDomain.GetAllAsQueryable();
            var response = new Response<List<PeliculaDTO>>();


            var queryable = _peliculasDomain.GetAllAsQueryable();
            ParametersPagination(queryable.Count(), paginacionDTO.CantidadRegistrosPorPagina);
            var entities = queryable.Skip((paginacionDTO.Pagina - 1) * paginacionDTO.CantidadRegistrosPorPagina)
                .Take(paginacionDTO.CantidadRegistrosPorPagina);

            response.Data = _mapper.Map<List<PeliculaDTO>>(entities.ToList());
            response.cantidadPaginas = cantidadPaginas;

            return response;
        }

        private void ParametersPagination(int count, double CantidadRegistrosPagina)
        {
            cantidadPaginas = Math.Ceiling(count / CantidadRegistrosPagina);
        }

        public async Task<Response<List<PeliculaDTO>>> Filtro(FiltroPeliculasDTO filtroPeliculasDTO)
        {
            var response = new Response<List<PeliculaDTO>>();


            var queryable = _peliculasDomain.GetAllAsQueryable();

            if (!string.IsNullOrEmpty(filtroPeliculasDTO.Titulo))
            {
                queryable = queryable.Where(x => x.Titulo.Contains(filtroPeliculasDTO.Titulo));
            }

            if (filtroPeliculasDTO.EnCines)
            {
                queryable = queryable.Where(x => x.EnCines);
            }

            if (filtroPeliculasDTO.ProximosEstrenos)
            {
                var hoy = DateTime.Today;
                queryable = queryable.Where(x => x.FechaEstreno > hoy);
            }

            if (filtroPeliculasDTO.GeneroId != 0)
            {
                queryable = queryable.Where(x => x.PeliculasGeneros.Select(y => y.GeneroId)
                .Contains(filtroPeliculasDTO.GeneroId));
            }

            try
            {

                if (!string.IsNullOrEmpty(filtroPeliculasDTO.CampoOrdenar))
                {
                    var tipoOrden = filtroPeliculasDTO.OrdenAscendente ? "ascending" : "descending";
                    queryable = queryable.OrderBy($"{ filtroPeliculasDTO.CampoOrdenar } {tipoOrden}");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            ParametersPagination(queryable.Count(), filtroPeliculasDTO.CantidadRegistrosPorPagina);
            var entities = queryable.Skip((filtroPeliculasDTO.Pagina - 1) * filtroPeliculasDTO.CantidadRegistrosPorPagina)
                .Take(filtroPeliculasDTO.CantidadRegistrosPorPagina);

            response.Data = _mapper.Map<List<PeliculaDTO>>(entities.ToList());
            response.cantidadPaginas = cantidadPaginas;
            response.IsSuccess = true;


            return response;
        }
    }
}

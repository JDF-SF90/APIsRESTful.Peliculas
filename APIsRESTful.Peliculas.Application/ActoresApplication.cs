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
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Web.Http.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APIsRESTful.Peliculas.Application
{
    public class ActoresApplication : IActoresApplication
    {
        private readonly IActoresDomain _actoresDomain;
        private readonly IMapper _mapper;
        private readonly string _contenedor = "actores";
        private readonly IAlmacenadorArhivos _almacenadorArhivos;

        double cantidadPaginas;

        public ActoresApplication(IActoresDomain actoresDomain, IMapper mapper, IAlmacenadorArhivos almacenadorArhivos)
        {
            _actoresDomain = actoresDomain;
            _mapper = mapper;
            _almacenadorArhivos = almacenadorArhivos;
        }

        public async Task<Response<IEnumerable<ActorDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<ActorDTO>>();
            try
            {
                var actores = await _actoresDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<ActorDTO>>(actores);
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

       

        public async Task<Response<ActorDTO>> GetAsync(int Id)
        {
            var response = new Response<ActorDTO>();
            try
            {
                var actor = await _actoresDomain.GetAsync(Id);
                response.Data = _mapper.Map<ActorDTO>(actor);
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

        public async Task<Response<ActorDTO>> InsertAsync(ActorCreateDTO actorCreateDCreateDTO)
        {

            var response = new Response<ActorDTO>();
            try
            {

                var entidad = _mapper.Map<Actor>(actorCreateDCreateDTO);

                if (actorCreateDCreateDTO.Foto != null)
                {

                    using (var memoryStream = new MemoryStream())
                    {
                        await actorCreateDCreateDTO.Foto.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(actorCreateDCreateDTO.Foto.FileName);
                        entidad.Foto = await _almacenadorArhivos.GuardarArchivo(contenido, extension, _contenedor, actorCreateDCreateDTO.Foto.ContentType);
                    }
                }




                entidad = await _actoresDomain.InsertAsync(entidad);
                response.Data = _mapper.Map<ActorDTO>(entidad);
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

        public async Task<Response<ActorDTO>> UpdateAsync(int Id, ActorCreateDTO actorCreateCreateDTO)
        {

            var response = new Response<ActorDTO>();
            try
            {

                var actorDB = await _actoresDomain.ExistAsync(Id);

                if (actorDB != null)
                {

                    actorDB = _mapper.Map(actorCreateCreateDTO, actorDB);

                    if (actorCreateCreateDTO.Foto != null)
                    {

                        using (var memoryStream = new MemoryStream())
                        {
                            await actorCreateCreateDTO.Foto.CopyToAsync(memoryStream);
                            var contenido = memoryStream.ToArray();
                            var extension = Path.GetExtension(actorCreateCreateDTO.Foto.FileName);
                            actorDB.Foto = await _almacenadorArhivos.EditarArchivo(contenido, extension, _contenedor, actorDB.Foto, actorCreateCreateDTO.Foto.ContentType);
                        }
                    }


                    actorDB = await _actoresDomain.UpdateAsync(actorDB);
                    response.Data = _mapper.Map<ActorDTO>(actorDB);
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
                var actorDB = await _actoresDomain.ExistAsync(Id);

                await _almacenadorArhivos.BorrarArchivo(actorDB.Foto, _contenedor);

                response.Data = await _actoresDomain.DeleteAsync(Id);
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

        public async Task<Response<ActorDTO>> PatchAsync(int Id, JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            var response = new Response<ActorDTO>();
            try
            {
                var actorDB = await _actoresDomain.ExistAsync(Id);

                if (actorDB != null)
                {

                    var entidadDTO = _mapper.Map<ActorPatchDTO>(actorDB);

                    _mapper.Map(entidadDTO, actorDB);

                    actorDB = await _actoresDomain.UpdateAsync(actorDB);
                    response.Data = _mapper.Map<ActorDTO>(actorDB);
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

        public async Task<Response<List<ActorDTO>>> GetAllPaginado(PaginacionDTO paginacionDTO)
        {
            //return _actoresDomain.GetAllAsQueryable();
            var response = new Response<List<ActorDTO>>();


            var queryable = _actoresDomain.GetAllAsQueryable();
            ParametersPagination(queryable.Count(), paginacionDTO.CantidadRegistrosPorPagina);
            var entities = queryable.Skip((paginacionDTO.Pagina - 1) * paginacionDTO.CantidadRegistrosPorPagina)
                .Take(paginacionDTO.CantidadRegistrosPorPagina);

            response.Data = _mapper.Map<List<ActorDTO>>(entities.ToList());
            response.cantidadPaginas = cantidadPaginas;

            return response;
        }

        private void ParametersPagination(int count, double CantidadRegistrosPagina)
        {
            cantidadPaginas = Math.Ceiling(count / CantidadRegistrosPagina);
        }

       
    }
}

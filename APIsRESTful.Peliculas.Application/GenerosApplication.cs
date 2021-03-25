using APIsRESTful.Peliculas.DTO;
using APIsRESTful.Peliculas.Entities;
using APIsRESTful.Peliculas.IApplication;
using APIsRESTful.Peliculas.IDomain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.Application
{
    public class GenerosApplication : IGenerosApplication
    {
        private readonly IGenerosDomain _generosDomain;
        private readonly IMapper _mapper;

        public GenerosApplication(IGenerosDomain generosDomain, IMapper mapper)
        {
            _generosDomain = generosDomain;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GeneroDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<GeneroDTO>>();
            try
            {
                var generos = await _generosDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<GeneroDTO>>(generos);
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

        public async Task<Response<GeneroDTO>> GetAsync(int Id)
        {
            var response = new Response<GeneroDTO>();
            try
            {
                var genero = await _generosDomain.GetAsync(Id);
                response.Data = _mapper.Map<GeneroDTO>(genero);
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

        public async Task<Response<GeneroDTO>> InsertAsync(GeneroCreateDTO generoCreateDCreateDTO)
        {

            var response = new Response<GeneroDTO>();
            try
            {

                var entidad = _mapper.Map<Genero>(generoCreateDCreateDTO);
                entidad = await _generosDomain.InsertAsync(entidad);
                response.Data = _mapper.Map<GeneroDTO>(entidad);
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

        public async Task<Response<GeneroDTO>> UpdateAsync(int Id, GeneroCreateDTO generoCreateDCreateDTO)
        {

            var response = new Response<GeneroDTO>();
            try
            {

                var entidad = _mapper.Map<Genero>(generoCreateDCreateDTO);
                entidad.Id = Id;
                entidad = await _generosDomain.UpdateAsync(entidad);
                response.Data = _mapper.Map<GeneroDTO>(entidad);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Actualizado!!!";
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

                response.Data = await _generosDomain.DeleteAsync(Id);
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

    }
}


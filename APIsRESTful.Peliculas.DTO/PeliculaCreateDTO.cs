using APIsRESTful.Peliculas.DTO.TypeBinder;
using APIsRESTful.Peliculas.DTO.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.DTO
{
    public class PeliculaCreateDTO
    {
       
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }

        public bool EnCines { get; set; }

        public DateTime FechaEstreno { get; set; }


        [PesoArchivoValidacion(PesoMaximoenMegaBytes:4)]
        [TipoArchivoValidacion(Enum.GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPeliculasCreateDTO>>))]
        public List<ActorPeliculasCreateDTO> Actores { get; set; }

    }
}

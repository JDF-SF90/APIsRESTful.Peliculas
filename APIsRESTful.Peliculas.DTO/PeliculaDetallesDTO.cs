using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.DTO
{
    public class PeliculaDetallesDTO : PeliculaDTO
    {

        public List<GeneroDTO> Generos { get; set; }

        public List<ActorPeliculaDetalleDTO> Actores { get; set; }

    }
}

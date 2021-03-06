using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIsRESTful.Peliculas.Entities
{
    public class Genero
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }

        public List<PeliculasGeneros> PeliculasGeneros { get; set; }

    }
}

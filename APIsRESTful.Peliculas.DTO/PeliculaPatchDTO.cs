using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.DTO
{
    public class PeliculaPatchDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }

        public bool EnCines { get; set; }

        public DateTime FechaEstreno { get; set; }

    }
}

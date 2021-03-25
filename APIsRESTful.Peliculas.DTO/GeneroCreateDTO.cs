using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APIsRESTful.Peliculas.DTO
{
    public class GeneroCreateDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}

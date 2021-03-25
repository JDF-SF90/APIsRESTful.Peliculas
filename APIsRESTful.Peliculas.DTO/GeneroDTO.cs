﻿using System;
using System.ComponentModel.DataAnnotations;

namespace APIsRESTful.Peliculas.DTO
{
    public class GeneroDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}

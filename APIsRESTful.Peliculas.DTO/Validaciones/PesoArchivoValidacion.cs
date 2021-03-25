using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APIsRESTful.Peliculas.DTO.Validaciones
{
    public class PesoArchivoValidacion : ValidationAttribute
    {
        private readonly int pesoMaximoenMegaBytes;

        public PesoArchivoValidacion(int PesoMaximoenMegaBytes)
        {
            pesoMaximoenMegaBytes = PesoMaximoenMegaBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }


            if (formFile.Length > pesoMaximoenMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {pesoMaximoenMegaBytes}mb");
            }

            return ValidationResult.Success;

        }

    }
}


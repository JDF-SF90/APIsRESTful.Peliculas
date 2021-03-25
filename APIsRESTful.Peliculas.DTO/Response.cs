using System;
using System.Collections.Generic;
using System.Text;

namespace APIsRESTful.Peliculas.DTO
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public double cantidadPaginas { get; set; }
    }
}

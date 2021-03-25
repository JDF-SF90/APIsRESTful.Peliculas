using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.IInfrastructure
{
    public interface IAlmacenadorArhivos
    {
        Task BorrarArchivo(string ruta, string contenedor);

        Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType);

        Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType);

    }
}

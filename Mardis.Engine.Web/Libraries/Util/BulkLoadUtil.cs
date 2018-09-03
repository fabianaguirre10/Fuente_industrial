using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Mardis.Engine.Web.Util
{
    /// <summary>
    /// Util de Carga de archivos
    /// </summary>
    public class BulkLoadUtil
    {

        /// <summary>
        /// Validar contenido de archivo
        /// </summary>
        /// <param name="file"></param>
        /// <param name="separator"></param>
        /// <param name="columnsNumber"></param>
        /// <returns></returns>
        public static string ValidateContentBulkFile(IFormFile file,
                                                     string separator, int columnsNumber,
                                                     out byte[] buffer)
        {
            buffer = new byte[0];
            var result = string.Empty;
            var counter = 1;
            StreamReader fileContent = null;
            var line = string.Empty;


            using (var stream = file.OpenReadStream())
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
            }

            fileContent = new StreamReader(new MemoryStream(buffer));

            while ((line = fileContent.ReadLine()) != null)
            {
                var values = line.Split(Convert.ToChar(separator));

                // valido que el numero de campos sea el correcto
                if (values.Length != columnsNumber)
                {
                    result = "No contiene el número exacto de columnas en la fila " + counter + ". Revise el archivo";
                    break;
                }


                counter++;
            }

            return result;
        }

       

    }
}

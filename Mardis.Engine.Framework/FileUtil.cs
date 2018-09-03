using System;
using System.IO;
using System.Linq;

namespace Mardis.Engine.Framework
{
    public class FileUtil
    {
        public static int NumberFiles(byte[] bufferFile)
        {
            var fileContent = new StreamReader(new MemoryStream(bufferFile));
            int numberFiles;

            try
            {
                var file = fileContent.ReadToEnd(); // big string
                var lines = file.Split('\n');           // big array

                numberFiles = lines.Count();
            }
            catch (Exception)
            {

                numberFiles = 0;
            }


            return numberFiles;
        }
    }
}

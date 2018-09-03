using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Mardis.Engine.Web.Libraries.Util
{
    public class BranchImagesUtil
    {

        public static void UploadFilesToAzure(string branch, IFormFileCollection files)
        {
            foreach (var file in files)
            {
                var name = Guid.NewGuid().ToString();

                byte[] bytes;
                using (var stream = file.OpenReadStream())
                {
                    bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                }

                var fileStream = (new MemoryStream(bytes));

                AzureStorageUtil.UploadFromStream(fileStream, CBranch.ImagesContainer,
                    name).Wait();
            }
        }

    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Mardis.Engine.Framework;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Mardis.Engine.Web.Libraries.Util
{
    public class AzureStorageUtil
    {
        public static async Task<string> GetBase64FileFromBlob(string containerName, string fileName)
        {
            var client = GetCloudStorageAccount().CreateCloudBlobClient();
            var cloudContainer = client.GetContainerReference(containerName);

            if (!cloudContainer.Exists())
            {
                throw new ExceptionMardis("No se encontró el contenedor " + containerName);
            }

            var blockBlob = cloudContainer.GetBlockBlobReference(fileName);

            if (!blockBlob.Exists())
            {
                throw new ExceptionMardis("No se encontró el archivo " + fileName + " en el contenedor " + containerName);
            }

            return await DownloadTextAsync(blockBlob);
        }

        private static CloudStorageAccount GetCloudStorageAccount()
        {
            var connString = Startup.Configuration["ConnectionStrings:AzureConnection"];
            return CloudStorageAccount.Parse(connString);
        }

        private static async Task<string> DownloadTextAsync(CloudBlockBlob blob)
        {
            using (Stream memoryStream = new MemoryStream())
            {
                IAsyncResult asyncResult = blob.BeginDownloadToStream(memoryStream, null, null);
                await Task.Factory.FromAsync(asyncResult, (r) => { blob.EndDownloadToStream(r); });
                memoryStream.Position = 0;
                byte[] biteArray = new byte[memoryStream.Length];
                memoryStream.Read(biteArray, 0, (int)memoryStream.Length);

                return System.Convert.ToBase64String(biteArray);
            }
        }

        public static async Task DownloadBlobToTempFile(string containerName, string fileName)
        {
            var client = GetCloudStorageAccount().CreateCloudBlobClient();
            var cloudContainer = client.GetContainerReference(containerName);

            if (!cloudContainer.Exists())
            {
                throw new ExceptionMardis("No se encontró el contenedor " + containerName);
            }

            var blockBlob = cloudContainer.GetBlockBlobReference(fileName);

            if (!blockBlob.Exists())
            {
                throw new ExceptionMardis("No se encontró el archivo " + fileName + " en el contenedor " + containerName);
            }

            using (Stream memoryStream = new MemoryStream())
            {
                IAsyncResult asyncResult = blockBlob.BeginDownloadToStream(memoryStream, null, null);
                await Task.Factory.FromAsync(asyncResult, (r) => { blockBlob.EndDownloadToStream(r); });
                memoryStream.Position = 0;
                byte[] biteArray = new byte[memoryStream.Length];
                memoryStream.Read(biteArray, 0, (int)memoryStream.Length);

                File.WriteAllBytes(Path.GetTempPath() + fileName, biteArray);
            }

        }

        public async void UploadFromPath(string pathFile, string containerName, string fileName)
        {
            var client = GetCloudStorageAccount().CreateCloudBlobClient();
            var cloudContainer = client.GetContainerReference(containerName);
            cloudContainer.CreateIfNotExists();
            await cloudContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            var blockBlob = cloudContainer.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(new FileStream(pathFile, FileMode.Open, FileAccess.Read));
        }

        public static async Task UploadFromStream(MemoryStream stream, string containerName, string fileName)
        {
            var client = GetCloudStorageAccount().CreateCloudBlobClient();
            var cloudContainer = client.GetContainerReference(containerName);
            cloudContainer.CreateIfNotExists();
            await cloudContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            var blockBlob = cloudContainer.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(stream);
        }

        public static void DeleteBlob(string containerName, string fileName)
        {
            var client = GetCloudStorageAccount().CreateCloudBlobClient();
            var cloudContainer = client.GetContainerReference(containerName);
            var blockBlob = cloudContainer.GetBlockBlobReference(fileName);
            blockBlob.Delete();
        }

        public static string GetUriFromBlob(string containerName, string fileName)
        {
            var client = GetCloudStorageAccount().CreateCloudBlobClient();
            var cloudContainer = client.GetContainerReference(containerName);
            var blockBlob = cloudContainer.GetBlockBlobReference(fileName);
            return blockBlob.Uri.AbsoluteUri;
        }
    }
}

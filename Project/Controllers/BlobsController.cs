using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Project.Controllers
{
    public class BlobsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private CloudBlobContainer GetCloudBlobContainer()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot Configuration = builder.Build();

            // Parse the connection string and return a reference to the storage account.
            var BlobConnectionString = Configuration.GetSection("ConnectionStrings:AzureStorageConnectionString-1");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(BlobConnectionString.Value);
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to the container.
            CloudBlobContainer container = blobClient.GetContainerReference("blobcontainer");
            return container;
        }
        
        public ActionResult CreateBlobContainer()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            ViewBag.Success = container.CreateIfNotExistsAsync().Result;
            ViewBag.BlobContainerName = container.Name;

            return View();
        }

        public string UploadBlob()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference("video.mp4");
            using (var fileStream = System.IO.File.OpenRead(@"C:\Users\WWStudent\Desktop\video.mp4"))
            {
                blob.UploadFromStreamAsync(fileStream).Wait();
            }
            return "success!";
        }

        public ActionResult ListBlobs()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            // The CloudBlobContainer.ListBlobsSegmentedAsync method returns a BlobResultSegment. 
            // This contains IListBlobItem objects that can be cast to Block, Page, or Directory objects.
            BlobResultSegment resultSegment = container.ListBlobsSegmentedAsync(null).Result;
            foreach (IListBlobItem item in resultSegment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob blob = (CloudPageBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory dir = (CloudBlobDirectory)item;
                    blobs.Add(dir.Uri.ToString());
                }
            }

            return View(blobs);
        }

        public string DownloadBlob()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference("video.mp4");
            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\WWStudent\Desktop\downloadedBlob.mp4"))
            {
                blob.DownloadToStreamAsync(fileStream).Wait();
            }
            return "success!";
        }

        public string DeleteBlob()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference("video.mp4");
            blob.DeleteAsync().Wait();
            return "success!";
        }
    }
}
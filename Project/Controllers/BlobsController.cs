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
using System.Linq;
using Project.Models;
using Microsoft.AspNetCore.Identity;
using Project.Data;

namespace Project.Controllers
{
    public class BlobsController : Controller
    {
        // JH- Created Dependancy injection with overridden constructor

        private readonly ApplicationDataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BlobsController(UserManager<IdentityUser> UserManager, ApplicationDataContext Context)
        {
            _context = Context;
            _userManager = UserManager;
        }

        private string currentUser { get { return _userManager.GetUserId(User); } }

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

        private ActionResult CreateBlobContainer()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            ViewBag.Success = container.CreateIfNotExistsAsync().Result;
            ViewBag.BlobContainerName = container.Name;

            return View();
        }

        [HttpGet]
        public IActionResult UploadBlob()
        {
            VideoModel VM = new VideoModel();

            return View(VM);
        }

        [HttpPost]
        public async Task<IActionResult> UploadBlob([Bind("Title, VideoLink, Title, Description, DatePosted")]VideoModel VM)
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference(VM.Title);
            // Create the blob in blob storage
            var path = $@"{VM.VideoLink}";

            var req = System.Net.WebRequest.Create(VM.VideoLink);
            using (Stream stream = req.GetResponse().GetResponseStream())
            {
                blob.UploadFromStreamAsync(stream).Wait();
            }

            var Wd = Directory.GetCurrentDirectory();
           
           /* using (var fileStream = System.IO.File.OpenRead(path))
            {
                blob.UploadFromStreamAsync(fileStream).Wait();
            }*/

            VM.Owner = currentUser;
            // Add video meta data to the SQL DB
            if (ModelState.IsValid)
            {
                _context.Add(VM);
                await _context.SaveChangesAsync();

                return View("Home/Index");
            }

            // if we got this far something went wrong
            return View();

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

        /*
        // This function is built into the HTML video tag.
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
        */

        public string DeleteBlob()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference("video.mp4");
            blob.DeleteAsync().Wait();
            return "success!";
        }
    }
}
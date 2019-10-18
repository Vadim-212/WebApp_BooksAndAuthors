using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Configuration;

namespace WebApplication1.Controllers
{
    public class AzureController : Controller
    {
        public string UploadFile()
        {
            string blobStorage = ConfigurationManager.ConnectionStrings["BlobStorageConnectionString"].ConnectionString; /*System.Configuration.ConfigurationManager.AppSettings.Get("BlobStorage");*/
            
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blobStorage);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("asharina67vadimblobstorage");
            blobContainer.CreateIfNotExists();
            //CloudBlockBlob blob = blobContainer.GetBlockBlobReference("asharina67vadimblobstorage");
            blobContainer.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference("Images.png");
            using (var file = System.IO.File.OpenRead(@"C:\Test\img.png"))
            {
                //string name = file.Name;
                //string content = 
                blob.UploadFromStream(file);
            }
            return "";
        }
        // GET: Azure
        public ActionResult Index()
        {
            UploadFile();
            return View();
        }
    }
}

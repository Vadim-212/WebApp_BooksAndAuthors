using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WebApplication1.Controllers
{
    public class AzureController : Controller
    {
        static string blobStorage = System.Configuration.ConfigurationManager.AppSettings.Get("BlobStorage");
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blobStorage);

        static CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        static CloudBlobContainer blobContainer = blobClient.GetContainerReference("asharina67vadimblobstorage");
        
        // GET: Azure
        public ActionResult Index()
        {
            return View();
        }
    }
}
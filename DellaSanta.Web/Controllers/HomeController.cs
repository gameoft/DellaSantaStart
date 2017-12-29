using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DellaSanta.Layer;
using DellaSanta.Web.Models;
using  System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DellaSanta.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _applicationDbContext;

        public HomeController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public ActionResult Index()
        {
            var uploadedFiles = _applicationDbContext.UploadedFiles.ToList();
            return View("Index", uploadedFiles);
        }

        public ActionResult Process()
        {
            ViewBag.Message = "Your application description page.";

            //DocxParser.Parse(fileBytes, Convert.ToInt32(file.ContentLength));

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        // Upload post method
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Upload(FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    var path = Server.MapPath("~/uploads");
                    var pathToCheck = Path.Combine(path, file.FileName);

                    // Create a temporary file name to use for checking duplicates.
                    string tempfileName = "";

                    // Check to see if a file already exists with the
                    // same name as the file to upload.        
                    if (System.IO.File.Exists(pathToCheck))
                    {
                        int counter = 2;
                        while (System.IO.File.Exists(pathToCheck))
                        {
                            // if a file with this name already exists,
                            // prefix the filename with a number.
                            tempfileName = counter.ToString() + fileName;
                            pathToCheck = Path.Combine(path, tempfileName); 
                            counter++;
                        }

                        fileName = tempfileName;
                    }

                    // Save the file.
                    pathToCheck = Path.Combine(path, fileName);
                    
                    try
                    {
                        file.SaveAs(pathToCheck);
                        _applicationDbContext.UploadedFiles.Add(new Core.UploadedFiles { Name = file.FileName, NameOnDisk = fileName });
                        _applicationDbContext.SaveChanges();
                        return View("UploadConfirmed");
                    }
                    catch(Exception ex)
                    {
                        return View("Index");
                    }
                    

                }
            }
            return View("Index");
        }

    }
}
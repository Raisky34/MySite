using MySite.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Task()
        {

            return View();
        }


        #region CreateTask

        private IEnumerable<Image> GetImageCollection()
        {
            //throw new NotImplementedException();
            return (from image in imageDataBase.Images
                    where image.User == User.Identity.Name
                    select image);
        }

        private ImageContext imageDataBase = new ImageContext();

        [HttpPost]
        [Authorize]
        public ActionResult Task(HttpPostedFileBase fileUpload, HttpPostAttribute selboton)
        {
            if (fileUpload == null)
            {
                return View(GetImageCollection());
            }

            var cloudinary = new Cloudinary(
            new Account(
            "raisky",
            "712813241936831",
            "Z9cprR-2l0R51ehGj5PIsUr2d3I"));

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileUpload.FileName, fileUpload.InputStream),
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            var uplPath = uploadResult.Uri;

            Image uploadedImage = new Image();
            uploadedImage.Path = uplPath.AbsoluteUri;
            uploadedImage.User = User.Identity.Name;
            uploadedImage.PublicId = uploadResult.PublicId;

            imageDataBase.Entry(uploadedImage).State = System.Data.Entity.EntityState.Added;
            imageDataBase.SaveChanges();

            return View(GetImageCollection());
            
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteImage(int id)
        {
            Image imageToDelete = imageDataBase.Images.Find(id);
            DelResParams deleteParams = new DelResParams()
            {
                PublicIds = new System.Collections.Generic.List<String>() { imageToDelete.PublicId },
                Invalidate = true
            };
            Cloudinary cloudinary = new Cloudinary(new Account(
            "raisky",
            "712813241936831",
            "Z9cprR-2l0R51ehGj5PIsUr2d3I"));

            cloudinary.DeleteResources(deleteParams);
            imageDataBase.Entry(imageToDelete).State = System.Data.Entity.EntityState.Deleted;
            imageDataBase.SaveChanges();
            return RedirectToAction("Task");
        }

        public System.IO.Stream InputStream { get; set; }

        #endregion CreateTask
    
    }
}

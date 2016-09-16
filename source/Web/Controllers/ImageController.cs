using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PiCam.Web.Models;

namespace PiCam.Web.Controllers
{
    public class ImageController : Controller
    {
        private ImageCache _imageCache;

        public ImageController(ImageCache imageCache)
        {
            _imageCache = imageCache;
        }
        
        public ActionResult Index(int cacheBusterId)
        {
            if (_imageCache.ImageJpeg == null)
            {
                return new EmptyResult();
            }
            return File(_imageCache.ImageJpeg, "image/jpg");
        }
    }
}
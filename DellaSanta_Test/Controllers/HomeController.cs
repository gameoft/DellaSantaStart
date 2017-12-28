using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DellaSanta.Layer;

namespace DellaSantaStart.Controllers
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
            var coursePaths = _applicationDbContext.CoursePaths.ToList();
            return View("Index", coursePaths);
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
    }
}
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class LogController : Controller
    {
        LibrariyEntities context = new LibrariyEntities();
        public ActionResult Index()
        {
            var data = context.log.ToList();
            return View(data);
        }
    }
}
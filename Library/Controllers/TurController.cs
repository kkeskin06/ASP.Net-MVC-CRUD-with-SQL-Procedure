using Library.Models;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class TurController : Controller
    {
        LibrariyEntities context = new LibrariyEntities();


        public ActionResult Index()
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@turid",1),
                new SqlParameter("@turadi","ad"),
                new SqlParameter("@Type", "Select"),
            };

            var result = context.Database.SqlQuery<tur>("dbo.turprocedure @turid,@turadi,@Type", sqlParameters).ToList();
            return View(result);
        }

        public ActionResult Create()
        {
            var model = new TurViewModel();

            model.turlist = context.tur.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(tur tur)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@turid",DBNull.Value),
                new SqlParameter("@turadi",tur.tur_adi),
                new SqlParameter("@Type", "Insert"),
            };

            var result = context.Database.SqlQuery<tur>("dbo.turprocedure @turid,@turadi,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@turid",id),
                new SqlParameter("@turadi","ad"),
                new SqlParameter("@Type", "Delete"),
            };

            context.Database.SqlQuery<tur>("dbo.turprocedure @turid,@turadi,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SqlParameter[] sqlParameters = {
                new SqlParameter("@turid",id),
                new SqlParameter("@turadi","ad"),
                new SqlParameter("@Type", "Find"),
            };

            var data = context.Database.SqlQuery<tur>("dbo.turprocedure @turid,@turadi,@Type", sqlParameters).ToList();
            var model = new TurViewModel();
            model.turlist = data;
            ViewBag.tur_id = id;
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(tur tur)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@turid",tur.tur_id),
                new SqlParameter("@turadi",tur.tur_adi),
                new SqlParameter("@Type", "Update"),
            };
            context.Database.SqlQuery<tur>("dbo.turprocedure @turid,@turadi,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }
    }
}
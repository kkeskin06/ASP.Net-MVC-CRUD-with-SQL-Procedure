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
    public class YazarsairController : Controller
    {
        LibrariyEntities context = new LibrariyEntities();
        public ActionResult Index()
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@yazarsairid",1),
                new SqlParameter("@yazarsairadi","ad"),
                new SqlParameter("@yazarsairsoyadi","ad"),
                new SqlParameter("@Type", "Select"),
            };

            var result = context.Database.SqlQuery<yazarvesair>("dbo.yazarvesairprocedure @yazarsairid,@yazarsairadi,@yazarsairsoyadi,@Type", sqlParameters).ToList();
            return View(result);
        }

        public ActionResult Create()
        {
            var model = new YazarsairViewModel();

            model.yazarsairlist = context.yazarvesair.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(yazarvesair yazarvesair)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@yazarsairid",DBNull.Value),
                new SqlParameter("@yazarsairadi",yazarvesair.yazar_sair_adi),
                new SqlParameter("@yazarsairsoyadi",yazarvesair.yazar_sair_soyadi),
                new SqlParameter("@Type", "Insert"),
            };

            context.Database.SqlQuery<yazarvesair>("dbo.yazarvesairprocedure @yazarsairid,@yazarsairadi,@yazarsairsoyadi,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@yazarsairid",id),
                new SqlParameter("@yazarsairadi","ad"),
                new SqlParameter("@yazarsairsoyadi","ad"),
                new SqlParameter("@Type", "Delete"),
            };

            context.Database.SqlQuery<yazarvesair>("dbo.yazarvesairprocedure @yazarsairid,@yazarsairadi,@yazarsairsoyadi,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SqlParameter[] sqlParameters = {
                new SqlParameter("@yazarsairid",id),
                new SqlParameter("@yazarsairadi","ad"),
                new SqlParameter("@yazarsairsoyadi","ad"),
                new SqlParameter("@Type", "Find"),
            };
            var data = context.Database.SqlQuery<yazarvesair>("dbo.yazarvesairprocedure @yazarsairid,@yazarsairadi,@yazarsairsoyadi,@Type", sqlParameters).ToList();
            var model = new YazarsairViewModel();
            model.yazarsairlist = data;
            ViewBag.yazar_sair_id = id;
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(yazarvesair yazarvesair)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@yazarsairid",yazarvesair.yazar_sair_id),
                new SqlParameter("@yazarsairadi",yazarvesair.yazar_sair_adi),
                new SqlParameter("@yazarsairsoyadi",yazarvesair.yazar_sair_soyadi),
                new SqlParameter("@Type", "Update"),
            };
            context.Database.SqlQuery<yazarvesair>("dbo.yazarvesairprocedure @yazarsairid,@yazarsairadi,@yazarsairsoyadi,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

    }
}
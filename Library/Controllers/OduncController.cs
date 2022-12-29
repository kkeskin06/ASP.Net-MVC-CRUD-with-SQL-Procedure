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
    public class OduncController : Controller
    {
        LibrariyEntities context = new LibrariyEntities();
        public ActionResult Index()
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@oduncid",1),
                new SqlParameter("@oduncalma",10),
                new SqlParameter("@oduncverme",20),
                new SqlParameter("@kullaniciid",1),
                new SqlParameter("@kitapid",2),
                new SqlParameter("@Type", "Select"),
            };
            
            var result = context.Database.SqlQuery<odunc>("dbo.oduncprocedure @oduncid,@oduncalma,@oduncverme,@kullaniciid,@kitapid,@Type", sqlParameters).ToList();
            var model = new OduncViewModel();
            model.odunclist = result;
            model.kitaplist = context.kitap.ToList();
            model.kullanicilist = context.kullanici.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new OduncViewModel();

            model.odunclist = context.odunc.ToList();
            model.kullanicilist = context.kullanici.ToList();
            model.kitaplist = context.kitap.ToList();


            return View(model);
        }

        [HttpPost]
        public ActionResult Create(odunc odunc)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@oduncid",DBNull.Value),
                new SqlParameter("@oduncalma",odunc.odunc_almatarihi),
                new SqlParameter("@oduncverme",odunc.odunc_vermetarihi),
                new SqlParameter("@kullaniciid",odunc.kullanici_id),
                new SqlParameter("@kitapid",odunc.kitap_id),
                new SqlParameter("@Type", "Insert"),
            };

            context.Database.SqlQuery<odunc>("dbo.oduncprocedure @oduncid,@oduncalma,@oduncverme,@kullaniciid,@kitapid,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@oduncid",id),
                new SqlParameter("@oduncalma",10),
                new SqlParameter("@oduncverme",10),
                new SqlParameter("@kullaniciid",1),
                new SqlParameter("@kitapid",2),
                new SqlParameter("@Type", "Delete"),
            };

            context.Database.SqlQuery<odunc>("dbo.oduncprocedure @oduncid,@oduncalma,@oduncverme,@kullaniciid,@kitapid,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SqlParameter[] sqlParameters = {
                new SqlParameter("@oduncid",id),
                new SqlParameter("@oduncalma",10),
                new SqlParameter("@oduncverme",10),
                new SqlParameter("@kullaniciid",1),
                new SqlParameter("@kitapid",2),
                new SqlParameter("@Type", "Find"),
            };

            var data = context.Database.SqlQuery<odunc>("dbo.oduncprocedure @oduncid,@oduncalma,@oduncverme,@kullaniciid,@kitapid,@Type", sqlParameters).ToList();

            var model = new OduncViewModel();

            model.odunclist = data;
            model.kitaplist = context.kitap.ToList();
            model.kullanicilist = context.kullanici.ToList();

            ViewBag.odunc_id = id;
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(odunc odunc)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@oduncid",odunc.odunc_id),
                new SqlParameter("@oduncalma",odunc.odunc_almatarihi),
                new SqlParameter("@oduncverme",odunc.odunc_vermetarihi),
                new SqlParameter("@kullaniciid",odunc.kullanici_id),
                new SqlParameter("@kitapid",odunc.kitap_id),
                new SqlParameter("@Type", "Update"),
            };

            context.Database.SqlQuery<odunc>("dbo.oduncprocedure @oduncid,@oduncalma,@oduncverme,@kullaniciid,@kitapid,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }
    }
}
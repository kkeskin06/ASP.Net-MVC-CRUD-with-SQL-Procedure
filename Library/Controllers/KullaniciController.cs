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
    public class KullaniciController : Controller
    {
        LibrariyEntities context = new LibrariyEntities();
        public ActionResult Index()
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kullaniciid",1),
                new SqlParameter("@kullaniciadi","kullaniciadi"),
                new SqlParameter("@kullanicisoyadi","kullanicisoyadi"),
                new SqlParameter("@kullanicimail","kullanicimail"),
                new SqlParameter("@kullanicisifre","kullanicisifre"),
                new SqlParameter("@kutuphaneid",1),
                new SqlParameter("@Type", "Select"),
            };

            var result = context.Database.SqlQuery<kullanici>("dbo.kullaniciprocedure @kullaniciid,@kullaniciadi,@kullanicisoyadi,@kullanicimail,@kullanicisifre,@kutuphaneid,@Type", sqlParameters).ToList();
            var model = new KullaniciViewModel();

            model.kullaniciList = result;
            model.kutuphanelist = context.kutuphane.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new KullaniciViewModel();

            model.kullaniciList = context.kullanici.ToList();
            model.kutuphanelist = context.kutuphane.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(kullanici kullanici)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kullaniciid",DBNull.Value),
                new SqlParameter("@kullaniciadi",kullanici.kullanici_adi),
                new SqlParameter("@kullanicisoyadi",kullanici.kullanici_soyadi),
                new SqlParameter("@kullanicimail",kullanici.kullanici_mail),
                new SqlParameter("@kullanicisifre",kullanici.kullanici_sifre),
                new SqlParameter("@kutuphaneid",kullanici.kutuphane_id),
                new SqlParameter("@Type", "Insert"),
            };

            context.Database.SqlQuery<kullanici>("dbo.kullaniciprocedure @kullaniciid,@kullaniciadi,@kullanicisoyadi,@kullanicimail,@kullanicisifre,@kutuphaneid,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kullaniciid",id),
                new SqlParameter("@kullaniciadi","kullaniciadi"),
                new SqlParameter("@kullanicisoyadi","kullanicisoyadi"),
                new SqlParameter("@kullanicimail","kullanicimail"),
                new SqlParameter("@kullanicisifre","kullanicisifre"),
                new SqlParameter("@kutuphaneid",1),
                new SqlParameter("@Type", "Delete"),
            };

            context.Database.SqlQuery<kullanici>("dbo.kullaniciprocedure @kullaniciid,@kullaniciadi,@kullanicisoyadi,@kullanicimail,@kullanicisifre,@kutuphaneid,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kullaniciid",id),
                new SqlParameter("@kullaniciadi","kullaniciadi"),
                new SqlParameter("@kullanicisoyadi","kullanicisoyadi"),
                new SqlParameter("@kullanicimail","kullanicimail"),
                new SqlParameter("@kullanicisifre","kullanicisifre"),
                new SqlParameter("@kutuphaneid",1),
                new SqlParameter("@Type", "Find"),
            };

            var data = context.Database.SqlQuery<kullanici>("dbo.kullaniciprocedure @kullaniciid,@kullaniciadi,@kullanicisoyadi,@kullanicimail,@kullanicisifre,@kutuphaneid,@Type", sqlParameters).ToList();

            var model = new KullaniciViewModel();

            model.kullaniciList = data;
            model.kutuphanelist = context.kutuphane.ToList();


            ViewBag.kullanici_id = id;
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(kullanici kullanici)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kullaniciid",kullanici.kullanici_id),
                new SqlParameter("@kullaniciadi",kullanici.kullanici_adi),
                new SqlParameter("@kullanicisoyadi",kullanici.kullanici_soyadi),
                new SqlParameter("@kullanicimail",kullanici.kullanici_mail),
                new SqlParameter("@kullanicisifre",kullanici.kullanici_sifre),
                new SqlParameter("@kutuphaneid",kullanici.kutuphane_id),
                new SqlParameter("@Type", "Update"),
            };

            context.Database.SqlQuery<kullanici>("dbo.kullaniciprocedure @kullaniciid,@kullaniciadi,@kullanicisoyadi,@kullanicimail,@kullanicisifre,@kutuphaneid,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

    }
}
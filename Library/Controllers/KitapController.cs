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
    public class KitapController : Controller
    {
        LibrariyEntities context = new LibrariyEntities();
        public ActionResult Index()
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kitap_id",1),
                new SqlParameter("@kitap_adi","ad"),
                new SqlParameter("@sayfasayisi","sayfa"),
                new SqlParameter("@konusu","konu"),
                new SqlParameter("@turpid",2),
                new SqlParameter("@yazarvesairid",2),
                new SqlParameter("@Type", "Select"),
            };

            var result = context.Database.SqlQuery<kitap>("dbo.kitapprocedure @kitap_id,@kitap_adi,@sayfasayisi,@konusu,@turpid,@yazarvesairid,@Type", sqlParameters).ToList();

            var model = new KitapViewModels();
            model.kitaplist = result;
            model.turlist = context.tur.ToList();
            model.yazarsairlist = context.yazarvesair.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new KitapViewModels();

            model.kitaplist = context.kitap.ToList();
            model.turlist = context.tur.ToList();
            model.yazarsairlist = context.yazarvesair.ToList();


            return View(model);
        }

        [HttpPost]
        public ActionResult Create(kitap kitap)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kitap_id",DBNull.Value),
                new SqlParameter("@kitap_adi",kitap.kitap_adi),
                new SqlParameter("@sayfasayisi",kitap.kitap_sayfasayisi),
                new SqlParameter("@konusu",kitap.kitap_konusu),
                new SqlParameter("@turpid",kitap.tur_id),
                new SqlParameter("@yazarvesairid",kitap.yazar_sair_id),
                new SqlParameter("@Type", "Insert"),
            };

            context.Database.SqlQuery<kitap>("dbo.kitapprocedure @kitap_id,@kitap_adi,@sayfasayisi,@konusu,@turpid,@yazarvesairid,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kitap_id",id),
                new SqlParameter("@kitap_adi","ad"),
                new SqlParameter("@sayfasayisi","sayfa"),
                new SqlParameter("@konusu","konu"),
                new SqlParameter("@turpid",2),
                new SqlParameter("@yazarvesairid",2),
                new SqlParameter("@Type", "Delete"),
            };

            context.Database.SqlQuery<kitap>("dbo.kitapprocedure @kitap_id,@kitap_adi,@sayfasayisi,@konusu,@turpid,@yazarvesairid,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kitap_id",id),
                new SqlParameter("@kitap_adi","ad"),
                new SqlParameter("@sayfasayisi","sayfa"),
                new SqlParameter("@konusu","konu"),
                new SqlParameter("@turpid",2),
                new SqlParameter("@yazarvesairid",2),
                new SqlParameter("@Type", "Find"),
            };

            var data = context.Database.SqlQuery<kitap>("dbo.kitapprocedure @kitap_id,@kitap_adi,@sayfasayisi,@konusu,@turpid,@yazarvesairid,@Type", sqlParameters).ToList();


            var model = new KitapViewModels();

            model.kitaplist = data;
            model.turlist = context.tur.ToList();
            model.yazarsairlist = context.yazarvesair.ToList();

            ViewBag.kitap_id = id;
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(kitap kitap)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kitap_id",kitap.kitap_id),
                new SqlParameter("@kitap_adi",kitap.kitap_adi),
                new SqlParameter("@sayfasayisi",kitap.kitap_sayfasayisi),
                new SqlParameter("@konusu",kitap.kitap_konusu),
                new SqlParameter("@turpid",kitap.tur_id),
                new SqlParameter("@yazarvesairid",kitap.yazar_sair_id),
                new SqlParameter("@Type", "Update"),
            };

            context.Database.SqlQuery<kitap>("dbo.kitapprocedure @kitap_id,@kitap_adi,@sayfasayisi,@konusu,@turpid,@yazarvesairid,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }
    }
}
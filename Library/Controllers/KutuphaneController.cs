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
    public class KutuphaneController : Controller
    {
        LibrariyEntities context = new LibrariyEntities();


        public ActionResult Index()
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kutuphaneid",1),
                new SqlParameter("@kutuphaneadi","ad"),
                new SqlParameter("@Type", "Select"),
            };

            var result = context.Database.SqlQuery<kutuphane>("dbo.kutuphaneprocedure @kutuphaneid,@kutuphaneadi,@Type", sqlParameters).ToList();
            return View(result);
        }

        public ActionResult Create()
        {
            var model = new KutuphaneViewModel();

            model.kutuphanelist = context.kutuphane.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(kutuphane kutuphane)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kutuphaneid",DBNull.Value),
                new SqlParameter("@kutuphaneadi",kutuphane.kutuphane_adi),
                new SqlParameter("@Type", "Insert"),
            };

            context.Database.SqlQuery<kutuphane>("dbo.kutuphaneprocedure @kutuphaneid,@kutuphaneadi,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kutuphaneid",id),
                new SqlParameter("@kutuphaneadi","ad"),
                new SqlParameter("@Type", "Delete"),
            };
            context.Database.SqlQuery<kutuphane>("dbo.kutuphaneprocedure @kutuphaneid,@kutuphaneadi,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kutuphaneid",id),
                new SqlParameter("@kutuphaneadi","ad"),
                new SqlParameter("@Type", "Find"),
            };

            var data = context.Database.SqlQuery<kutuphane>("dbo.kutuphaneprocedure @kutuphaneid,@kutuphaneadi,@Type", sqlParameters).ToList();
            var model = new KutuphaneViewModel();
            model.kutuphanelist = data;
            ViewBag.kutuphane_id = id;
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(kutuphane kutuphane)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@kutuphaneid",kutuphane.kutuphane_id),
                new SqlParameter("@kutuphaneadi",kutuphane.kutuphane_adi),
                new SqlParameter("@Type", "Update"),
            };
            context.Database.SqlQuery<kutuphane>("dbo.kutuphaneprocedure @kutuphaneid,@kutuphaneadi,@Type", sqlParameters).ToList();
            return RedirectToAction("Index");
        }

    }
}
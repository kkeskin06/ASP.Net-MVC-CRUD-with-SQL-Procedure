using Library.Models;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Library.Controllers
{
    public class SecurityController : Controller
    {
        LibrariyEntities context = new LibrariyEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(kullanici kullanici)
        {
            var kullanicidb = context.kullanici.FirstOrDefault(x => x.kullanici_mail == kullanici.kullanici_mail && x.kullanici_sifre == kullanici.kullanici_sifre);
            if (kullanicidb != null)
            {
                FormsAuthentication.SetAuthCookie(kullanici.kullanici_mail, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Security");
        }

        public ActionResult Register()
        {
            var model = new KullaniciViewModel();

            model.kullaniciList = context.kullanici.ToList();
            model.kutuphanelist = context.kutuphane.ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult Register(kullanici kullanici)
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
            return RedirectToAction("Login","Security");
        }
    }
}
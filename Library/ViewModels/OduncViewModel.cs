using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.ViewModels
{
    public class OduncViewModel
    {
        public List<odunc> odunclist { get; set; }

        public List<kitap> kitaplist { get; set; }

        public List<kullanici> kullanicilist { get; set; }
    }
}
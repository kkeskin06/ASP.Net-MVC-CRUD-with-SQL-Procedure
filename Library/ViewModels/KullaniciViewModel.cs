using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.ViewModels
{
    public class KullaniciViewModel
    {
        public List<kutuphane> kutuphanelist { get; set; }
        public List<kullanici> kullaniciList { get; set; }
    }
}
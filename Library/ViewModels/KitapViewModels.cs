using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.ViewModels
{
    public class KitapViewModels
    {
        public List<kitap> kitaplist { get; set; }

        public List<tur>   turlist { get; set; }

        public List<yazarvesair> yazarsairlist { get; set; }
    }
}
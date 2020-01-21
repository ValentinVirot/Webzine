using System;
using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class IndexPaysViewModel
    {
        public List<Pays> Pays { get; set; }
        public int TotalPays { get; set; }
        public int PageActuel { get; set; }
        public int LengthPage { get; set; }
        public int DisplayPage { get; set; }
        public Boolean Next { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class ManageArtistesViewModel
    {
        public ArtisteViewModel Artiste { get; set; }
        public SelectList Pays { get; set; }

    }


}

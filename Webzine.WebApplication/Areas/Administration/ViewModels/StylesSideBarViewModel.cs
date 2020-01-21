using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Webzine.Entity;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class StylesSideBarViewModel
    {
        
        public Style Style { get; set; }
        public int TotalTitres { get; set; }

    }
}

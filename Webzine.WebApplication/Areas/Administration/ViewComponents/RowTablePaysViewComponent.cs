using Microsoft.AspNetCore.Mvc;
using Webzine.Entity;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{
    public class RowTablePays : ViewComponent
    {
        public RowTablePays()
        {

        }

        public IViewComponentResult Invoke(Pays pays)
        {
            return View(pays);
        }
    }
}

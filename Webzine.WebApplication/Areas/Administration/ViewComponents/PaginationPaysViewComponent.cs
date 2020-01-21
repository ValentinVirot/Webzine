using Microsoft.AspNetCore.Mvc;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.ViewComponents
{

    public class PaginationPays : ViewComponent
    {


        public PaginationPays()
        {

        }

        public IViewComponentResult Invoke(IndexPaysViewModel pagination)
        {
            return View(pagination);
        }
    }
}

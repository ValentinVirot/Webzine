using Microsoft.AspNetCore.Mvc;
using Webzine.Repository.Contracts;

namespace Webzine.WebApplication.ViewComponents
{
    public class StylesSidebar : ViewComponent
    {
        private IStyleRepository _styleRepo;

        public StylesSidebar(IStyleRepository repo)
        {
            _styleRepo = repo;
        }

        public IViewComponentResult Invoke()
        {
            return View(_styleRepo.FindAll());
        }
    }
}

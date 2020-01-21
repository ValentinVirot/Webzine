using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Webzine.Entity;
using Webzine.Repository.Contracts;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.ViewComponents
{
    public class StylesAdmin : ViewComponent
    {
        private IStyleRepository _styleRepo;
        private ITitreRepository _titreRepository;

        public StylesAdmin(IStyleRepository repo, ITitreRepository titreRepository)
        {
            _styleRepo = repo;
            _titreRepository = titreRepository;
        }

        public IViewComponentResult Invoke()
        {
            List<StylesSideBarViewModel> styles = new List<StylesSideBarViewModel>();
            _styleRepo.FindAll().ForEach(style => styles.Add(new StylesSideBarViewModel { Style = style , TotalTitres = _titreRepository.CountByStyle(style.IdStyle) }));

            return View(styles);
        }
    }
}

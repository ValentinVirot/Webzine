using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;
using Webzine.WebApplication.Areas.Administration.ViewModels;

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class PaysController : Controller
    {

        #region Variables

        private IPaysRepository _paysRepository;
        private IConfiguration _configuration;
        private List<Pays> _pays = new List<Pays>();
        private Pays _paysAlone = new Pays();

        #endregion

        public PaysController(IPaysRepository paysRepository, IConfiguration configuration)
        {
            _paysRepository = paysRepository;
            _configuration = configuration;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);

            _pays = _paysRepository.Take(0, (_paysRepository.Count() <= lengthPage) ? _paysRepository.Count() : lengthPage).ToList();

            IndexPaysViewModel indexPaysViewModel = new IndexPaysViewModel
            {
                Pays = _pays,
                TotalPays = _paysRepository.Count(),
                PageActuel = 1,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_paysRepository.Count() - lengthPage > 0) ? true : false
            };

            return this.View(nameof(PaysController.Index), indexPaysViewModel);
        }

        [HttpGet]
        public IActionResult Navigate(int id)
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);
            int indexActuel = (id - 1) * lengthPage;

            _pays = _paysRepository.Take(indexActuel, (_paysRepository.Count() <= (id * lengthPage)) ? _paysRepository.Count() - indexActuel : lengthPage).ToList();

            IndexPaysViewModel indexPaysViewModel = new IndexPaysViewModel
            {
                Pays = _pays,
                TotalPays = _paysRepository.Count(),
                PageActuel = id,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_paysRepository.Count() - indexActuel > lengthPage) ? true : false
            };

            return this.View(nameof(PaysController.Index), indexPaysViewModel);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                return this.View(nameof(PaysController.Delete), _paysRepository?.Find(id));
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(PaysController.Index), "Pays", new { area = "Administration" });
            }
        }

        [HttpPost, ActionName(nameof(PaysController.Delete)), ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            try
            {
                _paysRepository.Delete(_paysRepository.Find(id));
                return RedirectToAction(nameof(PaysController.Index), "Pays", new { area = "Administration" });
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(PaysController.Index), "Pays", new { area = "Administration" });
            }
        }
        #endregion

        #region Manage
        [HttpGet]
        public IActionResult Manage(int? id, string fonction)
        {
            if ((fonction == "Create" && !id.HasValue) || (fonction == "Edit" && id.HasValue))
            {
                try
                {
                    if (id.HasValue)
                    {
                        _paysAlone = _paysRepository?.Find((int)id);
                       
                    }

                    return this.View(nameof(PaysController.Manage), _paysAlone);
                }
                catch (NullReferenceException e)
                {
                    return RedirectToAction(nameof(PaysController.Index), "Pays", new { area = "Administration" });
                }
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

        [HttpPost, ActionName(nameof(PaysController.Manage)), ValidateAntiForgeryToken]
        public IActionResult ManagePOST(Pays pays)
        {
            if (ModelState.IsValid)
            {
                Pays newPays = new Pays
                {
                    IdPays = (pays.IdPays != 0) ? pays.IdPays : 0,
                    Nom = pays.Nom
                };

                if (pays.IdPays != 0) { _paysRepository.Update(pays); } else { _paysRepository.Add(newPays); }

                return RedirectToAction(nameof(PaysController.Index), "Pays", new { area = "Administration" });
            }
            else
            {
                return this.View(nameof(PaysController.Manage), pays);
            }
        }
        #endregion

    }
}

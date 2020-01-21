using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ArtistesController : Controller
    {

        #region Variables

        private IArtisteRepository _artisteRepository;
        private IPaysRepository _paysRepository;
        private IConfiguration _configuration;
        private List<ArtisteViewModel> _artistes = new List<ArtisteViewModel>();
        private ArtisteViewModel _artiste;

        #endregion

        public ArtistesController(IArtisteRepository artisteRepository, IConfiguration configuration, IPaysRepository paysRepository)
        {
            _artisteRepository = artisteRepository;
            _paysRepository = paysRepository;
            _configuration = configuration;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);

            _artistes = _artisteRepository.Take(0, (_artisteRepository.Count() <= lengthPage) ? _artisteRepository.Count() : lengthPage).Select(vm => new ArtisteViewModel { IdArtiste = vm.IdArtiste, NomArtiste = vm.Nom, DateNaissance = vm.DateNaissance }).ToList();

            IndexArtistesViewModel indexArtisteViewModel = new IndexArtistesViewModel
            {
                Artistes = _artistes,
                TotalArtistes = _artisteRepository.Count(),
                PageActuel = 1,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_artisteRepository.Count() - lengthPage > 0) ? true : false
            };

            return this.View(nameof(ArtistesController.Index), indexArtisteViewModel);
        }

        [HttpGet]
        public IActionResult Navigate(int id)
        {
            int lengthPage = Convert.ToInt32(_configuration["lengthAdminPage"]);
            int displayPage = Convert.ToInt32(_configuration["displayAdminPage"]);
            int indexActuel = (id - 1) * lengthPage;

            _artistes = _artisteRepository.Take(indexActuel, (_artisteRepository.Count() <= (id * lengthPage)) ? _artisteRepository.Count() - indexActuel : lengthPage).Select(vm => new ArtisteViewModel { IdArtiste = vm.IdArtiste, NomArtiste = vm.Nom, DateNaissance = vm.DateNaissance }).ToList();

            IndexArtistesViewModel indexArtisteViewModel = new IndexArtistesViewModel
            {
                Artistes = _artistes,
                TotalArtistes = _artisteRepository.Count(),
                PageActuel = id,
                LengthPage = lengthPage,
                DisplayPage = displayPage,
                Next = (_artisteRepository.Count() - indexActuel > lengthPage) ? true : false
            };

            return this.View(nameof(ArtistesController.Index), indexArtisteViewModel);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Artiste artiste = _artisteRepository?.Find(id);
                _artiste = new ArtisteViewModel
                {
                    IdArtiste = artiste.IdArtiste,
                    NomArtiste = artiste.Nom
                };
                return this.View(nameof(ArtistesController.Delete), _artiste);
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
            }
        }

        [HttpPost, ActionName(nameof(ArtistesController.Delete)), ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            try
            {
                _artisteRepository.Delete(_artisteRepository.Find(id));
                return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
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
                    return this.View(nameof(ArtistesController.Manage), InitializeList(id));
                }
                catch (NullReferenceException e)
                {
                    return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
                }
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

        [HttpPost, ActionName(nameof(ArtistesController.Manage)), ValidateAntiForgeryToken]
        public IActionResult ManagePOST(ManageArtistesViewModel manageArtistesViewModel)
        {
            if (ModelState.IsValid)
            {
                Artiste artiste = new Artiste
                {
                    IdArtiste = (manageArtistesViewModel.Artiste.IdArtiste != 0) ? manageArtistesViewModel.Artiste.IdArtiste : 0,
                    Biographie = manageArtistesViewModel.Artiste.Biographie,
                    Nom = manageArtistesViewModel.Artiste.NomArtiste,
                    IdPays = manageArtistesViewModel.Artiste.IdPays,
                    Pays = _paysRepository.Find(manageArtistesViewModel.Artiste.IdPays),
                    DateNaissance = manageArtistesViewModel.Artiste.DateNaissance
                };

                if (manageArtistesViewModel.Artiste.IdArtiste != 0) { _artisteRepository.Update(artiste); } else { _artisteRepository.Add(artiste); }

                return RedirectToAction(nameof(ArtistesController.Index), "Artistes", new { area = "Administration" });
            }
            else
            {
                InitializePaysList(manageArtistesViewModel);
                return this.View(nameof(ArtistesController.Manage), manageArtistesViewModel);
            }
        }
        #endregion


        /// <summary>
        /// Permet d'initliaze la liste d'artistes et de styles avec id du titre en paramètre si edit et null si create
        /// </summary>
        private ManageArtistesViewModel InitializeList(int? id)
        {
            ManageArtistesViewModel artistesMAVM = new ManageArtistesViewModel();
            artistesMAVM.Artiste = new ArtisteViewModel { IdArtiste = (id.HasValue) ? (int)id : 0 };
            if (id.HasValue)
            { //Si la valeur id n'est pas égal à null, la propriété Titre est affecté.
                Artiste artiste = _artisteRepository.Find((int)id);
                artistesMAVM.Artiste.NomArtiste = artiste.Nom;
                artistesMAVM.Artiste.Biographie = artiste.Biographie;
                artistesMAVM.Artiste.IdPays = artiste.IdPays;
                artistesMAVM.Artiste.DateNaissance = artiste.DateNaissance;
            }
            InitializePaysList(artistesMAVM);
            return artistesMAVM;
        }


        /// <summary>
        /// Intialize la liste d'artiste 
        /// </summary>
        private void InitializePaysList(ManageArtistesViewModel manage)
        {
            //Initialize un SelectList pour asp-for dans la vue. Le premier paramètre est la liste à convertir, le deuxième est la propriété que l'on va afficher dans la liste, le troisième est la valeur affecter à une ligne
            //Le dernier paramètre est égal à l'artiste du titre si c'est un edit sinon c'est égal à null
            manage.Pays = new SelectList(_paysRepository.FindAll().Select(vm => new Pays { IdPays = vm.IdPays, Nom = vm.Nom }), nameof(Pays.IdPays), nameof(Pays.Nom), _paysRepository.Find(manage.Artiste.IdPays));

        }


    }
}

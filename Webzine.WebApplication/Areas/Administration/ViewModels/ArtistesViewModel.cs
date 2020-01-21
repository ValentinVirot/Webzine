using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class ArtisteViewModel
    {
        [HiddenInput]
        public int IdArtiste { get; set; }
        [Required(ErrorMessage = "Le nom de l'artiste doit être renseigné !")]
        public string NomArtiste { get; set; }
        [Required(ErrorMessage = "Le pays doit être renseigné !")]
        public int IdPays { get; set; }
        public string Biographie { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

    }
}

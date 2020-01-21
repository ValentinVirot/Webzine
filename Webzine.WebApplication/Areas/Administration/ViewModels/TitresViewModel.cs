namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TitresViewModel
    {
        public string NomArtiste { get; set; }
        [Required(ErrorMessage = "Le nom du titre doit être renseigné !")]
        public string NomTitre { get; set; }
        [Required(ErrorMessage = "Le nom de l'album doit être renseignée !")]
        public string Album { get; set; }
        [HiddenInput]
        public int IdTitre { get; set; }
        public int IdArtiste { get; set; }
        public int Duree { get; set; }
        [Required(ErrorMessage = "L'url de la jacquette doit être renseignée !")]
        public string UrlJaquette { get; set; }
        public string UrlEcoute { get; set; }
        [DataType(DataType.Date), Required(ErrorMessage = "La date de sortie doit être renseignée !")]
        public DateTime DateSortie { get; set; }
        public int NbLectures { get; set; }
        public int NbLikes { get; set; }
        public int NbCommentaires { get; set; }
        public List<StylesViewModel> Styles { get; set; }
        [Required(ErrorMessage = "La chronique doit être renseignée !")]
        public string Chronique { get; set; }

    }
}

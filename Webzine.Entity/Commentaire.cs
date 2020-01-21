using System;
using System.ComponentModel.DataAnnotations;

namespace Webzine.Entity
{
    /// <summary>
    /// Contient les différentes informations concernant un commentaire.
    /// </summary>
    public class Commentaire
    {
        [Display(Name = "IdCommentaire")]
        public int IdCommentaire { get; set; }

        [Display(Name = "Nom"), MinLength(2), MaxLength(30), Required(ErrorMessage = "Le nom de l'auteur doit être renseigné !")]
        public string Auteur { get; set; }

        [Display(Name = "Commentaire"), MinLength(10), MaxLength(1000), Required(ErrorMessage = "Le contenu du commentaire doit être renseigné !")]
        public string Contenu { get; set; }

        [Display(Name = "Date de création"), Required]
        public DateTime DateCreation { get; set; }

        [Display(Name = "IdTitre")]
        public int IdTitre { get; set; }

        [Display(Name = "Titre")]
        public Titre Titre { get; set; }
    }
}

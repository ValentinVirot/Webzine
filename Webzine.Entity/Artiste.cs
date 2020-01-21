using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Webzine.Entity
{
    /// <summary>
    /// Contient les différentes information concernant un artiste, ainsi que les titres associés à celui ci.
    /// </summary>
    public class Artiste
    {
        [Display(Name = "IdArtiste")]
        public int IdArtiste { get; set; }

        [Display(Name = "Nom de l'artiste"), MinLength(1), MaxLength(50), Required]
        public string Nom { get; set; }

        [Display(Name = "Biographie"), Required]
        public string Biographie { get; set; }

       
        [Display(Name = "Titres")]
        public List<Titre> Titres { get; set; }


        [Display(Name = "IdPays")]
        public int IdPays { get; set; }
        [Display(Name ="Pays")]
        public Pays Pays { get; set; }


        [Display(Name = "Date de naissance"), DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Webzine.Entity
{
    public class Pays
    {
        [Display(Name = "IdPays")]
        public int IdPays { get; set; }

        [Display(Name = "Nom du pays"), Required]
        public string Nom { get; set; }

        [Display(Name = "Artistes")]
        public List<Artiste> Artistes { get; set; }

    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Webzine.Entity
{
    /// <summary>
    /// Contient les différentes information concernant un style de musique, ainsi que les titres associés à celui ci.
    /// </summary>
    public class Style
    {
        [Display(Name = "IdStyle")]
        public int IdStyle { get; set; }

        [Display(Name = "Libellé"), MinLength(2), MaxLength(50), Required]
        public string Libelle { get; set; }

        public ICollection<LienStyle> LienStyle { get; set; }

        public Style()
        {
            LienStyle = new List<LienStyle>();
        }
    }
}

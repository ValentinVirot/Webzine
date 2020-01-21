using System.ComponentModel.DataAnnotations;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class StylesViewModel
    {
        public int IdStyle { get; set; }
        [Required(ErrorMessage = "Le nom du style doit être renseigné !")]
        public string Libelle { get; set; }
        public bool Checked { get; set; }
    }
}

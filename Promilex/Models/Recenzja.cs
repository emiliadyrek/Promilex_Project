using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Promiex.Models
{
    public class Recenzja
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Musisz wybrać produkt, który oceniasz")]
        [Display(Name = "Produkt")]
        public int ProduktId { get; set; }

        [ForeignKey("ProduktId")]
        public virtual Produkt? Produkt { get; set; }

        [Required(ErrorMessage = "Treść recenzji jest wymagana")]
        [Display(Name = "Twoja opinia")]
        [StringLength(500, ErrorMessage = "Recenzja nie może przekraczać 500 znaków")]
        public string Tresc { get; set; }

        [Required(ErrorMessage = "Proszę wystawić ocenę")]
        [Range(1, 5, ErrorMessage = "Ocena musi być od 1 do 5")]
        [Display(Name = "Ocena (1-5)")]
        public int Ocena { get; set; }
    }
}
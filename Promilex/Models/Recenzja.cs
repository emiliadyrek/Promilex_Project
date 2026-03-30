using System.ComponentModel.DataAnnotations;

namespace Promiex.Models
{
    public class Recenzja
    {
        public int Id { get; set; }

        public int ProduktId { get; set; }

        [Required(ErrorMessage = "Treść recenzji jest wymagana")]
        public string Tresc { get; set; }

        [Range(1, 5, ErrorMessage = "Ocena musi być od 1 do 5")]
        public int Ocena { get; set; } 
    }
}
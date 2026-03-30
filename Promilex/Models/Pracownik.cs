using System.ComponentModel.DataAnnotations;

namespace Promiex.Models
{
    public class Pracownik
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Nazwisko { get; set; }

        public string Stanowisko { get; set; } 
    }
}
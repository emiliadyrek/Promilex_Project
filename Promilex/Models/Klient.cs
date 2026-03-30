using System.ComponentModel.DataAnnotations;

namespace Promiex.Models
{
    public class Klient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Nazwisko { get; set; }

        [EmailAddress(ErrorMessage = "Błędny format e-mail")]
        public string Email { get; set; }
    }
}
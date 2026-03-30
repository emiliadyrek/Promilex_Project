using System.ComponentModel.DataAnnotations;

namespace Promiex.Models
{
    public class Dostawca
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa firmy dostawczej jest wymagana!")]
        public string NazwaFirmy { get; set; }

        public string NumerTelefonu { get; set; }
    }
}
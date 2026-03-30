using System.ComponentModel.DataAnnotations;

namespace Promiex.Models
{
    public class Producent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa producenta jest wymagana!")]
        public string Nazwa { get; set; }

        public string KrajPochodzenia { get; set; }
    }
}
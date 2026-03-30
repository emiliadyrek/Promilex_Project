using System.ComponentModel.DataAnnotations;

namespace Promiex.Models
{
    public class Kategoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana!")]
        public string Nazwa { get; set; }
    }
}
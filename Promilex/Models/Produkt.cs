using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Ta linijka jest nowa

namespace Promiex.Models
{
    public class Produkt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwę trunku!")]
        public string Nazwa { get; set; }

        public string Opis { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Cena musi być większa od zera")]
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Cena { get; set; }

        public double ZawartoscAlkoholu { get; set; }
    }
}
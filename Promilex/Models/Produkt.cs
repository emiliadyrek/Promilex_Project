using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promiex.Models
{
    public class Produkt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwę trunku!")]
        [Display(Name = "Nazwa Trunku")]
        public string Nazwa { get; set; }

        [Display(Name = "Opis")]
        public string Opis { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Cena musi być większa od zera")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena (PLN)")]
        public decimal Cena { get; set; }

        [Display(Name = "Zawartość Alkoholu (%)")]
        public double ZawartoscAlkoholu { get; set; }

        [Required(ErrorMessage = "Wybierz kategorię trunku")]
        [Display(Name = "Kategoria")]
        public int KategoriaId { get; set; }

        [ForeignKey("KategoriaId")]
        public virtual Kategoria? Kategoria { get; set; }

        [Required(ErrorMessage = "Wybierz producenta")]
        [Display(Name = "Producent")]
        public int ProducentId { get; set; }

        [ForeignKey("ProducentId")]
        public virtual Producent? Producent { get; set; }

        [Display(Name = "Składniki")]
        public virtual ICollection<Skladnik> Skladniki { get; set; } = new List<Skladnik>();
    }
}
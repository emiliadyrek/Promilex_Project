using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Promiex.Models
{
    public class Skladnik
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Składnik musi mieć nazwę!")]
        [Display(Name = "Nazwa Składnika")]
        public string Nazwa { get; set; }

        public virtual ICollection<Produkt> Produkty { get; set; } = new List<Produkt>();
    }
}
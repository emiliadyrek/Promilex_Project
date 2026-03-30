using System;
using System.ComponentModel.DataAnnotations;

namespace Promiex.Models
{
    public class Zamowienie
    {
        public int Id { get; set; }

        public DateTime DataZamowienia { get; set; } = DateTime.Now;

        public int KlientId { get; set; }
        public string Status { get; set; } 
    }
}
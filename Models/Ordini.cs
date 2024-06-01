using System;
using System.ComponentModel.DataAnnotations;

namespace basilico.karol._5h.Ecommerce.Models
{
    public class Ordine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeProdotto { get; set; }

        [Required]
        public int Quantita { get; set; }

        [Required]
        public decimal Prezzo { get; set; }

        [Required]
        public DateTime DataOrdine { get; set; }

        [Required]
        public string UtenteEmail { get; set; } // Email dell'utente che ha fatto l'ordine

        // Relazione con Utente (opzionale)
        public Utente Utente { get; set; }
    }
}

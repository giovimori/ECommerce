using System.ComponentModel.DataAnnotations;

namespace basilico.karol._5h.Ecommerce.Models;

public class Prodotto
{
    public Prodotto()
    {
    
    }

    [Key]
    public int? IdP { get; set; }
    public string? NomeP { get; set; }
    public string? Descrizione { get; set; }
    public string? Prezzo { get; set; }
    
}
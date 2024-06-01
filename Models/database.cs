using Microsoft.EntityFrameworkCore;
using basilico.karol._5h.Ecommerce.Models;
public class dbContext : DbContext
{
    public dbContext() { }

    protected override void
            OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=database.db");
    public dbContext(DbContextOptions options)
    {}
    public DbSet<Utente> Utenti { get; set; }
       
        public string? Nome{ get; set; }


    public DbSet<Prodotto> Prodotti{get;set;}    

        public string? NomeP{ get; set; }

      public DbSet<Ordine> Ordini { get; set; }
        

}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using basilico.karol._5h.Ecommerce.Models;
using Newtonsoft.Json;

namespace basilico.karol._5h.Ecommerce.Controllers;

public class HomeController : Controller
{

    //private static List<Utente> Utenti = new List<Utente>();
    private readonly dbContext _context = new();

    public IActionResult Index()
    {
       /* string? login = HttpContext.Session.GetString("EMail");
        if(login == "admin@farmaciapalazzetti.it")
        {
            return View("Prodotti", "Index");
        } */
        return View(); 
    }

    public IActionResult ErroreLogin()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RemoveUser(int id)
    {
        var user = _context.Utenti.Find(id);
        if (user != null)
        {
            _context.Utenti.Remove(user);
            _context.SaveChanges();
        }
        return RedirectToAction("Registrato","Home");
    }

    public IActionResult Profilo()
    {
       return View(); 

    }
    public IActionResult Conferma()
    {
    var cartJson = HttpContext.Session.GetString("Cart");
    if (string.IsNullOrEmpty(cartJson))
    {
        return RedirectToAction("Carrello");
    }

    var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
    var userEmail = HttpContext.Session.GetString("EMail");

    var utente = _context.Utenti.FirstOrDefault(u => u.EMail == userEmail);
    if (utente == null)
    {
        return RedirectToAction("Login");
    }

    foreach (var item in cartItems)
    {
        var ordine = new Ordine
        {
            NomeProdotto = item.ProductName,
            Quantita = item.Quantity,
            Prezzo = item.Price,
            DataOrdine = DateTime.Now,
            UtenteEmail = userEmail,
            Utente = utente  // Associa l'utente all'ordine
        };

        _context.Ordini.Add(ordine);
    }

    _context.SaveChanges();
    HttpContext.Session.Remove("Cart");

    return RedirectToAction("Ordini");
    }

    public IActionResult Ordini()
    {
    var userEmail = HttpContext.Session.GetString("EMail");
    if (string.IsNullOrEmpty(userEmail))
    {
        return RedirectToAction("Login");
    }

    var ordini = _context.Ordini.Where(o => o.UtenteEmail == userEmail).ToList();
    return View(ordini);
    }

    public IActionResult DeleteFromCart(int prodottoId)
    {
    var cart = HttpContext.Session.GetString("Cart");
    if (string.IsNullOrEmpty(cart))
    {
        return RedirectToAction("Carrello");
    }

    var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cart);
    var cartItem = cartItems.FirstOrDefault(ci => ci.ProductId == prodottoId);
    if (cartItem != null)
    {
        cartItems.Remove(cartItem);
        HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));
    }

    return RedirectToAction("Carrello");
    }

    public IActionResult Privacy()
    {
        return View();
    }

     public IActionResult Registrazione()
    {
        return View();
    }

      public IActionResult Checkout()
    {
        var cart = HttpContext.Session.GetString("Cart");
        var cartItems = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);
        return View(cartItems);
    }

    public IActionResult Login()
    {
        return View();
    }

     public IActionResult Logout()
    {
        return View();
    }

    public IActionResult Farmaci()
    {
         var prodotti = _context.Prodotti.ToList();
            return View(prodotti);
    }

    public IActionResult Aiuto()
    {
        return View();
    }

     public IActionResult Delete()
    {
        return View();
    }
    public IActionResult ResetPass()
    {
        return View();
    }

   public IActionResult Carrello()
    {
    var cartJson = HttpContext.Session.GetString("Cart");
    var cartItems = string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);

    return View(cartItems);
    }

    public IActionResult Database()
    {
        string? Nome = HttpContext.Session.GetString("Nome");
        if (string.IsNullOrEmpty(Nome))
            return Redirect("\\home\\index");
        dbContext db = new ();
        db.SaveChanges();
        return View(db);       
    }

    [HttpPost]
    public IActionResult p1(Utente u) //login
    {
        dbContext db = new();      
        if(u.EMail != null && u.Password != null)
        {
            HttpContext.Session.SetString("EMail", u.EMail);
            HttpContext.Session.SetString("Password", u.Password);
           // HttpContext.Session.SetString("Nome", u.Nome);
            // HttpContext.Session.SetString("Cognome", u.Cognome);

        }
        foreach(var item in db.Utenti)
        {
            if(u.EMail==item.EMail && u.Password==item.Password)
            {              
                return RedirectToAction("Index");
            }        
        }
        return View("ErroreLogin");
    } 

    [HttpPost]
    public IActionResult p2()  //logout
    {
    
    HttpContext.Session.Remove("EMail");
    HttpContext.Session.Remove("Password");
    HttpContext.Session.Remove("Cart");

    return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Registrato( Utente u )
    {

        dbContext db = new ();
        db.Utenti.Add(u);
        db.SaveChanges();
        //settiamo una variabile di sessione chiamata nomeutente con valore u.Nome
        // HttpContext.Session.SetString("NomeUtente", u.Nome );
        return View(db);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

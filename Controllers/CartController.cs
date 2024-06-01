using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using basilico.karol._5h.Ecommerce.Models;
using Newtonsoft.Json;

namespace basilico.karol._5h.Ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly dbContext _context = new();


        // GET: Cart
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prodotti.ToListAsync());
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti
                .FirstOrDefaultAsync(m => m.IdP == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // GET: Cart/Create
        public IActionResult Create()
        {
            return View();
        }
         public IActionResult Admin()
        {
            return View();
        }
          public IActionResult Registrati()
        {
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdP,NomeP,Descrizione,Prezzo")] Prodotto prodotto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prodotto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prodotto);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("IdP,NomeP,Descrizione,Prezzo")] Prodotto prodotto)
        {
            if (id != prodotto.IdP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodotto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdottoExists(prodotto.IdP))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(prodotto);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti
                .FirstOrDefaultAsync(m => m.IdP == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto != null)
            {
                _context.Prodotti.Remove(prodotto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdottoExists(int? id)
        {
            return _context.Prodotti.Any(e => e.IdP == id);
        }

          public IActionResult AddToCart(int prodottoId)
        {
            var userEmail = HttpContext.Session.GetString("EMail");
            var userPassword = HttpContext.Session.GetString("Password");

            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userPassword))
            {
                return RedirectToAction("Login", "Home");
            }

            var prodotto = _context.Prodotti.FirstOrDefault(p => p.IdP == prodottoId);
            if (prodotto == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);

            var cartItem = cartItems.FirstOrDefault(ci => ci.ProductId == prodottoId);
            if (cartItem == null)
            {
                cartItems.Add(new CartItem
                {
                    ProductId = prodotto.IdP.Value,
                    ProductName = prodotto.NomeP,
                    Quantity = 1,
                    Price = decimal.Parse(prodotto.Prezzo)
                });
            }
            else
            {
                cartItem.Quantity++;
            }

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));

            return RedirectToAction("Farmaci", "Home");
        }

        public IActionResult ViewCart()
        {
            var cart = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);

            return View(cartItems);
        }
    }
}
    


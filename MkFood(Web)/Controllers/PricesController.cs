using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKFood.DB.Models;

namespace MkFood_Web_.Controllers
{
    public class PricesController : Controller
    {
        private readonly MKFoodDbContext _context;

        public PricesController(MKFoodDbContext context)
        {
            _context = context;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            var mKFoodDbContext = _context.Prices.Include(p => p.Food);
            return View(await mKFoodDbContext.ToListAsync());
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prices == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .Include(p => p.Food)
                .FirstOrDefaultAsync(m => m.PriceId == id);
            if (price == null)
            {
                return NotFound();
            }

            //return View(price);
            return RedirectToAction("Index", "Foods");

        }

        // GET: Prices/Create
        public async Task<IActionResult> Create(int? id)
        {
            Food? food = await _context.Foods.FindAsync(id);
           
            if (food == null)
                return NotFound();

            ViewData["FoodId"] = new SelectList(_context.Foods, "FoodId", "Name",id);
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("PriceId,Cost,Expierd,FoodId")] Price price)
        {

            //Deactive Price Actived before
            Price? p = await _context.Prices.Where(p => p.FoodId == id && !p.Expierd).FirstOrDefaultAsync<Price>();
            if (p != null)
            {
                p.Expierd = true;
                _context.Prices.Update(p);
            }
                
            _context.Add(price);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Foods");

        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prices == null)
            {
                return NotFound();
            }

            var price = await _context.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            ViewData["FoodId"] = new SelectList(_context.Foods, "FoodId", "FoodId", price.FoodId);
            //return View(price);
            return RedirectToAction("Index", "Foods");
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PriceId,Cost,Expierd,FoodId")] Price price)
        {
            if (id != price.PriceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(price);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.PriceId))
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
            ViewData["FoodId"] = new SelectList(_context.Foods, "FoodId", "FoodId", price.FoodId);
            return View(price);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prices == null)
            {
                return NotFound();
            }

            var price = await _context.Prices
                .Include(p => p.Food)
                .FirstOrDefaultAsync(m => m.PriceId == id);
            if (price == null)
            {
                return NotFound();
            }

            //return View(price);
            return RedirectToAction("Index", "Foods");

        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prices == null)
            {
                return Problem("Entity set 'MKFoodDbContext.Prices'  is null.");
            }
            var price = await _context.Prices.FindAsync(id);
            if (price != null)
            {
                _context.Prices.Remove(price);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceExists(int id)
        {
          return _context.Prices.Any(e => e.PriceId == id);
        }
    }
}

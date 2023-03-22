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
    }
}

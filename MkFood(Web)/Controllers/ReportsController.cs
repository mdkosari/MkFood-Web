using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKFood.DB.Models;

namespace MkFood_Web_.Controllers
{
    public class ReportsController : Controller
    {
        private readonly MKFoodDbContext _context;

        public ReportsController(MKFoodDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var mkFoomKFoodDbContext = _context.Bills
                .Where(b => !b.Open)
                .Include(b => b.orders)
                .Include(b => b.Customer);

            ViewData["From"] = "00/00/0000";
            ViewData["To"] = DateTime.Now.ToString("dd/MM/yyyy");
            ViewData["Success"] = false;

            return View(await mkFoomKFoodDbContext.ToListAsync());
        }


        //GET: Fillter/?from=_&to=_&success=_
        public async Task<IActionResult> Filter(string from, string to, bool success = false)
        {
            //check null format Date
            from = from == "00/00/0000" ? DateTime.Now.ToString("dd/MM/yyyy") : from;
            to = to == "00/00/0000" ? DateTime.Now.ToString("dd/MM/yyyy") : to;

            //Pars date
            DateTime dFrom = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dTo = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            
            //Extract Data
            List<Bill> bills = new List<Bill>();
            if (success)
            {
                bills = await _context.Bills
                .Where(b => b.Created >= dFrom && b.Created <= dTo && b.Success == success)
                .Include(b => b.orders)
                .ToListAsync();
            }
            else
            {
                bills = await _context.Bills
                .Where(b => b.Created >= dFrom && b.Created <= dTo)
                .Include(b => b.orders)
                .ToListAsync();
            }

            ViewData["From"] = from;
            ViewData["To"] = to;
            ViewData["Success"] = success;

            return View("Index", bills);
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bills == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.orders)
                    .ThenInclude(o => o.Food)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

    }
}

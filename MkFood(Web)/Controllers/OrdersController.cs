using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKFood.DB.Models;

namespace MkFood_Web_.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MKFoodDbContext _context;

        public OrdersController(MKFoodDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var mKFoodDbContext = _context.Orders
                .Where(o => o.Bill != null && o.Bill.Open)
                .Include(o => o.Bill)
                .Include(o => o.Food);
            return View(await mKFoodDbContext.ToListAsync());
        }


        //GET: CheckOrderedFood
        //Chek Ordered Food
        public async Task<string> CheckOrderedFood()
        {
            List<Order> orders = await _context.Orders
                .Where(o => o.Bill != null && o.Bill.Open ).ToListAsync();

            
            return JsonSerializer.Serialize<List<Order>>(orders);

        }


        //GET: AddOrders/2
        public async Task<string> AddOrders(int id)
        {
            if (id <= 0)
            {
                return "Id Not Valide";
            }
            Food? food = await _context.Foods.FindAsync(id);

            if (food == null)
            {
                return "Food Not Found";
            }
            

            Bill? bill = await _context.Bills.Where(b => b.Open).FirstOrDefaultAsync();
            if (bill == null)
            {
                bill = new Bill();
                _context.Bills.Add(bill);
            }

            double cost = (await _context.Prices
                     .Where(p => p.FoodId == id && !p.Expierd)
                     .FirstAsync()).Cost;

            Order order = new Order() { BillId = bill.BillId,Bill = bill, Count = 1, Cost = cost, FoodId = id, OrderNumber = bill.BillId };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return "Success Added";
        }

        //GET : DecOrder/2
        //Decreas Count and Update Order
        public async Task<string> DecOrder(int id)
        {
            double[] result = { -1, -1 };
            Order? order = await _context.Orders.FindAsync(id);
            if(order == null)
            {
                return JsonSerializer.Serialize(result);
            }

           
            if(order.Count <= 1)
            {
                _context.Orders.Remove(order);
                --order.Count;
            }
            else
            {
                double cost = order.Cost / order.Count;
                --order.Count;
                order.Cost = order.Count * cost;
                _context.Orders.Update(order);
                    
            }
            await _context.SaveChangesAsync();
            result[0] = order.Count;
            result[1] = order.Cost;
            return JsonSerializer.Serialize(result);

        }

        //GET : IncOrder/2
        //Increas Count and Update Order
        public async Task<string> IncOrder(int id)
        {
            double[] result = {-1,-1};
            Order? order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return JsonSerializer.Serialize(result);
            }
            double cost = order.Cost / order.Count;
            ++order.Count;
            order.Cost = order.Count * cost;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            result[0] = order.Count;
            result[1] = order.Cost;
            
            return JsonSerializer.Serialize(result);
        }


        //GET: BillCalculator/?id=_&fee=_&tax=_&date=_
        //Calculate Cost And Update Bill
        public async Task<double> BillCalculator(int id,double? fee, double? tax, string? date)
        {
            Bill? bill = await _context.Bills
                .Where(b => b.BillId == id)
                .Include(b => b.orders)
                .FirstOrDefaultAsync();
                ;
            if(bill != null)
            {
                bill.Fee = fee ?? 0;
                bill.Tax = tax ?? 0;
                double cost = 0;
                foreach(Order ord in bill.orders)
                {
                    cost += ord.Cost;
                }

                double? present = (cost * tax) / 100;
                cost += present ?? 0;
                cost += fee ?? 0;

                bill.Cost = cost;
                _context.Update(bill);
                await _context.SaveChangesAsync();
                return cost;
            }
            return 0;
        }

        //GET: SaveBill/?id=_&date=_&name
        //Save Customer and Update Bill
        public async Task<bool> SaveBill(int id, string date,string? name)
        {
            Bill? bill = await _context.Bills.FindAsync(id);
            if(bill == null)
            {
                return false;
            }

            
            Customer? customer = null;
            if (name != null)
            {
                Random r = new Random(9999);
                customer = new Customer() { Name = name,AccountNumber= r.Next()};
                if(customer != null)
                {
                    _context.Customers.Add(customer);
                }
            }
            bill.Customer = customer;
            DateTime dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            bill.Created = dt;
            bill.Open = false;
            bill.Success = true;
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

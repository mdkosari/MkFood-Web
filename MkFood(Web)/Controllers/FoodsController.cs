﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKFood.DB.Models;
using NuGet.Packaging.Signing;

namespace MkFood_Web_.Controllers
{
    public class FoodsController : Controller
    {
        private readonly MKFoodDbContext _context;

        public FoodsController(MKFoodDbContext context)
        {
            _context = context;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            List<Food> foods  = await _context.Foods
                .Include(f => f.Category)
                .Include(f => f.Prices)
                .ToListAsync();

            return View(foods);
        }

        //GET: CategoryFoods/2
        public async Task<IActionResult> CategoryFoods(int? id)
        {
            if(id != null || id >= 0)
            {
                List<Food> foods = await _context.Foods
                    .Where(f => f.CategoryId == id)
                    .Include(f => f.Category)
                    .Include(f => f.Prices)
                    .ToListAsync();

                return View("Index", foods);
            }
            return View("Index");
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.Category)
                .Include(f => f.Prices)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Foods/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FoodId,Name,Description,Picture,CategoryId")] Food food)
        {
            
            _context.Add(food);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", food.CategoryId);
            return View(food);
        }

        // POST: Foods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodId,Name,Description,Picture,CategoryId")] Food food)
        {
            if (id != food.FoodId)
            {
                return NotFound();
            }

           
            try
            {
                _context.Update(food);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(food.FoodId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
            
        }

        private bool FoodExists(int id)
        {
          return _context.Foods.Any(e => e.FoodId == id);
        }
    }
}

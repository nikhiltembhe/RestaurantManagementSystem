using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Areas.ResMgmtSys.Controllers
{
    [Area("ResMgmtSys")]
    public class FoodCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResMgmtSys/FoodCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodCategories.ToListAsync());
        }

        // GET: ResMgmtSys/FoodCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodCategory = await _context.FoodCategories
                .FirstOrDefaultAsync(m => m.FoodCategoryId == id);
            if (foodCategory == null)
            {
                return NotFound();
            }

            return View(foodCategory);
        }

        // GET: ResMgmtSys/FoodCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResMgmtSys/FoodCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodCategoryId,FoodCategoryName")] FoodCategory foodCategory)
        {
            // Sanitize the data
            foodCategory.FoodCategoryName = foodCategory.FoodCategoryName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.FoodCategories.Any(c => c.FoodCategoryName == foodCategory.FoodCategoryName);
            if (duplicateExists)
            {
                ModelState.AddModelError("FoodCategoryName", "Duplicate Category Found!");
            }
            if (ModelState.IsValid)
            {
                _context.Add(foodCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodCategory);
        }

        // GET: ResMgmtSys/FoodCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodCategory = await _context.FoodCategories.FindAsync(id);
            if (foodCategory == null)
            {
                return NotFound();
            }
            return View(foodCategory);
        }

        // POST: ResMgmtSys/FoodCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodCategoryId,FoodCategoryName")] FoodCategory foodCategory)
        {
            if (id != foodCategory.FoodCategoryId)
            {
                return NotFound();
            }
            // Sanitize the data
            foodCategory.FoodCategoryName = foodCategory.FoodCategoryName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.FoodCategories.Any(c => c.FoodCategoryName == foodCategory.FoodCategoryName && c.FoodCategoryId != foodCategory.FoodCategoryId);
            if (duplicateExists)
            {
                ModelState.AddModelError("FoodCategoryName", "Duplicate Category Found!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodCategoryExists(foodCategory.FoodCategoryId))
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
            return View(foodCategory);
        }

        // GET: ResMgmtSys/FoodCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodCategory = await _context.FoodCategories
                .FirstOrDefaultAsync(m => m.FoodCategoryId == id);
            if (foodCategory == null)
            {
                return NotFound();
            }

            return View(foodCategory);
        }

        // POST: ResMgmtSys/FoodCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodCategory = await _context.FoodCategories.FindAsync(id);
            _context.FoodCategories.Remove(foodCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodCategoryExists(int id)
        {
            return _context.FoodCategories.Any(e => e.FoodCategoryId == id);
        }
    }
}

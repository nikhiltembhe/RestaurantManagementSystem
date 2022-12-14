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
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResMgmtSys/Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        // GET: ResMgmtSys/Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: ResMgmtSys/Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResMgmtSys/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,PhoneNumer,Email,Address")] Customer customer)
        {
            // Sanitize the data
            customer.CustomerName = customer.CustomerName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.Customer.Any(c => c.CustomerName == customer.CustomerName);
            if (duplicateExists)
            {
                ModelState.AddModelError("CustomerName", "Duplicate Category Found!");
            }

            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                //_logger.LogInformation($"Created a New Category: ID = {category.CategoryId} !");
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Create","Orders");
            }
            return View(customer);
        }

        // GET: ResMgmtSys/Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: ResMgmtSys/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,PhoneNumer,Email,Address")] Customer customer)
        {
            // Validation Check if the CategoryId matches with the ID for the row to edit.
            if (id != customer.CustomerId)
            {
                return NotFound();
            }
            // Sanitize the data
            customer.CustomerName = customer.CustomerName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.Customer.Any(c => c.CustomerName == customer.CustomerName && c.CustomerId != customer.CustomerId);
            if (duplicateExists)
            {
                ModelState.AddModelError("CustomerName", "Duplicate Category Found!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: ResMgmtSys/Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: ResMgmtSys/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}

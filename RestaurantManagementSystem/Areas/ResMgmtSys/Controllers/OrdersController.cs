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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResMgmtSys/Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.Customer).Include(o => o.FoodItem).Include(o => o.Waiter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ResMgmtSys/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.FoodItem)
                .Include(o => o.Waiter)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: ResMgmtSys/Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "CustomerId", "CustomerName");
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "FoodItemId", "FoodItemName");
            ViewData["WaiterId"] = new SelectList(_context.Set<Waiter>(), "WaiterId", "WaiterName");
            return View();
        }

        // POST: ResMgmtSys/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderName,CreatedOn,OrderCompleted,OrderQuantity,FoodItemId,CustomerId,WaiterId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "CustomerId", "CustomerName", order.CustomerId);
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "FoodItemId", "FoodItemName", order.FoodItemId);
            ViewData["WaiterId"] = new SelectList(_context.Set<Waiter>(), "WaiterId", "WaiterName", order.WaiterId);
            return View(order);
        }

        // GET: ResMgmtSys/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "CustomerId", "CustomerName", order.CustomerId);
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "FoodItemId", "FoodItemName", order.FoodItemId);
            ViewData["WaiterId"] = new SelectList(_context.Set<Waiter>(), "WaiterId", "WaiterName", order.WaiterId);
            return View(order);
        }

        // POST: ResMgmtSys/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderName,CreatedOn,OrderCompleted,OrderQuantity,FoodItemId,CustomerId,WaiterId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "CustomerId", "CustomerName", order.CustomerId);
            ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "FoodItemId", "FoodItemName", order.FoodItemId);
            ViewData["WaiterId"] = new SelectList(_context.Set<Waiter>(), "WaiterId", "WaiterName", order.WaiterId);
            return View(order);
        }

        // GET: ResMgmtSys/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.FoodItem)
                .Include(o => o.Waiter)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: ResMgmtSys/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}

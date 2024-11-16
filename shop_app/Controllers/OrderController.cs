using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_app.Models;

namespace shop_app.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(OrderContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Отримати всі замовлення (Read)
        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.Product)
                .ToList();
            return View(orders);
        }

        // Створення нового замовлення (Create)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                order.IdentityUserId = user?.Id;
                order.OrderDate = DateTime.Now;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // Отримання замовлення для редагування (Update)
        public IActionResult Edit(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // Видалення замовлення (Delete)
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

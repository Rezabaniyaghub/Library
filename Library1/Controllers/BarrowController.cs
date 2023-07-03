using System;
using System.Linq;
using System.Threading.Tasks;
using Library1.Data;
using Library1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YourProject.Controllers
{
    public class BarrowController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarrowController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var barrows = await _context.Barrows
                .Include(b => b.Member)
                .Include(b => b.Book)
                .ToListAsync();

            return View(barrows);
        }

        public async Task<IActionResult> Details(int id)
        {
            var barrow = await _context.Barrows
                .Include(b => b.Member)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (barrow == null)
            {
                return NotFound();
            }

            return View(barrow);
        }

        public IActionResult Create()
        {
            // در این قسمت می‌توانید لیست اعضا و کتاب‌ها را از پایگاه داده خوانده و به مدل منتقل کنید
            // سپس مدل را به صفحه Create ارسال کنید
            // مثال: 
            // var members = await _context.Members.ToListAsync();
            // var books = await _context.Books.ToListAsync();
            // var viewModel = new BarrowViewModel
            // {
            //     Members = members,
            //     Books = books
            // };
            // return View(viewModel);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Barrow barrow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(barrow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(barrow);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var barrow = await _context.Barrows.FindAsync(id);

            if (barrow == null)
            {
                return NotFound();
            }

            return View(barrow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Barrow barrow)
        {
            if (id != barrow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barrow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarrowExists(barrow.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(barrow);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var barrow = await _context.Barrows.FindAsync(id);

            if (barrow == null)
            {
                return NotFound();
            }

            return View(barrow);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barrow = await _context.Barrows.FindAsync(id);
            _context.Barrows.Remove(barrow);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool BarrowExists(int id)
        {
            return _context.Barrows.Any(b => b.Id == id);
        }
    }
}

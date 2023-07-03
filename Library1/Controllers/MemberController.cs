using System.Linq;
using System.Threading.Tasks;
using Library1.Data;
using Library1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YourProject.Controllers
{
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _context.Members.ToListAsync();
            return View(members);
        }

        public async Task<IActionResult> Details(int id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(m => m.Id == id);
        }
    }
}

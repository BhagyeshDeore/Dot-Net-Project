using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityDemo2.Data;
using IdentityDemo2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityDemo2.Controllers
{
    public class ContestsController : Controller
    {
        private readonly ParikshakDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ContestsController(ParikshakDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }



        // GET: Contests
        public async Task<IActionResult> Index()
        {
            var parikshakDBContext = _context.Contestes.Include(c => c.ApplicationUser);
            return View(await parikshakDBContext.ToListAsync());
        }

        // GET: Contests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contestes == null)
            {
                return NotFound();
            }

            var contest = await _context.Contestes
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contest == null)
            {
                return NotFound();
            }

            return View(contest);
        }

        [Authorize(Roles ="TEACHER")]
        // GET: Contests/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Contests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Topic,CreatedAt")] Contest contest)
        {
            //if (ModelState.IsValid)
            //{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            Console.WriteLine("Userid creating contest is " + userId+" with user name : "+userName);
            contest.ApplicationUserId = userId;
                _context.Add(contest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", contest.ApplicationUserId);
            //return View(contest);
        }

        // GET: Contests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contestes == null)
            {
                return NotFound();
            }

            var contest = await _context.Contestes.FindAsync(id);
            if (contest == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", contest.ApplicationUserId);
            return View(contest);
        }

        // POST: Contests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Topic,CreatedAt,ApplicationUserId")] Contest contest)
        {
            if (id != contest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContestExists(contest.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", contest.ApplicationUserId);
            return View(contest);
        }

        // GET: Contests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contestes == null)
            {
                return NotFound();
            }

            var contest = await _context.Contestes
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contest == null)
            {
                return NotFound();
            }

            return View(contest);
        }

        // POST: Contests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contestes == null)
            {
                return Problem("Entity set 'ParikshakDBContext.Contestes'  is null.");
            }
            var contest = await _context.Contestes.FindAsync(id);
            if (contest != null)
            {
                _context.Contestes.Remove(contest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContestExists(int id)
        {
          return (_context.Contestes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityDemo2.Data;
using IdentityDemo2.Models;

namespace IdentityDemo2.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly ParikshakDBContext _context;

        public ProblemsController(ParikshakDBContext context)
        {
            _context = context;
        }

        // GET: Problems
        public async Task<IActionResult> Index()
        {
            var parikshakDBContext = _context.Problemes.Include(p => p.Contest);
            return View(await parikshakDBContext.ToListAsync());
        }

        // GET: Problems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Problemes == null)
            {
                return NotFound();
            }

            var problem = await _context.Problemes
                .Include(p => p.Contest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // GET: Problems/Create
        public IActionResult Create()
        {
            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id");
            return View();
        }

        // POST: Problems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ProblemStatement,Explanation,Marks,DifficultyLevel,SampleInput,SampleOutput,Testcase,ResultOfTestCase,SolutionCode,ContestId")] Problem problem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id", problem.ContestId);
            return View(problem);
        }

        // GET: Problems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Problemes == null)
            {
                return NotFound();
            }

            var problem = await _context.Problemes.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }
            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id", problem.ContestId);
            return View(problem);
        }

        // POST: Problems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ProblemStatement,Explanation,Marks,DifficultyLevel,SampleInput,SampleOutput,Testcase,ResultOfTestCase,SolutionCode,ContestId")] Problem problem)
        {
            if (id != problem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemExists(problem.Id))
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
            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id", problem.ContestId);
            return View(problem);
        }

        // GET: Problems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Problemes == null)
            {
                return NotFound();
            }

            var problem = await _context.Problemes
                .Include(p => p.Contest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // POST: Problems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Problemes == null)
            {
                return Problem("Entity set 'ParikshakDBContext.Problemes'  is null.");
            }
            var problem = await _context.Problemes.FindAsync(id);
            if (problem != null)
            {
                _context.Problemes.Remove(problem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemExists(int id)
        {
          return (_context.Problemes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

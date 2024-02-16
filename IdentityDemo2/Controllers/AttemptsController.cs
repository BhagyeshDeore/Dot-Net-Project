using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityDemo2.Data;
using IdentityDemo2.Models;
using Microsoft.AspNetCore.Identity;
using IdentityDemo2.Migrations;
using System.Security.Claims;

namespace IdentityDemo2.Controllers
{
    public class AttemptsController : Controller
    {
        private readonly ParikshakDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public AttemptsController(ParikshakDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Attempts
        public async Task<IActionResult> Index()
        {
            var parikshakDBContext = _context.Attempt.Include(a => a.ApplicationUser).Include(a => a.Contest).Include(a => a.Problem);
            return View(await parikshakDBContext.ToListAsync());
        }

        // GET: Attempts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attempt == null)
            {
                return NotFound();
            }

            var attempt = await _context.Attempt
                .Include(a => a.ApplicationUser)
                .Include(a => a.Contest)
                .Include(a => a.Problem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attempt == null)
            {
                return NotFound();
            }

            return View(attempt);
        }

        // GET: Attempts/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id");
            ViewData["ProblemId"] = new SelectList(_context.Problemes, "Id", "Id");
            return View();
        }

        // POST: Attempts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Language,SolvedStatus,Result,ObtainedMarks,ApplicationUserId,ContestId,ProblemId")] Attempt attempt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attempt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", attempt.ApplicationUserId);
            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id", attempt.ContestId);
            ViewData["ProblemId"] = new SelectList(_context.Problemes, "Id", "Id", attempt.ProblemId);
            return View(attempt);
        }

        // GET: Attempts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attempt == null)
            {
                return NotFound();
            }

            var attempt = await _context.Attempt.FindAsync(id);
            if (attempt == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", attempt.ApplicationUserId);
            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id", attempt.ContestId);
            ViewData["ProblemId"] = new SelectList(_context.Problemes, "Id", "Id", attempt.ProblemId);
            return View(attempt);
        }

        // POST: Attempts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Language,SolvedStatus,Result,ObtainedMarks,ApplicationUserId,ContestId,ProblemId")] Attempt attempt)
        {
            if (id != attempt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attempt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttemptExists(attempt.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", attempt.ApplicationUserId);
            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id", attempt.ContestId);
            ViewData["ProblemId"] = new SelectList(_context.Problemes, "Id", "Id", attempt.ProblemId);
            return View(attempt);
        }

        // GET: Attempts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attempt == null)
            {
                return NotFound();
            }

            var attempt = await _context.Attempt
                .Include(a => a.ApplicationUser)
                .Include(a => a.Contest)
                .Include(a => a.Problem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attempt == null)
            {
                return NotFound();
            }

            return View(attempt);
        }

        // POST: Attempts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attempt == null)
            {
                return Problem("Entity set 'ParikshakDBContext.Attempt'  is null.");
            }
            var attempt = await _context.Attempt.FindAsync(id);
            if (attempt != null)
            {
                _context.Attempt.Remove(attempt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttemptExists(int id)
        {
          return (_context.Attempt?.Any(e => e.Id == id)).GetValueOrDefault();
        }





        ////*** Viraj working here ***/////////////////////////////////////////////////////////////////


        // GET: Attempts/Create
        public IActionResult AttemptProblemCodeIt()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id");
            ViewData["ProblemId"] = new SelectList(_context.Problemes, "Id", "Id");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttemptProblemCodeIt([Bind("Code,ApplicationUserId,ContestId,ProblemId")] Attempt attempt)
        {
           
                _context.Add(attempt);
                await _context.SaveChangesAsync();
                return RedirectToAction("");
           
        }



        ////*** Viraj Completed ***////////////////////////////////////////////////////////////////////












        ////*** Rushikesh working here ***/////////////////////////////////////////////////////////////
        // POST: Attempts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> attemptproblem_partial_(int? problem_id ithe get kar,[Bind("Code")] Attempt attempt)
        {

            //contest id shall be passed and logged in user shall also post 
            Console.WriteLine("Submitted problem for id: " + attempt.Id);
            attempt.Language = CodingLanguage.JAVA;
            attempt.SolvedStatus = SolvedStatus.Solved;
            attempt.Result = "PASS";
            attempt.ObtainedMarks = 10;

            _context.Add(attempt);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> attemptproblem_partial_([Bind("Code")] Attempt attempt, int ProblemId)
        {
           
            attempt.Language = CodingLanguage.JAVA;
            attempt.SolvedStatus = SolvedStatus.Solved;
            attempt.Result = "PASS";
            attempt.ObtainedMarks = 10;
            attempt.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var problem = _context.Problemes.Find(ProblemId);
            attempt.ContestId = problem.ContestId; 
            attempt.ProblemId = ProblemId;

            Console.WriteLine("Submitted problem id  : " + attempt.ProblemId + " contest id " + attempt.ContestId);
            Console.WriteLine("Submitted  for user  id: " + attempt.ApplicationUserId);


            _context.Add(attempt);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //POST: 
        [HttpPost]
        public async Task<IActionResult> attemptproblemCode()
        {
            Console.WriteLine("Hello from attempt");

            return View();
        }




        ////*** Rushikesh Completed ***////////////////////////////////////////////////////////////////














        ////*** Bhagyesh working here ***////////////////////////////////////////////////////////////////
        [Route("Attempts/SeeAttempts/{contestId}")]
        public async Task<IActionResult> SeeAttempts(int contestId)
        {
            
            var attempts = await _context.Attempt
                .Include(a => a.ApplicationUser) 
                .Include(a => a.Problem)
                    .ThenInclude(p => p.Contest)
                .Where(a => a.ContestId == contestId)
                .ToListAsync();

            return View(attempts);
        }

        ////*** Bhagyesh Completed ***////////////////////////////////////////////////////////////////////////////










        ////*** Ankita working here ***/////////////////////////////////////////////////////////////////


        ////*** Ankita Completed ***///////////////////////////////////////////////////////////////////////////







        ////*** Vaishnavi working here ***////////////////////////////////////////////////////////////////


        ////*** Vaishanavi Completed ***/////////////////////////////////////////////////////////////////

    }
}

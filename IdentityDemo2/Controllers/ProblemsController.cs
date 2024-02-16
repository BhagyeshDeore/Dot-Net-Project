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
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Tls;

namespace IdentityDemo2.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly ParikshakDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProblemsController(ParikshakDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

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

            

              
                try
                {
                    _context.Update(problem);
                    await _context.SaveChangesAsync();

                    int contestId = problem.ContestId;
                    return RedirectToAction("TeacherContestDetails", "Contests", new { id = contestId });
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
            
            

            ViewData["ContestId"] = new SelectList(_context.Contestes, "Id", "Id", problem.ContestId);
            return RedirectToAction("TeacherMyContests");
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




        ////*** Viraj working here ***/////////////////////////////////////////////////////////////////


        [Authorize(Roles ="TEACHER")]
        // GET: Problems/Create
        public IActionResult AddProblem()
        {
            return View();
        }



        // POST: Problems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="TEACHER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProblem(int id,[Bind("Title,Description,ProblemStatement,Explanation,Marks,DifficultyLevel,SampleInput,SampleOutput,Testcase,ResultOfTestCase,SolutionCode")] Problem problem)
        {
            Console.WriteLine("craeting prblem for id : " + id+ "with problem Id "+problem.Id);
            //setting contest id in problem 
            problem.ContestId = id;

            _context.Add(problem);
            await _context.SaveChangesAsync();
            return RedirectToAction( "TeacherContestDetails","Contests", new { id = id});
           
           
        }

        public async Task<IActionResult> ContestroblemsList_partial_() {

            var parikshakDBContext = _context.Problemes.ToListAsync();
            Console.WriteLine(parikshakDBContext);
            return PartialView( parikshakDBContext );

;        }

        public async Task<IActionResult> ProblemDetails(int? id)
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







        ////*** Viraj Completed ***////////////////////////////////////////////////////////////////////












        ////*** Rushikesh working here ***/////////////////////////////////////////////////////////////
        // GET: Attempt a Problem
        [Authorize(Roles = "STUDENT")]
        public async Task<IActionResult> AttemptProblem(int problemId)
        {
            var problemDetails = await _context.Problemes
                .Where(p => p.Id == problemId)
                .FirstOrDefaultAsync();

            if (problemDetails == null)
            {
                return NotFound();
            }

            return View(problemDetails);
        }


        // GET: Attempts/Create
        [Authorize(Roles = "STUDENT")]
        public IActionResult attemptproblem_partial_()
        {
            ViewBag.ApplicationUserId = new SelectList(_context.Users, "Id", "Id");
            ViewBag.ContestId = new SelectList(_context.Contestes, "Id", "Id");
            ViewBag.ProblemId= new SelectList(_context.Problemes, "Id", "Id");
            return PartialView();
        }

        //POST: 
        [HttpPost]
        public async Task<IActionResult> attemptproblemCode()
        {
            Console.WriteLine("Hello");

            return View();
        }



        ////*** Rushikesh Completed ***////////////////////////////////////////////////////////////////














        ////*** Bhagyesh working here ***////////////////////////////////////////////////////////////////
        //public async Task<IActionResult> SeeResult(int contestId)
        //{
        //    // Retrieve problems for the specified contest including attempts made by students
        //    var problemsWithAttempts = await _context.Problemes
        //        .Include(p => p.Contest)
        //        .ThenInclude(a => a.ApplicationUser) // Include user information for attempts
        //        .Where(p => p.ContestId == contestId)
        //        .ToListAsync();

        //    return View(problemsWithAttempts);
        //}


        ////*** Bhagyesh Completed ***////////////////////////////////////////////////////////////////////////////










        ////*** Ankita working here ***/////////////////////////////////////////////////////////////////


        ////*** Ankita Completed ***///////////////////////////////////////////////////////////////////////////







        ////*** Vaishnavi working here ***////////////////////////////////////////////////////////////////


        ////*** Vaishanavi Completed ***/////////////////////////////////////////////////////////////////

    }
}

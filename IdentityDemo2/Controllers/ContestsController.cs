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
using System.Security.Principal;
using System.Runtime.Intrinsics.X86;

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


        ////*** Viraj working here ***/////////////////////////////////////////////////////////////////

        [Authorize(Roles ="TEACHER")]
        // GET: TeacherAllContests
        public async Task<IActionResult> TeacherAllContests()
        {
            var parikshakDBContext = _context.Contestes.Include(c => c.ApplicationUser);
            return View(await parikshakDBContext.ToListAsync());
        }


        [Authorize(Roles = "TEACHER")]
        // GET: TeacherAllContests
        public async Task<IActionResult> TeacherMyContests()
        {
            //getting logged in user id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //fetching contests where user id is this
            var parikshakDBContext = _context.Contestes.Where(c => c.ApplicationUserId == userId ).Include(c => c.ApplicationUser);
            return View(await parikshakDBContext.ToListAsync());
        }



        [Authorize(Roles = "TEACHER")]
        // GET: Contests/Create
        public IActionResult CreateNewContest()
        {
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }


        [Authorize(Roles = "TEACHER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewContest([Bind("Id,Title,Description,Topic,CreatedAt")] Contest contest)
        {
           
            //getting logged in user id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //assigning it to the contest object
            contest.ApplicationUserId = userId;
            //adding to database
            _context.Add(contest);
            await _context.SaveChangesAsync();
            return RedirectToAction("TeacherAllContests");
           
        }

        [Authorize(Roles = "TEACHER")]
        // GET: Contests/Details/5
        public async Task<IActionResult> TeacherContestDetails(int? id)
        {
            if (id == null || _context.Contestes == null)
            {
                return NotFound();
            }

            var contest = await _context.Contestes
                .Include(c => c.ApplicationUser)
                .Include(c => c.problems)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contest == null)
            {
                return NotFound();
            }
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (contest.ApplicationUserId != userId) {
            //    return NotFound();
            //}

            return View(contest);
        }

        [Authorize(Roles = "TEACHER")]
        // GET: Contests/Edit/5
        public async Task<IActionResult> EditMyContest(int? id)
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (contest.ApplicationUserId != userId)
            {
                return NotFound();
            }

            return View(contest);
        }

        // POST: Contests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "TEACHER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMyContest(int id, [Bind("Id,Title,Description,Topic")] Contest contest)
        {
            if (id != contest.Id)
            {
                return NotFound();
            }

            try
            {
                //getting logged in user id
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //assigning it to the contest object
                contest.ApplicationUserId = userId;
                Console.WriteLine(contest.Title + contest.ApplicationUserId + contest.Description);
                var contestUpdated =  _context.Contestes.Find(contest.Id);
                contestUpdated.Title = contest.Title;
                contestUpdated.Topic = contest.Topic;
                contestUpdated.Description = contest.Description;
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
            return RedirectToAction("TeacherMyContests");
           
        }






        ////*** Viraj Completed ***////////////////////////////////////////////////////////////////////












        ////*** Rushikesh working here ***/////////////////////////////////////////////////////////////
        
        
        // GET: All Contests for Student
        [Authorize(Roles = "STUDENT")]
        public async Task<IActionResult> AllContestsForStudent()
        {
            var contests = await _context.Contestes.Include(c => c.ApplicationUser).ToListAsync();
            return View(contests);
        }


        // GET: Problems of a Specific Contest for Student
        [Authorize(Roles = "STUDENT")]
        public async Task<IActionResult> ProblemsForContest(int contestId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var contest = await _context.Contestes
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == contestId);

            if (contest == null)
            {
                return NotFound();
            }

            var problemsForContest = await _context.Problemes
                .Where(p => p.ContestId == contestId)
                .ToListAsync();

            var leaderboardData = await _context.Attempt
             .Include(a => a.ApplicationUser)
             .Where(a => a.ContestId == contestId)
             .GroupBy(a => a.ApplicationUser)
             .Select(g => new Attempt
             {
                 ApplicationUser = g.Key,
                 ObtainedMarks = g.Sum(a => _context.Problemes.FirstOrDefault(p => p.Id == a.ProblemId).Marks)
             })
             .OrderByDescending(a => a.ObtainedMarks)
             .Take(10)
             .ToListAsync();

            var alreadyAttemptOnProblems = await _context.Attempt
                .Where(a => a.ContestId == contestId)
                .Where(a => a.ApplicationUserId == userId )
                .ToListAsync();


            ViewData["ContestNumber"] = contest.Id;
            ViewData["ContestTitle"] = contest.Title;
            ViewData["LeaderboardData"] = leaderboardData;
            ViewData["AlreadyAttempted"] = alreadyAttemptOnProblems;
            return View(problemsForContest);
        }


        ////*** Rushikesh Completed ***////////////////////////////////////////////////////////////////














        ////*** Bhagyesh working here ***////////////////////////////////////////////////////////////////

        //// Done all work in teammate's area

        ////*** Bhagyesh Completed ***////////////////////////////////////////////////////////////////////////////










        ////*** Ankita working here ***/////////////////////////////////////////////////////////////////


        ////*** Ankita Completed ***///////////////////////////////////////////////////////////////////////////







        ////*** Vaishnavi working here ***////////////////////////////////////////////////////////////////


        ////*** Vaishanavi Completed ***/////////////////////////////////////////////////////////////////

    }
}

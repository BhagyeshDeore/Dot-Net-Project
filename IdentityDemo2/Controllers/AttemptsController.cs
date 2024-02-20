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
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json;
using IdentityDemo2.DTOs;

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
        /*public IActionResult AttemptProblemCodeIt()
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
           
        }*/



        ////*** Viraj Completed ***////////////////////////////////////////////////////////////////////












        ////*** Rushikesh working here ***/////////////////////////////////////////////////////////////
        /* [HttpPost] *OLD CODE*
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

            // Print the actual content of attempt.Code to the console for debugging
            Console.WriteLine($"Code to be submitted:\n{attempt.Code}");

            string input = problem.SampleInput;

            // Prepare data in the required JSON structure for the external API
            var requestData = new
            {
                language = "java",
                stdin = input,
                files = new[]
                {
                    new
                    {
                        name = "Main.java",
                        content = attempt.Code
                    }
                }
            };

            // Convert the request data to JSON
            string jsonData = JsonConvert.SerializeObject(requestData);

            // Print the request data to the console
            Console.WriteLine($"Request Data:\n{jsonData}");

            // Set the API endpoint URL
            string apiUrl = "https://onecompiler-apis.p.rapidapi.com/api/v1/run";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Set the necessary headers for the external API
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "47124e78c3msh49b2a0bec47e7dap1a5bd0jsne5bde89a75f5");
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "onecompiler-apis.p.rapidapi.com");

                    // Send a POST request
                    HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

                    // Print the response status code to the console
                    Console.WriteLine($"Response Status Code: {response.StatusCode}");

                    // Check the response
                    if (response.IsSuccessStatusCode)
                    {
                        // Successful request, handle the response
                        string responseData = await response.Content.ReadAsStringAsync();
                        // Print the response data to the console
                        Console.WriteLine($"Response Data:\n{responseData}");

                        // Assuming you want to return some data to the client
                        return RedirectToAction();
                    }
                    else
                    {
                        // Handle error
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        // Print the error response to the console
                        Console.WriteLine($"Error Response:\n{errorResponse}");

                        return BadRequest($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //POST: 
        [HttpPost]
        public async Task<IActionResult> attemptproblemCode()
        {
            Console.WriteLine("Hello from attempt");

            return View();
        }

        /*[HttpPost]
        public IActionResult AttemptSubmission([Bind("Code")] Attempt attempt, int ProblemId)
        {
            // Process the codeModel and problemId as needed
            // You can replace AttemptCodeModel with your actual model class

            // For demonstration purposes, creating a simple JSON object
            var result = new
            {
                Status = "Success",
                Message = "Custom method called successfully",
                ProblemId = ProblemId,
                Code = attempt.Code
            };

            Console.WriteLine( "Custome Method Result :" + result);

            // Return the JSON object
            return Json(result);
        }*/

        public class CodeModel
        {
            public string Code { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AttemptSubmission(int problemId, [FromBody] CodeModel model)
        {
            // Access model.Code for the code data
            Attempt attempt = new Attempt();
            attempt.Code = model.Code;

            //////
            attempt.Language = CodingLanguage.JAVA;
            attempt.SolvedStatus = SolvedStatus.Solved;
            attempt.Result = "PASS";
            attempt.ObtainedMarks = 10;
            attempt.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var problem = _context.Problemes.Find(problemId);
            attempt.ContestId = problem.ContestId;
            attempt.ProblemId = problemId;
            string input = problem.SampleInput;
            string expectedOutput = problem.SampleOutput; // ex

            Console.WriteLine("Submitted problem id  : " + attempt.ProblemId + " contest id " + attempt.ContestId);
            Console.WriteLine("Submitted  for user  id: " + attempt.ApplicationUserId);

            // Print the actual content of attempt.Code to the console for debugging
            Console.WriteLine($"Code to be submitted:\n{attempt.Code}");
            ///////
            ///

            // Call ExecuteCode and await the result
            ActionResult<string> response = await ExecuteCode(attempt.Code, input);

            var result = new
            {
                Status = "",
                Message = "", //stdoutput

            };

            try
            {
                
                // Deserialize the JSON string into an instance of ExecutionResponse
                ExecutionResponse executionResponse = JsonConvert.DeserializeObject<ExecutionResponse>(response.ToString());

                if(executionResponse.Stderr != null )
                {
                    result = new
                    {
                        Status = "Compilation Error",
                        Message = executionResponse.Stderr

                    };

                }
                else if(executionResponse.Exception != null)
                 {

                    result = new
                    {
                        Status = "Runtime Exception Occured",
                        Message = executionResponse.Exception 

                    };

                }
                else
                {
                    result = new
                    {
                        Status = "Success",
                        Message = executionResponse.Stdout

                    };
                }

                // Now you can access the properties of executionResponse
                
                // ... and so on

            }
            catch (JsonException ex)
            {
                // Handle the exception if the string is not a valid JSON
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }

            Console.WriteLine(response.ToString());


            

            Console.WriteLine("Custom Method Result: " + result);

            return Ok(result);
        }

        public async Task<ActionResult<string>> ExecuteCode(string Code, string input)
{
    // Prepare data in the required JSON structure for the external API
    var requestData = new
    {
        language = "java",
        stdin = input,
        files = new[]
        {
            new
            {
                name = "Main.java",
                content = Code
            }
        }
    };

    // Convert the request data to JSON
    string jsonData = JsonConvert.SerializeObject(requestData);

    // Print the request data to the console
    Console.WriteLine($"Request Data:\n{jsonData}");

    // Set the API endpoint URL
    string apiUrl = "https://onecompiler-apis.p.rapidapi.com/api/v1/run";

    try
    {
        using (HttpClient client = new HttpClient())
        {
            // Set the necessary headers for the external API
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "47124e78c3msh49b2a0bec47e7dap1a5bd0jsne5bde89a75f5");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "onecompiler-apis.p.rapidapi.com");

            // Send a POST request
            HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

            // Print the response status code to the console
            Console.WriteLine($"Response Status Code: {response.StatusCode}");

            // Check the response
            if (response.IsSuccessStatusCode)
            {
                // Successful request, handle the response
                string responseData = await response.Content.ReadAsStringAsync();
                // Print the response data to the console
                Console.WriteLine($"Response Data:\n{responseData}");

                // Deserialize the JSON directly into ExecutionResponse
                ExecutionResponse executionResponse = JsonConvert.DeserializeObject<ExecutionResponse>(responseData);

                // Check if there was an exception
                if (executionResponse.Exception != null)
                {
                    // Handle the exception
                    Console.WriteLine($"Exception occurred: {executionResponse.Exception}");
                    return BadRequest($"Error: {executionResponse.Exception}");
                }

                // Assuming you want to return some data to the client
                return Ok(executionResponse.Stdout);
            }
            else
            {
                // Handle error
                string errorResponse = await response.Content.ReadAsStringAsync();
                // Print the error response to the console
                Console.WriteLine($"Error Response:\n{errorResponse}");

                return BadRequest($"Error: {response.StatusCode}");
            }
        }
    }
    catch (Exception ex)
    {
        // Handle exception
        Console.WriteLine($"Exception: {ex.Message}");
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
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


        //// tried but this logic written in ContestController , This is for future refrence.
        
        // GET: Attempt/Leaderboard
        //public async Task<IActionResult> Leaderboard(int contestId)
        //{
        //    var attempts = await _context.Attempt
        //        .Include(a => a.ApplicationUser)
        //        .Where(a => a.ContestId == contestId)
        //        .ToListAsync();

        //    var leaderboard = attempts
        //        .GroupBy(a => a.ApplicationUser)
        //        .Select(g => new
        //        {
        //            StudentName = g.Key.UserName,
        //            TotalMarks = g.Sum(a => a.ObtainedMarks)
        //        })
        //        .OrderByDescending(x => x.TotalMarks)
        //        .ToList();

        //    return PartialView("Leaderboard", leaderboard);
        //}

        
        //public async Task<List<Attempt>> Leaderboard(int contestId)
        //{
        //    var leaderboardData = await _context.Attempt
        //        .Include(a => a.ApplicationUser)
        //        .Where(a => a.ContestId == contestId)
        //        .OrderByDescending(a => a.ObtainedMarks)
        //        .Take(10) 
        //        .ToListAsync();

        //    return leaderboardData;
        //}



        ////*** Bhagyesh Completed ***////////////////////////////////////////////////////////////////////////////










        ////*** Ankita working here ***/////////////////////////////////////////////////////////////////


        ////*** Ankita Completed ***///////////////////////////////////////////////////////////////////////////







        ////*** Vaishnavi working here ***////////////////////////////////////////////////////////////////


        ////*** Vaishanavi Completed ***/////////////////////////////////////////////////////////////////

    }
}

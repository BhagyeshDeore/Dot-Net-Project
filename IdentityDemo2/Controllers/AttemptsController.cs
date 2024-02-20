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
using NuGet.Protocol;
using Newtonsoft.Json.Linq;


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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> attemptproblem_partial_([Bind("Code")] Attempt attempt, int ProblemId)
        //{
        //    attempt.Language = CodingLanguage.JAVA;
        //    attempt.SolvedStatus = SolvedStatus.Solved;
        //    attempt.Result = "PASS";
        //    attempt.ObtainedMarks = 10;
        //    attempt.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var problem = _context.Problemes.Find(ProblemId);
        //    attempt.ContestId = problem.ContestId;
        //    attempt.ProblemId = ProblemId;

        //    Console.WriteLine("Submitted problem id  : " + attempt.ProblemId + " contest id " + attempt.ContestId);
        //    Console.WriteLine("Submitted  for user  id: " + attempt.ApplicationUserId);

        //    // Print the actual content of attempt.Code to the console for debugging
        //    Console.WriteLine($"Code to be submitted:\n{attempt.Code}");

        //    string input = problem.SampleInput;

        //    // Prepare data in the required JSON structure for the external API
        //    var requestData = new
        //    {
        //        language = "java",
        //        stdin = input,
        //        files = new[]
        //        {
        //            new
        //            {
        //                name = "Main.java",
        //                content = attempt.Code
        //            }
        //        }
        //    };

        //    // Convert the request data to JSON
        //    string jsonData = JsonConvert.SerializeObject(requestData);

        //    // Print the request data to the console
        //    Console.WriteLine($"Request Data:\n{jsonData}");

        //    // Set the API endpoint URL
        //    string apiUrl = "https://onecompiler-apis.p.rapidapi.com/api/v1/run";

        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            // Set the necessary headers for the external API
        //            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "47124e78c3msh49b2a0bec47e7dap1a5bd0jsne5bde89a75f5");
        //            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "onecompiler-apis.p.rapidapi.com");

        //            // Send a POST request
        //            HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

        //            // Print the response status code to the console
        //            Console.WriteLine($"Response Status Code: {response.StatusCode}");

        //            // Check the response
        //            if (response.IsSuccessStatusCode)
        //            {
        //                // Successful request, handle the response
        //                string responseData = await response.Content.ReadAsStringAsync();
        //                // Print the response data to the console
        //                Console.WriteLine($"Response Data:\n{responseData}");

        //                // Assuming you want to return some data to the client
        //                return RedirectToAction();
        //            }
        //            else
        //            {
        //                // Handle error
        //                string errorResponse = await response.Content.ReadAsStringAsync();
        //                // Print the error response to the console
        //                Console.WriteLine($"Error Response:\n{errorResponse}");

        //                return BadRequest($"Error: {response.StatusCode}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        Console.WriteLine($"Exception: {ex.Message}");
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        //}

        //POST: 
        [HttpPost]
        public async Task<IActionResult> attemptproblemCode()
        {
            Console.WriteLine("Hello from attempt");

            return View();
        }

        public class CodeModel
        {
            public string Code { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AttemptSubmission(int problemId, [FromBody] CodeModel model)
        {
            // Access model.Code for the code data
            var problem = _context.Problemes.Find(problemId);
            string input = problem.SampleInput;
            string expectedOutput = problem.SampleOutput; // ex

            // Call ExecuteCode and await the result
            string response = await ExecuteCode(model.Code, input);
            Console.WriteLine("response from excute code :");
            Console.WriteLine(response);

            var result = new
            {
                Status = "Network Failure or Internal Server error",
                Message = "Try again later!!" 
            };

            try
            {
                
                // Deserialize the JSON string into an instance of ExecutionResponse
                ExecutionResponse executionResponse = JsonConvert.DeserializeObject<ExecutionResponse>(response);
                Console.WriteLine(executionResponse.ToString);

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
                        Status = "Executed but Sample Testcases failed",
                        Message = executionResponse.Stdout

                    };
                    //sample output checking
                    if (checkSampleOutput(expectedOutput, executionResponse.Stdout)) {
                        result = new
                        {
                            Status = "Success : Sample Testcases passed but Hidden testcases failed",
                            Message = executionResponse.Stdout

                        };
                        string responseTestcases = await ExecuteCode(model.Code, problem.Testcase);
                        try
                        {

                            // Deserialize the JSON string into an instance of ExecutionResponse
                            ExecutionResponse executionResponseTestCases = JsonConvert.DeserializeObject<ExecutionResponse>(responseTestcases);
                            if (executionResponseTestCases.Stdout != null && checkSampleOutput( problem.ResultOfTestCase, executionResponseTestCases.Stdout)) {
                                
                                result = new
                                {
                                    Status = "Submitted : Sample Testcases and Hidden Testcases passed",
                                    Message = executionResponse.Stdout

                                };

                                Attempt attempt = new Attempt();
                                attempt.Code = model.Code;
                                attempt.Language = CodingLanguage.JAVA;
                                attempt.SolvedStatus = SolvedStatus.Solved;
                                attempt.Result = "PASS";
                                attempt.ObtainedMarks = problem.Marks;
                                attempt.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                                attempt.ContestId = problem.ContestId;
                                attempt.ProblemId = problemId;
                                Console.WriteLine("Submitted problem id  : " + attempt.ProblemId + " contest id " + attempt.ContestId);
                                Console.WriteLine("Submitted  for user  id: " + attempt.ApplicationUserId);

                                await createAttempt(attempt);


                            }
                        }
                        catch (JsonException ex)
                        {
                            // Handle the exception if the string is not a valid JSON
                            Console.WriteLine($"Error parsing JSON: {ex.Message}");
                        }

                    }
                   
                }

            }
            catch (JsonException ex)
            {
                // Handle the exception if the string is not a valid JSON
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }

            Console.WriteLine("returning Custom Method Result: " + result);
            return Ok(result);
        }
        //System.out.pritnln("hello");
        public async Task<string> ExecuteCode(string Code, string input)
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
                        // Read the response content as a string
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("**********");
                        Console.WriteLine(responseData);
                        return responseData;
                    }
                    else
                    {
                        // Handle unsuccessful response
                        string errorMessage = $"HTTP request failed with status code: {response.StatusCode}";
                        Console.WriteLine(errorMessage);
                        throw new Exception(errorMessage); // Throw an exception or return a default value
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Exception: {ex.Message}");
                return "Internal Server Error: "+ex.Message;
            }
        }

        public Boolean checkSampleOutput( String sampleOutput, String Codeoutput) { 
            return sampleOutput.Trim() == Codeoutput.Trim();
        }

        public async Task createAttempt( Attempt attempt) {
            _context.Add(attempt);
            await _context.SaveChangesAsync();
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

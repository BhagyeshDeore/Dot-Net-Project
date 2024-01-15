using IdentityDemo2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IdentityDemo2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        ////*** Viraj working here ***/////////////////////////////////////////////////////////////////

        public async Task<IActionResult> adminDashBoard() {
            return View();
        }
        ////*** Viraj Completed ***////////////////////////////////////////////////////////////////////












        ////*** Rushikesh working here ***/////////////////////////////////////////////////////////////


        ////*** Rushikesh Completed ***////////////////////////////////////////////////////////////////














        ////*** Bhagyesh working here ***////////////////////////////////////////////////////////////////


        ////*** Bhagyesh Completed ***////////////////////////////////////////////////////////////////////////////










        ////*** Ankita working here ***/////////////////////////////////////////////////////////////////


        ////*** Ankita Completed ***///////////////////////////////////////////////////////////////////////////







        ////*** Vaishnavi working here ***////////////////////////////////////////////////////////////////


        ////*** Vaishanavi Completed ***/////////////////////////////////////////////////////////////////

    }
}

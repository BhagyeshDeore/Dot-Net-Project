using IdentityDemo2.DTOs;
using IdentityDemo2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo2.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private IPasswordHasher<ApplicationUser> _passwordHasher;

        public AdminController(RoleManager<IdentityRole> roleManager,
                          UserManager<ApplicationUser> userManager,
                         IPasswordHasher<ApplicationUser> passwordHash)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _passwordHasher = passwordHash;



        }
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleStore roleStore)
        {
            var alreadyAdded = await _roleManager.RoleExistsAsync(roleStore.RoleName);

            if (!alreadyAdded)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleStore.RoleName));
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }





        ////*** Viraj working here ***/////////////////////////////////////////////////////////////////


        ////*** Viraj Completed ***////////////////////////////////////////////////////////////////////












        ////*** Rushikesh working here ***/////////////////////////////////////////////////////////////


        ////*** Rushikesh Completed ***////////////////////////////////////////////////////////////////














        ////*** Bhagyesh working here ***////////////////////////////////////////////////////////////////


        ////*** Bhagyesh Completed ***////////////////////////////////////////////////////////////////////////////










        ////*** Ankita working here ***/////////////////////////////////////////////////////////////////


        ////*** Ankita Completed ***///////////////////////////////////////////////////////////////////////////









        ////*** Vaishnavi working here ***////////////////////////////////////////////////////////////////

        //list of teachers
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult ListTeacher()
        {
            // Assuming "TEACHER" is the role you want to filter by
            var teacherUsers = _userManager.GetUsersInRoleAsync("TEACHER").Result;

            return View(teacherUsers);
        }
        //list of student
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult ListStudent()
        {
            // Assuming "TEACHER" is the role you want to filter by
            var studentUsers = _userManager.GetUsersInRoleAsync("STUDENT").Result;

            return View(studentUsers);
        }


        //udate password and mail of TEACHER

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("ListTeacher");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            ApplicationUser teacherUser = await _userManager.FindByIdAsync(id);
            if (teacherUser != null)
            {
                if (!string.IsNullOrEmpty(email))
                    teacherUser.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    teacherUser.PasswordHash = _passwordHasher.HashPassword(teacherUser, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(teacherUser);
                    if (result.Succeeded)
                        return RedirectToAction("ListTeacher");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(teacherUser);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }


        //udate password and mail of student

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateStudent(string id)
        {
            ApplicationUser users = await _userManager.FindByIdAsync(id);
            if (users != null)
                return View(users);
            else
                return RedirectToAction("ListStudent");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudent(string id, string email, string password)
        {
            ApplicationUser studentUsers = await _userManager.FindByIdAsync(id);
            if (studentUsers != null)
            {
                if (!string.IsNullOrEmpty(email))
                    studentUsers.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    studentUsers.PasswordHash = _passwordHasher.HashPassword(studentUsers, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult results = await _userManager.UpdateAsync(studentUsers);
                    if (results.Succeeded)
                        return RedirectToAction("ListStudent");
                    else
                        Errorss(results);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return RedirectToAction("ListStudent");
        }

        private void Errorss(IdentityResult results)
        {
            foreach (IdentityError errorr in results.Errors)
                ModelState.AddModelError("", errorr.Description);
        }
        ////*** Vaishanavi Completed ***/////////////////////////////////////////////////////////////////////////



    }
}

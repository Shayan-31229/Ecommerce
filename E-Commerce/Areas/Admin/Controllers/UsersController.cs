using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Models.VMs;
using E_Commerce.Repository.Interfaces;
using E_Commerce.Repository.Interfaces; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace E_Commerce.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService Mail_Service = null;


        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMailService _MailService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _roleManager = roleManager;
            Mail_Service = _MailService;

        }

        [Route("{culture:length(2)}/Users/profile/{id?}/{tab?}")]
        public async Task<IActionResult> ProfileAsync(Guid? id, string? tab)
        {
            ApplicationUser? user;


            if (!id.HasValue)
            {
                user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user = await _context.ApplicationUsers
                        .Include(g => g.Gender)
                        .Include(n => n.Nationality)
                        .Include(l => l.loginLogs.OrderByDescending(u => u.id).Take(50))
                        .FirstOrDefaultAsync(u => u.Id == user.Id);


                }
            }
            else
            {
                user = await _context.ApplicationUsers
                    .Include(g => g.Gender)
                    .Include(n => n.Nationality)
                    .Include(l => l.loginLogs.OrderByDescending(u => u.id).Take(50))
                    .FirstOrDefaultAsync(u => u.Id == id.ToString());
            }

            if (user == null)
            {
                return NotFound($"Unable to load user");
            }


            IList<string> assignedRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();



            VMUserWithRoles userWithRoles = new VMUserWithRoles() { User = user, Roles = roles, AssignedRoles = assignedRoles };


            ViewBag.Genders = _context.Genders.Select(g => new SelectListItem
            {
                Value = g.id.ToString(),
                Text = g.title
            }).ToList();

            ViewBag.Nationalities = _context.Nationalities.Select(g => new SelectListItem
            {
                Value = g.id.ToString(),
                Text = g.title
            }).ToList();

            return View(userWithRoles);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<string> update(ApplicationUser user)
        {
            try
            {
                ApplicationUser dbUser = await _userManager.FindByIdAsync(user.Id.ToString());
                if (dbUser == null)
                {
                    throw new Exception("Invalid user");
                }
                dbUser.IsAdmin = user.IsAdmin;
                dbUser.Email = user.Email;
                dbUser.FullName = user.FullName;
                dbUser.address = user.address;
                dbUser.gender_id = user.gender_id;
                dbUser.nationality_id = user.nationality_id;
                dbUser.dob = user.dob;
                // _context.Update(dbUser);
                IdentityResult result = await _userManager.UpdateAsync(dbUser);
                if (!result.Succeeded)
                {
                    string errorString = string.Join("<br>", result.Errors.Select(e => e.Description));
                    throw new Exception(errorString);
                }

                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
            ApplicationUser? user;


            if (!id.HasValue)
            {
                user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user = await _context.ApplicationUsers
                        .Include(g => g.Gender)
                        .Include(n => n.Nationality)
                        .FirstOrDefaultAsync(u => u.Id == user.Id);


                }
            }
            else
            {
                user = await _context.ApplicationUsers
                    .Include(g => g.Gender)
                    .Include(n => n.Nationality)
                    .FirstOrDefaultAsync(u => u.Id == id.ToString());
            }

            if (user == null)
            {
                return NotFound($"Unable to load user");
            }


            IList<string> assignedRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();
            VMUserWithRoles userWithRoles = new VMUserWithRoles() { User = user, Roles = roles, AssignedRoles = assignedRoles };

            return View(userWithRoles);
        }

        [HttpPost]
        [Route("{culture:length(2)}/users/reset_password/{userId}")]
        public async Task<IActionResult> resetPassword(int userId, [FromBody] List<string> passwords)
        {
            try
            {
                if (!User.IsInRole("sa"))
                {
                    throw new Exception("You are not authorized to perform this action");

                }

                ApplicationUser? user = user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    throw new Exception("Invalid user");
                }
                if (passwords.Count != 3)
                {
                    throw new Exception("Invalid data provided");
                }

                if (passwords[0] != passwords[1])
                {
                    throw new Exception("Both passwords do not match");
                }


                if (passwords[0] != null && passwords[0].Length < 4)
                {
                    throw new Exception("Password is too simple");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                string p = passwords[0];
                var result = await _userManager.ResetPasswordAsync(user, code, p);
                if (!result.Succeeded)
                {
                    throw new Exception("could not reset the password");
                }
                if (passwords[2] == "1")//send email
                {
                    sendPwdEmail(user, p);
                }
                return Ok("1");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal server error with message
            }
        }

        private string sendPwdEmail(ApplicationUser user, string pwd)
        {
            try
            {
                var link = Request.Scheme + "://" + Request.Host.Host + "/";
                VmMailData maildata = new VmMailData();

                maildata.EmailToId = user.Email.ToString();
                maildata.EmailSubject = "Password reset";
                maildata.EmailBody = $"Your password has been reset by admin<br>" +
                    $"Link:<a href='{link}' target='_blank'>{link}</a><br>" +
                    $"username: {user.UserName.ToString()}<br>" +
                    $"Password: {pwd}<br>" +
                    $"We strongly recommend to change your password and keep it secure.<br><br>";

                Mail_Service.SendMail(maildata);
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPost]
        [Route("{culture:length(2)}/users/update_roles/{userId}")]
        public async Task<IActionResult> UpdateRoles(int userId, [FromBody] List<string> rolesToAssign)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID or role IDs");
            }

            try
            {
                ApplicationUser? user = user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    throw new Exception("Invalid user");
                }

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    // Remove existing roles
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        return BadRequest(removeResult.Errors.Select(e => e.Description));
                    }

                    // Add new roles
                    if (rolesToAssign != null)
                    {
                        var result = await _userManager.AddToRolesAsync(user, rolesToAssign.Select(r => r.ToString()));
                        if (!result.Succeeded)
                        {
                            return BadRequest(result.Errors.Select(e => e.Description));
                        }
                    }

                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return Ok("1"); // Success indicator
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal server error with message
            }
        }
    }
}

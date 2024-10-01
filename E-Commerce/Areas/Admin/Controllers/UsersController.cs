using E_Commerce.Data;
using E_Commerce.Models.VMs;
using E_Commerce.Repository.Interfaces; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Drawing;

namespace Identity5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecordCountService _recordCountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService Mail_Service = null; 


        public UsersController(ApplicationDbContext context, IRecordCountService recordCountService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMailService _MailService)
        {
            _context = context;
            _recordCountService = recordCountService;
            _userManager = userManager;
            _roleManager = roleManager;
            Mail_Service = _MailService; 
        }



        // GET: Admin/Memberships
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Admin/Memberships
        public async Task<IActionResult> getdata()
        {
            var queryString = Request.QueryString.ToString();
            IDictionary<string, StringValues> qs = QueryHelpers.ParseQuery(queryString);
            var entityType = _context.Model.FindEntityType(typeof(ApplicationUser));
            //var entityType = typeof(DTMembership);
            var cols = entityType.GetProperties().Select(p => p.Name).ToArray();


            string qry = "select * from " + entityType.GetTableName() + " where 1=1 ";

            string mainConds = "";

            //ordering
            string orderBy = "";
            if (qs.ContainsKey("iSortingCols") && !string.IsNullOrEmpty(qs["iSortingCols"]))
            {
                var numOfSortCols = Convert.ToInt32(qs["iSortingCols"]);
                List<string> dtOrders = new List<string>();
                for (int i = 0; i < numOfSortCols; i++)
                {
                    dtOrders.Add(qs["mDataProp_" + Convert.ToInt32(qs["iSortCol_" + i])] + " " + qs["sSortDir_" + i]);
                }
                if (dtOrders.Count > 0)
                {
                    orderBy = " ORDER BY " + string.Join(",", dtOrders);
                }
            }

            //main filter

            List<string> filterCondsList = new List<string>();
            if (qs.ContainsKey("sSearch") && !string.IsNullOrEmpty(qs["sSearch"]))
            {
                var searchTerm = qs["sSearch"];
                foreach (var col in cols)
                {
                    string thisCond = " " + col + " LIKE '%" + searchTerm + "%'";
                    filterCondsList.Add(thisCond);
                }
            }
            string filterConds = "";
            if (filterCondsList.Count > 0)
            {
                filterConds = "(" + string.Join(" OR ", filterCondsList) + ")";
            }


            //column level filtering
            string colConds = "";
            int qsColumns = Convert.ToInt32(qs["iColumns"]);
            for (int i = 0; i < qsColumns; i++)
            {
                var searchableKey = "bSearchable_" + i;
                var cSearchTermIndx = "sSearch_" + i;

                if (qs.ContainsKey(searchableKey) && qs[searchableKey] == "true" && qs.ContainsKey(cSearchTermIndx) && !string.IsNullOrEmpty(qs[cSearchTermIndx]))
                {
                    colConds += " AND " + qs["mDataProp_" + i] + " LIKE '%" + qs[cSearchTermIndx] + "%'";
                }
            }
            if (colConds != "")
            {
                colConds = colConds.Substring(4);
            }


            //prepare execute sql query
            string sqlQry = qry + mainConds;
            //int totalRecords = await getTototalRecordsAsync(sqlQry);
            int totalRecords = await _recordCountService.GetTotalRecordsAsync(sqlQry);

            if (!string.IsNullOrEmpty(filterConds))
            {
                sqlQry += " AND " + filterConds;
            }

            if (!string.IsNullOrEmpty(colConds))
            {
                sqlQry += " AND " + colConds;
            }
            int recordsFiltered = await _recordCountService.GetTotalRecordsAsync(sqlQry);
            if (!string.IsNullOrEmpty(orderBy))
            {
                sqlQry += orderBy;
            }

            int displaySkip = Convert.ToInt32(qs["iDisplayStart"]);
            int displayLength = Convert.ToInt32(qs["iDisplayLength"]);

            sqlQry += " OFFSET " + displaySkip + " ROWS FETCH NEXT " + displayLength + " ROWS ONLY";

            var gates = _context.Database.SqlQueryRaw<DTUser>(sqlQry).ToList();

            DataTableResponse<DTUser> response = new DataTableResponse<DTUser>();
            response.draw = qs["sEcho"];
            response.recordsTotal = totalRecords;
            response.recordsFiltered = recordsFiltered;
            response.data = gates;

            var json = JsonConvert.SerializeObject(response);

            return Content(json, "application/json");
        }

        [Route("[area]/Users/profile/{id?}/{tab?}")]
        public async Task<IActionResult> ProfileAsync(string? id, string? tab)
        {
            ApplicationUser? user;


            if (id != null)
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
                    .FirstOrDefaultAsync(u => u.Id == id);
            }

            if (user == null)
            {
                return NotFound($"Unable to load user");
            }


            IList<string> assignedRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();

            

            VMUserWithRoles userWithRoles = new VMUserWithRoles()
            {
                User = user,
                Roles = roles,
                AssignedRoles = assignedRoles
            };


            ViewBag.Genders = _context.Genders.Where(e => e.status == 1).Select(g => new SelectListItem
            {
                Value = g.id.ToString(),
                Text = g.title
            }).ToList();

            ViewBag.Nationalities = _context.Nationalities.Where(e => e.status == 1).Select(g => new SelectListItem
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



        [HttpPost]
        [Route("[area]/users/reset_password/{userId}")]
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
        [Route("[area]/users/update_roles/{userId}")]
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
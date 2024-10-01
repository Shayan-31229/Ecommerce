using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Repository.Interfaces;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.WebUtilities;
using E_Commerce.Models.VMs;
using Newtonsoft.Json;
using Microsoft.Extensions.Localization;

using E_Commerce.Data;
using E_Commerce.Models;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GendersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecordCountService _recordCountService;

        public GendersController(ApplicationDbContext context, IRecordCountService recordCountService)
        {
            _context = context;
            _recordCountService = recordCountService; 
        }

        // GET: Admin/Genders
        public async Task<IActionResult> Index()
        {     
            return View();
        }

    // GET: Admin/Genders
        public async Task<IActionResult>getdata()
        {
        var queryString = Request.QueryString.ToString();
        IDictionary<string, StringValues> qs = QueryHelpers.ParseQuery(queryString);
        var entityType = _context.Model.FindEntityType(typeof(Gender));
        //var entityType = typeof(DTGender);
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

        var gates = _context.Database.SqlQueryRaw<DTGender>(sqlQry).ToList();

        DataTableResponse<DTGender> response = new DataTableResponse<DTGender>();
        response.draw = qs["sEcho"];
        response.recordsTotal = totalRecords;
        response.recordsFiltered = recordsFiltered;
        response.data = gates;

        var json = JsonConvert.SerializeObject(response);

        return Content(json, "application/json");
        }

        // GET: Admin/Genders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Genders
                .FirstOrDefaultAsync(m => m.id == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // GET: Admin/Genders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Genders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gender gender)
        {
            gender.created = DateTime.Now;
            gender.created_by = Guid.Parse(User.Identity.Id());
            if (ModelState.IsValid)
            {
                _context.Add(gender);
                await _context.SaveChangesAsync();
                this.Flash("New record created successfully", "success");
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        // GET: Admin/Genders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Genders.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        // POST: Admin/Genders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gender gender)
        {
            if (id != gender.id)
            {
                return NotFound();
            }

            gender.modified = DateTime.Now;
            gender.modified_by = Guid.Parse(User.Identity.Id());

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gender);
                    this.Flash("Record updated successfully", "success");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderExists(gender.id))
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
            return View(gender);
        }

        // GET: Admin/Genders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Genders
                .FirstOrDefaultAsync(m => m.id == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // POST: Admin/Genders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gender = await _context.Genders.FindAsync(id);
            if (gender != null)
            {
                _context.Genders.Remove(gender);
            }

            await _context.SaveChangesAsync();
            this.Flash("Record deleted successfully", "success");
            return RedirectToAction(nameof(Index));
        }

        private bool GenderExists(int id)
        {
            return _context.Genders.Any(e => e.id == id);
        }
    } 
}



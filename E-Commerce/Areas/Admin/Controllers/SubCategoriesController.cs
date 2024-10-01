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
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecordCountService _recordCountService; 

        public SubCategoriesController(ApplicationDbContext context, IRecordCountService recordCountService)
        {
            _context = context;
            _recordCountService = recordCountService; 
        }

        // GET: Admin/SubCategories
        public async Task<IActionResult> Index()
        {     
            return View();
        }

    // GET: Admin/SubCategories
        public async Task<IActionResult>getdata()
        {
        var queryString = Request.QueryString.ToString();
        IDictionary<string, StringValues> qs = QueryHelpers.ParseQuery(queryString);
        //var entityType = _context.Model.FindEntityType(typeof(SubCategory));
        var entityType = typeof(DTSubCategory);
        var cols = entityType.GetProperties().Select(p => p.Name).ToArray();


        string qry = "select * from dt_SubCategories where 1=1 ";

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

        var gates = _context.Database.SqlQueryRaw<DTSubCategory>(sqlQry).ToList();

        DataTableResponse<DTSubCategory> response = new DataTableResponse<DTSubCategory>();
        response.draw = qs["sEcho"];
        response.recordsTotal = totalRecords;
        response.recordsFiltered = recordsFiltered;
        response.data = gates;

        var json = JsonConvert.SerializeObject(response);

        return Content(json, "application/json");
        }

        // GET: Admin/SubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // GET: Admin/SubCategories/Create
        public IActionResult Create()
        {
            ViewData["category_id"] = new SelectList(_context.Categories, "id", "title");
            return View();
        }

        // POST: Admin/SubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategory subCategory)
        {
            subCategory.created = DateTime.Now;
        subCategory.created_by = Guid.Parse(User.Identity.Id());
            if (ModelState.IsValid)
            {
                _context.Add(subCategory);
                await _context.SaveChangesAsync();
                this.Flash("New record created successfully", "success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["category_id"] = new SelectList(_context.Categories, "id", "title", subCategory.category_id);
            return View(subCategory);
        }

        // GET: Admin/SubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            ViewData["category_id"] = new SelectList(_context.Categories, "id", "title", subCategory.category_id);
            return View(subCategory);
        }

        // POST: Admin/SubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategory subCategory)
        {
            if (id != subCategory.id)
            {
                return NotFound();
            }

            subCategory.modified = DateTime.Now;
                subCategory.modified_by = Guid.Parse(User.Identity.Id());

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCategory);
                    this.Flash("Record updated successfully", "success");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategoryExists(subCategory.id))
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
            ViewData["category_id"] = new SelectList(_context.Categories, "id", "title", subCategory.category_id);
            return View(subCategory);
        }

        // GET: Admin/SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // POST: Admin/SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
            }

            await _context.SaveChangesAsync();
            this.Flash("Record deleted successfully", "success");
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(e => e.id == id);
        }
    } 
}



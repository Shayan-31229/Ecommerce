using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Repository.Interfaces;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.WebUtilities;
using E_Commerce.Models.VMs;
using Newtonsoft.Json;

using E_Commerce.Data;
using E_Commerce.Models;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EndCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecordCountService _recordCountService; 

        public EndCategoriesController(ApplicationDbContext context, IRecordCountService recordCountService)
        {
            _context = context;
            _recordCountService = recordCountService; 
        }

        // GET: Admin/EndCategories
        public async Task<IActionResult> Index()
        {     
            return View();
        }

    // GET: Admin/EndCategories
        public async Task<IActionResult>getdata()
        {
        var queryString = Request.QueryString.ToString();
        IDictionary<string, StringValues> qs = QueryHelpers.ParseQuery(queryString);
        //var entityType = _context.Model.FindEntityType(typeof(EndCategory));
        var entityType = typeof(DTEndCategory);
        var cols = entityType.GetProperties().Select(p => p.Name).ToArray();


        string qry = "select * from dt_EndCategories where 1=1 ";

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

        var records = _context.Database.SqlQueryRaw<DTEndCategory>(sqlQry).ToList();

        DataTableResponse<DTEndCategory> response = new DataTableResponse<DTEndCategory>();
        response.draw = qs["sEcho"];
        response.recordsTotal = totalRecords;
        response.recordsFiltered = recordsFiltered;
        response.data = records;

        var json = JsonConvert.SerializeObject(response);

        return Content(json, "application/json");
        }

        // GET: Admin/EndCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endCategory = await _context.EndCategories
                .Include(e => e.SubCategory)
                .FirstOrDefaultAsync(m => m.id == id);
            if (endCategory == null)
            {
                return NotFound();
            }

            return View(endCategory);
        }

        // GET: Admin/EndCategories/Create
        public IActionResult Create()
        {
            ViewData["sub_category_id"] = new SelectList(_context.SubCategories, "id", "title");
            return View();
        }

        // POST: Admin/EndCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EndCategory endCategory)
        {
            endCategory.created = DateTime.Now;
        endCategory.created_by = Guid.Parse(User.Identity.Id());
            if (ModelState.IsValid)
            {
                _context.Add(endCategory);
                await _context.SaveChangesAsync();
                this.Flash("New record created successfully", "success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["sub_category_id"] = new SelectList(_context.SubCategories, "id", "title", endCategory.sub_category_id);
            return View(endCategory);
        }

        // GET: Admin/EndCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endCategory = await _context.EndCategories.FindAsync(id);
            if (endCategory == null)
            {
                return NotFound();
            }
            ViewData["sub_category_id"] = new SelectList(_context.SubCategories, "id", "title", endCategory.sub_category_id);
            return View(endCategory);
        }

        // POST: Admin/EndCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EndCategory endCategory)
        {
            if (id != endCategory.id)
            {
                return NotFound();
            }

            endCategory.modified = DateTime.Now;
                endCategory.modified_by = Guid.Parse(User.Identity.Id());

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endCategory);
                    this.Flash("Record updated successfully", "success");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EndCategoryExists(endCategory.id))
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
            ViewData["sub_category_id"] = new SelectList(_context.SubCategories, "id", "title", endCategory.sub_category_id);
            return View(endCategory);
        }

        // GET: Admin/EndCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endCategory = await _context.EndCategories
                .Include(e => e.SubCategory)
                .FirstOrDefaultAsync(m => m.id == id);
            if (endCategory == null)
            {
                return NotFound();
            }

            return View(endCategory);
        }

        // POST: Admin/EndCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endCategory = await _context.EndCategories.FindAsync(id);
            if (endCategory != null)
            {
                _context.EndCategories.Remove(endCategory);
            }

            await _context.SaveChangesAsync();
            this.Flash("Record deleted successfully", "success");
            return RedirectToAction(nameof(Index));
        }

        private bool EndCategoryExists(int id)
        {
            return _context.EndCategories.Any(e => e.id == id);
        }
    } 
}



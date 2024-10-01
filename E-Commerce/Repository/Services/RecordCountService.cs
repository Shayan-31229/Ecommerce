using E_Commerce.Data;
using E_Commerce.Models.VMs;
using E_Commerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services
{
    public class RecordCountService : IRecordCountService
    {
        private readonly ApplicationDbContext _context;

        public RecordCountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalRecordsAsync(string query)
        {
            try
            {
                string sql = $"select count(*) as total from ({query}) AS SubQuery";
                VMSqlTotal? totalObj = await _context.Database.SqlQueryRaw<VMSqlTotal?>(sql).FirstOrDefaultAsync();
                return totalObj.total ?? 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}

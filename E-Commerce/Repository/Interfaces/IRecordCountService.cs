namespace E_Commerce.Repository.Interfaces
{
    public interface IRecordCountService
    {
        Task<int> GetTotalRecordsAsync(string query);
    }
}
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_Commerce.Models.VMs
{
    public class DatatableRequest
    {
        public int sEcho { get; set; }
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public int iColumns { get; set; }
        public string sColumns { get; set; }
        //columns
        public string[] mDataProp { get; set; }
        public bool[] sSearch { get; set; }
        public bool[] bRegex { get; set; }
        public bool[] bSearchable { get; set; }
        public bool[] bSortable { get; set; }
        //order
        public int[] iSortCol { get; set; }
        public string[] sSortDir { get; set; }
        //search
        //public string sSearch { get; set; }
        //public bool bRegex { get; set; }
        public int iSortingCols { get; set; }
    }
    public class Column
    {
        public string[] mDataProp { get; set; }
        public bool[] sSearch { get; set; }
        public bool[] bRegex { get; set; }
        public bool[] bSearchable { get; set; }
        public bool[] bSortable { get; set; }
    }

    public class Order
    {
        public int[] iSortCol { get; set; }
        public string[] sSortDir { get; set; }
    }

    public class Search
    {
        public string sSearch { get; set; }
        public bool bRegex { get; set; }
    }

    public class DataTableResponse<T>
    {
        public string draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }

    }
    //public static class ModelHelper
    //{
    //    public static IEnumerable<PropertyInfo> GetModelProperties<T>()
    //    {
    //        return typeof(T).GetProperties();
    //    }
    //}

    public static class ModelHelper
    {
        public static async Task<IEnumerable<string>> GetColumnNames<T>(DbContext context) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            return entityType.GetProperties().Select(p => p.Name);
        }
    }
}

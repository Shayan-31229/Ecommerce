namespace E_Commerce.Models.VMs
{
    public class AjaxResponse<T>
    {
        public int Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}

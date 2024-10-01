using E_Commerce.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class LoginLog
    {
        public int id { get; set; }
        [ForeignKey(nameof(User))]
        public string user_id { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public string username { get; set; }
        public string? ip { get; set; }
        public int loggedin { get; set; }
        public string? message { get; set; }
        public string? pre_ids { get; set; }
        public DateTime created { get; set; }
    }
}

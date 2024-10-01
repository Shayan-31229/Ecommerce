using E_Commerce.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace E_Commerce.Models
{
    public class Gender
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string? title { get; set; }

        public int sort { get; set; }

        public int status { get; set; }

        public DateTime created { get; set; }

        public Guid created_by { get; set; }

        public DateTime? modified { get; set; }

        public Guid? modified_by { get; set; }
         
        public ICollection<ApplicationUser>? Users { get; set; }
    }
}

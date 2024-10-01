using E_Commerce.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class SubCategory
    {
        public int id { get; set; }

        [ForeignKey(nameof(Category))]
        public int category_id { get; set; }
        public virtual Category? Category { get; set; }

        [Required]
        [StringLength(255)]
        public string? title { get; set; }

        public int sort { get; set; }

        public int status { get; set; }

        public DateTime created { get; set; }

        public Guid created_by { get; set; }

        public DateTime? modified { get; set; }

        public Guid? modified_by { get; set; }

        public ICollection<EndCategory>? EndCategories { get; set; }
    }
}

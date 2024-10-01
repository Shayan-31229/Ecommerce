using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class EndCategory
    {
        public int id { get; set; }

        [ForeignKey(nameof(SubCategory))]
        public int sub_category_id { get; set; }
        public virtual SubCategory? SubCategory { get; set; }

        [Required]
        [StringLength(255)]
        public string? title { get; set; }

        public int sort { get; set; }

        public int status { get; set; }

        public DateTime created { get; set; }

        public Guid created_by { get; set; }

        public DateTime? modified { get; set; }

        public Guid? modified_by { get; set; }

        public ICollection<Item>? Items { get; set; }
    }
}

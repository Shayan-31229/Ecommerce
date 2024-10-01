using E_Commerce.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Item
    {
        public int id { get; set; }

        [ForeignKey(nameof(EndCategory))]
        public int end_category_id { get; set; }
        public virtual EndCategory? EndCategory { get; set; }
        [Required]
        public string title { get; set; }
        public string barcode { get; set; }
        public  double old_price { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public int qty { get; set; }

        [StringLength(255)]
        public string? featured_pic { get; set; }
        public string? description { get; set; }
         
        public string? short_description { get; set; }
        public string? features { get; set; }
        public string? terms_conditions { get; set; }

        public int hits { get; set; }
        [Required]
        public int is_featured { get; set; }
        public int sort { get; set; }

        public int status { get; set; }
        public string? remarks { get; set; }

        public DateTime created { get; set; }

        public Guid created_by { get; set; }

        public DateTime? modified { get; set; }

        public Guid? modified_by { get; set; }

        public ICollection<ItemPhoto>? ItemPhotos { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class ItemPhoto
    {
        public Guid id { get; set; }

        [ForeignKey(nameof(Item))]
        public int item_id { get; set; }
        public virtual Item? Item { get; set; }
         
        public string? title { get; set; }
        [Required]
        public string pic { get; set; }

        public int sort { get; set; }

        public int status { get; set; }

        public DateTime created { get; set; }

        public Guid created_by { get; set; }

        public DateTime? modified { get; set; }

        public Guid? modified_by { get; set; }

    }
}

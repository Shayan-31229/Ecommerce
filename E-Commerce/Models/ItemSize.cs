using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class ItemSize
    {
        public int id { get; set; }
        [ForeignKey(nameof(Item))]
        public int item_id { get; set; }
        public virtual Item? Item { get; set; }


        [ForeignKey(nameof(Size))]
        public int size_id { get; set; }
        public virtual Size? Size { get; set; }
    }
}

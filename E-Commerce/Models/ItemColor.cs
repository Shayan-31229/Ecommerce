using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class ItemColor
    {
        public int id { get; set; }
        [ForeignKey(nameof(Item))]
        public int item_id { get; set; }
        public virtual Item? Item { get; set; }


        [ForeignKey(nameof(Color))]
        public int color_id { get; set; }
        public virtual Color? Color { get; set; }
    }
}

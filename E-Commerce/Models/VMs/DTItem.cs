using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models.VMs
{
  public class DTItem
  {
    public int id { get; set; }

     
    public int? end_category_id { get; set; }
    public string? end_category_title { get; set; }

    public int? sub_category_id { get; set; }
    public string? sub_category_title { get; set; }

    public int? category_id { get; set; }
    public string? category_title { get; set; }

    public string title { get; set; }
    public string barcode { get; set; }
    public double old_price { get; set; }
    [Required]
    public double price { get; set; }
    [Required]
    public int qty { get; set; } 
    public string featured_pic { get; set; }
    public string? description { get; set; }

    public string? short_description { get; set; }
    public string? features { get; set; }
    public string? terms_conditions { get; set; }

    public int hits { get; set; } 
    public int is_featured { get; set; }
    public int sort { get; set; }

    public int status { get; set; }
    public string? remarks { get; set; }

    public DateTime created { get; set; }

    public Guid created_by { get; set; }

    public DateTime? modified { get; set; }

    public Guid? modified_by { get; set; }

  }
}

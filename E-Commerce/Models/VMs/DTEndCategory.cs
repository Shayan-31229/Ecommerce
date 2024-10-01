using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models.VMs
{
  public class DTEndCategory
  {
    public int id { get; set; } 
    public int? sub_category_id { get; set; }
    public string? sub_category_title { get; set; }
    public int? category_id { get; set; }
    public string? category_title { get; set; }
    public string? title { get; set; }

    public int sort { get; set; }

    public int status { get; set; }

    public DateTime created { get; set; }

    public Guid created_by { get; set; }

    public DateTime? modified { get; set; }

    public Guid? modified_by { get; set; }

  }
}

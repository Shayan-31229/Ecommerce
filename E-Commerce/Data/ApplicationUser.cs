using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Data
{
  public class ApplicationUser : IdentityUser
  {
    [Display(Name = "Full Name"), Required, MaxLength(120)]
    public String FullName { get; set; } = String.Empty;

    [Display(Name = "Last Login") ]
    public DateTime? LastLogin { get; set; }

    [Display(Name = "Member Since")]
    public DateTime MemberSince { get; set; } = DateTime.Now;

    [Display(Name = "Status")]
    public Boolean IsLocked { get; set; }

    [Display(Name = "Authorization")]
    public Boolean IsAdmin { get; set; }

    [ForeignKey(nameof(Gender))]
    public int? gender_id { get; set; }

    public virtual Gender? Gender { get; set; } // Navigation property
    [ForeignKey(nameof(Nationality))]
    public int? nationality_id { get; set; }

    public virtual Nationality? Nationality { get; set; } // Navigation property
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateOnly? dob { get; set; }

    public string? address   { get; set; }
    public string? dp { get; set; }
    public int status { get; set; }
    public string? remarks { get; set; }

    public DateTime created { get; set; }

    public int created_by { get; set; }

    public DateTime? modified { get; set; }

    public int? modified_by { get; set; }

    }
}

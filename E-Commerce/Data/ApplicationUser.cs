using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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

  }
}

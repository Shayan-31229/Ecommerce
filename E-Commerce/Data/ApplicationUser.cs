using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Data
{
  public class ApplicationUser : IdentityUser
  {
    public String FullName { get; set; } = String.Empty;
    public DateTime LastLogin { get; set; }
  }
}

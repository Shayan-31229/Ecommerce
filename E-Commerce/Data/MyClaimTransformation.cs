using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using E_Commerce.Data;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication;
//using System.Security.Claims;

public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
{
  ApplicationDbContext Context;
  public AppClaimsPrincipalFactory(
      UserManager<ApplicationUser> userManager,
      ApplicationDbContext _context,
      IOptions<IdentityOptions> options
      ) : base(userManager, options)
  {
    Context = _context;
  }

  protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
  {
    var identity = await base.GenerateClaimsAsync(user);
    
    identity.AddClaim(new Claim("UserID", user.Id.ToString()));
    identity.AddClaim(new Claim("FullName", user.FullName ?? ""));
    
    
    //if (appUser.ProfilePicture != null)
    //{
    //  identity.AddClaim(new Claim("ProfilePicture", Convert.ToBase64String(appUser.ProfilePicture)));
    //}
    //else
    //{
    //  identity.AddClaim(new Claim("ProfilePicture", "N/A"));
    //}

    user.LastLogin = DateTime.Now;
    await UserManager.UpdateAsync(user);
    return identity;
  }

}

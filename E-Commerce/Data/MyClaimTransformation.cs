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
    private readonly UserManager<ApplicationUser> _userManager;
    public AppClaimsPrincipalFactory(
      UserManager<ApplicationUser> userManager,
      ApplicationDbContext _context,
      IOptions<IdentityOptions> options
      ) : base(userManager, options)
  {
        Context = _context;
        _userManager = userManager;
    }

  protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
  {
    var identity = await base.GenerateClaimsAsync(user);

        var userWithIncludes = await Context.Users
                .Where(x => x.Id == user.Id)
                .Include(g => g.Gender)
                .Include(g => g.Nationality)
                .FirstAsync();

        //get assigned roles to  current user from DB and make claims
        ////var roles = await _userManager.GetRolesAsync(user);
        ////foreach (var role in roles)
        ////{
        ////    identity.AddClaim(new Claim(ClaimTypes.Role, role));
        ////}

        identity.AddClaim(new Claim("UserID", user.Id.ToString()));
        identity.AddClaim(new Claim("FullName", user.FullName ?? ""));
        identity.AddClaim(new Claim("MemberSince", user.MemberSince.ToString("dd MMM yyyy") ?? "N/A"));


        identity.AddClaim(new Claim("Gender", userWithIncludes.Gender?.title ?? "-"));
        identity.AddClaim(new Claim("Nationality", userWithIncludes.Nationality?.title ?? "-"));

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

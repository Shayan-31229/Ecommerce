using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol;
using System.Security.Claims;
using System.Security.Principal;
//using static E_Commerce.Data.Enums;

public static class IdentityExtensions
{
  public static string FullName(this IIdentity identity)
  {
    var claim = ((ClaimsIdentity)identity).FindFirst("FullName");
    // Test for null to avoid issues during local testing
    return (claim != null) ? claim.Value : string.Empty;
  }
  public static string OfficeName(this IIdentity identity)
  {
    var claim = ((ClaimsIdentity)identity).FindFirst("OfficeName");
    // Test for null to avoid issues during local testing
    return (claim != null) ? claim.Value : string.Empty;
  }
  
  public static string CompanyName(this IIdentity identity)
  {
    if (identity.IsAuthenticated)
    {
      return "De-Arte Home";
    }
    else
    {
      return "N/A";
    }
  }

  public static string CompanyAddress(this IIdentity identity)
  {
    if(identity.IsAuthenticated) 
    {
      return "Falak Sair Plaza 1st Floor Shop No.5 Peshawar Saddar.";
    }
    else
    {
      return "N/A";
    }
  }

  
  public static string CompanyContact(this IIdentity identity)
  {
    if(identity.IsAuthenticated) 
    {
      return "03119002030";
    }
    else
    {
      return "N/A";
    }
  }




  public static string AuthLevel(this IIdentity identity)
  {
    var claim = ((ClaimsIdentity)identity).FindFirst("AuthLevel");
    // Test for null to avoid issues during local testing
    return (claim != null) ? claim.Value : string.Empty;
  }

  public static Guid RegionID(this IIdentity identity)
  {
    var claim = ((ClaimsIdentity)identity).FindFirst("RegionID");
    // Test for null to avoid issues during local testing
    return (claim != null) ? Guid.Parse(claim.Value) : new Guid("00000000-0000-0000-0000-000000000000");
  }
  public static Guid OfficeID(this IIdentity identity)
  {
    var claim = ((ClaimsIdentity)identity).FindFirst("OfficeID");
    // Test for null to avoid issues during local testing
    return (claim != null) ? Guid.Parse(claim.Value) : new Guid("00000000-0000-0000-0000-000000000000");
  }

  public static bool SuperUser(this IIdentity identity)
  {
    bool superUser = false;
    var claim = ((ClaimsIdentity)identity).FindFirst("SuperUser");
    // Test for null to avoid issues during local testing
    Boolean.TryParse(claim.Value, out superUser);
    return superUser;
  }
  public static bool IsAdmin(this IIdentity identity)
  {
    bool superUser = false;
    var claim = ((ClaimsIdentity)identity).FindFirst("IsAdmin");
    // Test for null to avoid issues during local testing
    Boolean.TryParse(claim.Value, out superUser);
    return superUser;
  }
  public static bool NewEntry(this IIdentity identity)
  {
    bool NewEntry = false;
    var claim = ((ClaimsIdentity)identity).FindFirst("NewEntry");
    // Test for null to avoid issues during local testing
    Boolean.TryParse(claim.Value, out NewEntry);
    return NewEntry;
  }

  public static bool AdditionalDocs(this IIdentity identity)
  {
    bool AdditionalScan = false;
    var claim = ((ClaimsIdentity)identity).FindFirst("AdditionalScan");
    // Test for null to avoid issues during local testing
    Boolean.TryParse(claim.Value, out AdditionalScan);
    return AdditionalScan;
  }

  public static string CNICNo(this IIdentity identity)
  {
    var claim = ((ClaimsIdentity)identity).FindFirst("CNICNo");
    // Test for null to avoid issues during local testing
    return (claim != null) ? claim.Value : string.Empty;
  }
}
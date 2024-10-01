using E_Commerce.Models;
using E_Commerce.Data;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models.VMs
{
    public class VMUserWithRoles
    {
        public ApplicationUser? User { get; set; }
        public List<IdentityRole>? Roles { get; set; }

        public IList<string>? AssignedRoles { get; set; }

        public IList<LoginLog>? Logins { get; set; }

         

        //public IList<Order>? Orders { get; set; }
    }
}

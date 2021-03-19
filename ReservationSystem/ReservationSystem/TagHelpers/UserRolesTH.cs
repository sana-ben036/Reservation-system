using ReservationSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationSystem.TagHelpers
{
   
        [HtmlTargetElement("td", Attributes = "i-user")]
        public class UserRolesTH : TagHelper
        {
            private readonly UserManager<AppUser> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            public UserRolesTH(UserManager<AppUser> usermgr, RoleManager<IdentityRole> rolemgr)
            {
                userManager = usermgr;
                roleManager = rolemgr;
            }

            [HtmlAttributeName("i-user")]
            public string User { get; set; }

            public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
            {
                List<string> names = new List<string>();
                AppUser user = await userManager.FindByIdAsync(User);
            

                if (user != null)
                {
                    foreach (var role in roleManager.Roles)
                    {
                        if (role != null && await userManager.IsInRoleAsync(user, role.Name))
                            names.Add(role.Name);
                    }
                }
                output.Content.SetContent(names.Count == 0 ? "No Roles" : string.Join(", ",names));
            }
        }
    }




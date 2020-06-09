using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;
using TRMDataManager.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public async Task<UserModel> GetById()
        {
            string id = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
          var result= await data.GetUserById(id).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("api/User/Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();
            using (var context= new ApplicationDbContext())
            {
            var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var users = userManager.Users.ToList();
                var roles = context.Roles.ToList();
                foreach (var user in users)
                {
                    ApplicationUserModel userModel = new ApplicationUserModel
                    {
                        Id = user.Id,
                        Email = user.Email,

                    };
                    foreach (var userRole in user.Roles)
                    {
                        userModel.Roles.Add(userRole.RoleId, roles.Where(x => x.Id == userRole.RoleId).FirstOrDefault().Name);
                    }
                    output.Add(userModel);
                }
                return output;
            }
        }
    }
}

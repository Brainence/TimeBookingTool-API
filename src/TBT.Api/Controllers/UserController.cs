using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : CrudApiController<UserModel>
    {
        public UserController(IManagerStore managerStore) 
            : base(managerStore, managerStore.UserManager)
        { }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public UserModel GetByEmail(string email)
        {
            return ManagerStore.UserManager.GetByEmail(email);
        }

        [HttpGet]
        [Route("ValidatePassword/{userId:int:min(1)}/{password}")]
        public async Task<bool> IsPasswordValid(int userId, string password)
        {
            return await ManagerStore.UserManager.IsPasswordValid(userId, password);
        }


        [HttpGet]
        [Route("ChangePassword/{userId:int:min(1)}/{oldPassword}/{newPassword}")]
        public async Task ChangePassword(int userId, string oldPassword, string newPassword)
        {
            await ManagerStore.UserManager.ChangePassword(userId, oldPassword, newPassword);
        }
    }
}

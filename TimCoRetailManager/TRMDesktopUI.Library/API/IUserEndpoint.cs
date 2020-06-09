using System.Collections.Generic;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Model;

namespace TRMDesktopUI.Library.API
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
    }
}
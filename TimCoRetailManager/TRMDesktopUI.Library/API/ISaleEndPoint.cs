using System.Threading.Tasks;
using TRMDesktopUI.Library.Model;

namespace TRMDesktopUI.Library.API
{
    public interface ISaleEndPoint
    {
        Task PostSaleAsync(SaleModel sale);
    }
}
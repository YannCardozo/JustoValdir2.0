using System.Threading.Tasks;

namespace Blazor_Catalogo.Client.Auth
{
    public interface IAuthorizeService
    {
        Task Login(string token);
        Task Logout();
    }
}

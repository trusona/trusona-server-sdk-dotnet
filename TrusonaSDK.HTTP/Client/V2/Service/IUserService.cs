using System.Threading.Tasks;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface IUserService
  {
    Task DeleteUserAsync(string userIdentifier);
  }
}

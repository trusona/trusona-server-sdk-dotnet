using System.Threading.Tasks;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface IUserService
  {
    void DeleteUser(string userIdentifier);
    Task DeleteUserAsync(string userIdentifier);
  }
}

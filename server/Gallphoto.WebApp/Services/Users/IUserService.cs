using Gallphoto.Infrastructure.Entities;
using Gallphoto.WebApp.Models.Requests.Users;
using Gallphoto.WebApp.Models.Response.Users;

namespace Gallphoto.WebApp.Services.Users;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(Guid id);
    (User,string err) RegisterNewUser(RegisterNewUserRequest model);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gallphoto.Infrastructure.Entities;
using Gallphoto.WebApp.Configurations.Models;
using Gallphoto.WebApp.Models.Requests.Users;
using Gallphoto.WebApp.Models.Response.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gallphoto.WebApp.Services.Users;

public class UserService : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    
    private static List<User> _users = new()
    {
        new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Sehitcan",
            LastName = "Kutel",
            Username = "test",
            Password = "1q2w3e$R"
        }
    };

    private readonly JwtSettings _appSettings;

    public UserService(IOptions<JwtSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public (User,string err) RegisterNewUser(RegisterNewUserRequest model)
    {
        var existUser = _users.FirstOrDefault(x => x.Username.Equals(model.Username.Trim(), StringComparison.CurrentCultureIgnoreCase));

        if (existUser is not null)
        {
            return (null, $"Username = {existUser.Username} already possessed by another user.");
        }
        
        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = model.FirstName.Trim(),
            LastName = model.LastName.Trim(),
            Username = model.Username.Trim(),
            Password = model.Password.Trim()
        };
        
        _users.Add(newUser);

        return (newUser, null);
    }
    
    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User GetById(Guid id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
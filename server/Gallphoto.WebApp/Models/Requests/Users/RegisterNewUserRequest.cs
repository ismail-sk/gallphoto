using System.ComponentModel.DataAnnotations;

namespace Gallphoto.WebApp.Models.Requests.Users;

public class RegisterNewUserRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
    
    public string LastName { get; set; }
    
    public string FirstName { get; set; }
}
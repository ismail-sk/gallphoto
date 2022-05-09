using System.ComponentModel.DataAnnotations;

namespace Gallphoto.WebApp.Models.Requests.Users;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
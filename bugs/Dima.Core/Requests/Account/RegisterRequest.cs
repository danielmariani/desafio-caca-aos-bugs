using System.ComponentModel.DataAnnotations;
using Dima.Core.Common.CustomAttributes;

namespace Dima.Core.Requests.Account;

public class RegisterRequest : Request
{
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    [DimaPassword]
    public string Password { get; set; } = string.Empty;
}
using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs.Requests.Authentication;

public class LoginRequest
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    public string Password { get; set; } = string.Empty;
}
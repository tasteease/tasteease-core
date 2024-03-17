namespace Fiap.TasteEase.Api.ViewModels.Client;

public record LoginRequest(string Username);

public record LoginResponse(string RefreshToken, string AccessToken, int Expiration);
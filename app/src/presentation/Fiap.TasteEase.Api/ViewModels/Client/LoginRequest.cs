using System.Diagnostics.CodeAnalysis;

namespace Fiap.TasteEase.Api.ViewModels.Client;

[ExcludeFromCodeCoverage]
public record LoginRequest(string Username);

[ExcludeFromCodeCoverage]
public record LoginResponse(string RefreshToken, string AccessToken, int Expiration);
using System.Diagnostics.CodeAnalysis;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Fiap.TasteEase.Api.ViewModels;
using Fiap.TasteEase.Api.ViewModels.Client;
using Fiap.TasteEase.Application.Helpers;
using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fiap.TasteEase.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ExcludeFromCodeCoverage]
public class ClientController : ControllerBase
{
    private readonly IAmazonCognitoIdentityProvider _identityProvider;
    private readonly CognitoUserPool _userPool;
    private readonly AwsSettings _awsSettings;
    private readonly IPublisher _sendMessage;

    public ClientController(
        IAmazonCognitoIdentityProvider identityProvider,
        CognitoUserPool userPool,
        IOptions<AwsSettings> awsSettings, IPublisher sendMessage)
    {
        _identityProvider = identityProvider;
        _userPool = userPool;
        _sendMessage = sendMessage;
        _awsSettings = awsSettings.Value;
    }
    
    public class PaymentModel
    {
        public bool Paid { get; set; }
        public DateTime? PaidDate { get; set; }
        public string? Reference { get; set; }
        public Guid OrderId { get; set; }
    }
    
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<ResponseViewModel<LoginResponse>>> Login(LoginRequest request)
    {
        try
        {
            await _sendMessage.Pub(new PaymentModel { OrderId = Guid.NewGuid(), Paid = true }, "tasteease-payment-webhook");
            return Ok();
            // var user = new CognitoUser(
            //     request.Username, 
            //     _awsSettings.UserPoolClientId, 
            //     _userPool, 
            //     _identityProvider);
            //
            // var authRequest = new InitiateCustomAuthRequest
            // {
            //     AuthParameters = new Dictionary<string, string>
            //     {
            //         { "CHALLENGE_NAME", "CUSTOM_CHALLENGE" },
            //         { "USERNAME", request.Username },
            //         { "SECRET_HASH", CognitoHash.GetSecretHash(request.Username, _awsSettings.UserPoolClientId,_awsSettings.UserPoolClientSecret) }
            //     },
            //     ClientMetadata = new Dictionary<string, string>()
            // };
            //
            // var authResponse = await user.StartWithCustomAuthAsync(authRequest);
            //
            // return StatusCode(StatusCodes.Status201Created,
            //     new ResponseViewModel<LoginResponse>
            //     {
            //         Data = new LoginResponse(authResponse.AuthenticationResult.RefreshToken, authResponse.AuthenticationResult.AccessToken, authResponse.AuthenticationResult.ExpiresIn)
            //     }
            // );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest,
                new ResponseViewModel<LoginResponse>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message }
                }
            );
        }
    }
}
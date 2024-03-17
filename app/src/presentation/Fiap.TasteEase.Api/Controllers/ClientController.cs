using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Fiap.TasteEase.Api.ViewModels;
using Fiap.TasteEase.Api.ViewModels.Client;
using Fiap.TasteEase.Application.Helpers;
using Fiap.TasteEase.Application.UseCases.ClientUseCase;
using Fiap.TasteEase.Domain.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fiap.TasteEase.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IMediator _mediator;
    private readonly IAmazonCognitoIdentityProvider _identityProvider;
    private readonly CognitoUserPool _userPool;
    private readonly AwsSettings _awsSettings;

    public ClientController(
        ILogger<ClientController> logger,
        IMediator mediator,
        IAmazonCognitoIdentityProvider identityProvider,
        CognitoUserPool userPool,
        IOptions<AwsSettings> awsSettings)
    {
        _logger = logger;
        _mediator = mediator;
        _identityProvider = identityProvider;
        _userPool = userPool;
        _awsSettings = awsSettings.Value;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseViewModel<Guid?>>> Post(CreateClientRequest request)
    {
        try
        {
            var command = request.Adapt<Create>();

            var mediatorResponse = await _mediator.Send(command);

            if (mediatorResponse.IsFailed)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseViewModel<Guid?>
                    {
                        Error = true,
                        ErrorMessages = mediatorResponse.Errors.Select(x => x.Message)
                    }
                );

            return StatusCode(StatusCodes.Status201Created,
                new ResponseViewModel<Guid?>
                {
                    Data = mediatorResponse.ValueOrDefault
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<Guid?>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                }
            );
        }
    }
    
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<ResponseViewModel<LoginResponse>>> Login(LoginRequest request)
    {
        try
        {
            var user = new CognitoUser(
                request.Username, 
                _awsSettings.UserPoolClientId, 
                _userPool, 
                _identityProvider);

            var authRequest = new InitiateCustomAuthRequest
            {
                AuthParameters = new Dictionary<string, string>
                {
                    { "CHALLENGE_NAME", "CUSTOM_CHALLENGE" },
                    { "USERNAME", request.Username },
                    { "SECRET_HASH", CognitoHash.GetSecretHash(request.Username, _awsSettings.UserPoolClientId,_awsSettings.UserPoolClientSecret) }
                },
                ClientMetadata = new Dictionary<string, string>()
            };

            var authResponse = await user.StartWithCustomAuthAsync(authRequest);
            
            return StatusCode(StatusCodes.Status201Created,
                new ResponseViewModel<LoginResponse>
                {
                    Data = new LoginResponse(authResponse.AuthenticationResult.RefreshToken, authResponse.AuthenticationResult.AccessToken, authResponse.AuthenticationResult.ExpiresIn)
                }
            );
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
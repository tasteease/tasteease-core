using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Fiap.TasteEase.Application.Helpers;
using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.ClientAggregate;
using Fiap.TasteEase.Domain.DTOs;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Options;

namespace Fiap.TasteEase.Application.UseCases.ClientUseCase;

public class ClientHandler : IRequestHandler<Create, Result<Guid>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAmazonCognitoIdentityProvider _identityProvider;
    private readonly AwsSettings _awsSettings;

    public ClientHandler(IClientRepository clientRepository, IAmazonCognitoIdentityProvider identityProvider, IOptions<AwsSettings> awsSettings)
    {
        _clientRepository = clientRepository;
        _identityProvider = identityProvider;
        _awsSettings = awsSettings.Value;
    }

    public async Task<Result<Guid>> Handle(Create request, CancellationToken cancellationToken)
    {
        var existClient = await _clientRepository.Get(w => w.TaxpayerNumber == request.TaxpayerNumber);
        if (existClient.IsFailed || existClient.ValueOrDefault.Any()) return Result.Fail("Cliente já existe");
        
        var (_, isFailed, client) = Client.Create(new CreateClientProps(request.Name, request.TaxpayerNumber));
        if (isFailed) return Result.Fail("Erro registrando cliente");

        _clientRepository.Add(client);
        await _clientRepository.SaveChanges();

        await SignUpAsync(request.TaxpayerNumber, request.Name);

        return Result.Ok(client.Id.Value);
    }
    
    private async Task<Result> SignUpAsync(string username, string name)
    {
        try
        {
            var request = CreateSignUpRequest(username, name);
            await _identityProvider.SignUpAsync(request);

            return Result.Ok();
        }
        catch(Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    private SignUpRequest CreateSignUpRequest(string username, string name)
    {
        return new SignUpRequest
        {
            ClientId = _awsSettings.UserPoolClientId,
            SecretHash = CognitoHash.GetSecretHash(username, _awsSettings.UserPoolClientId, _awsSettings.UserPoolClientSecret),
            Username = username,
            Password = "12345678",
            UserAttributes = new List<AttributeType>
            {
                new() { Name = "name", Value = name }
            }
        };
    }
}
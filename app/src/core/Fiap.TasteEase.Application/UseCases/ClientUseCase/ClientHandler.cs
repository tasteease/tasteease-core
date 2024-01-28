using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.ClientAggregate;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.ClientUseCase
{
    public class ClientHandler : IRequestHandler<Create, Result<Guid>>
    {
        private readonly IMediator _mediator;
        private readonly IClientRepository _clientRepository;

        public ClientHandler(IMediator mediator, IClientRepository clientRepository)
        {
            _mediator = mediator;
            _clientRepository = clientRepository;
        }

        public async Task<Result<Guid>> Handle(Create request, CancellationToken cancellationToken)
        {
            var clientResult = Client.Create(new CreateClientProps(request.Name, request.TaxpayerNumber));
            if (clientResult.IsFailed)
                return Result.Fail("Erro registrando cliente");

            var client = clientResult.ValueOrDefault;

            var result = _clientRepository.Add(client);
            await _clientRepository.SaveChanges();

            return Result.Ok(client.Id.Value);
        }
    }
}
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.ClientUseCase
{
    public class Create : IRequest<Result<Guid>>
    {
        public string Name { get; set; }
        public string TaxpayerNumber { get; set; }
    }
}
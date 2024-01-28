using Fiap.TasteEase.Application.Ports;
using FluentResults;
using Mapster;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetWithDescription
{
    public class GetOrderWithDescriptionHandler : IRequestHandler<GetOrderWithDescriptionQuery, Result<IEnumerable<OrderWithDescriptionQuery>>>
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public GetOrderWithDescriptionHandler(IMediator mediator, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        public async Task<Result<IEnumerable<OrderWithDescriptionQuery>>> Handle(GetOrderWithDescriptionQuery request,
            CancellationToken cancellationToken)
        {
            var ordersResult = await _orderRepository.GetWithDescription();

            if (ordersResult.IsFailed)
                return Result.Fail("Erro ao obter os pedidos");

            var orders = ordersResult.ValueOrDefault;
            var response = orders.Adapt<IEnumerable<OrderWithDescriptionQuery>>();
            return Result.Ok(response);
        }
    }
}
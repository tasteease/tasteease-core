using Fiap.TasteEase.Api.ViewModels;
using Fiap.TasteEase.Api.ViewModels.Order;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Checkout;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Checkout.Process;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Create;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetById;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetWithDescription;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Update;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fiap.TasteEase.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IMediator _mediator;

    public OrderController(
        ILogger<OrderController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseViewModel<CreateOrderResponse>>> Create([FromBody] OrderRequest request)
    {
        try
        {
            var command = request.Adapt<CreateOrderCommand>();
            var response = await _mediator.Send(command);

            if (response.IsFailed)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseViewModel<CreateOrderResponse>
                    {
                        Error = true,
                        ErrorMessages = response.Errors.Select(x => x.Message),
                        Data = null!
                    }
                );

            return StatusCode(StatusCodes.Status201Created,
                new ResponseViewModel<CreateOrderResponse>
                {
                    Data = response.ValueOrDefault.Adapt<CreateOrderResponse>()
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<CreateOrderResponse>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                    Data = null!
                }
            );
        }
    }

    [HttpGet]
    public async Task<ActionResult<ResponseViewModel<IEnumerable<OrderResponse>>>> GetAll(
        [FromQuery] List<OrderStatus> status, Guid? clientId)
    {
        try
        {
            var response = await _mediator.Send(new GetOrderAllQuery { ClientId = clientId, Status = status });

            if (response.IsFailed)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseViewModel<IEnumerable<OrderResponse>>
                    {
                        Error = true,
                        ErrorMessages = response.Errors.Select(x => x.Message),
                        Data = null!
                    }
                );

            return StatusCode(StatusCodes.Status200OK,
                new ResponseViewModel<IEnumerable<OrderResponse>>
                {
                    Data = response.ValueOrDefault.Adapt<IEnumerable<OrderResponse>>()
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<IEnumerable<OrderResponse>>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                    Data = null!
                }
            );
        }
    }


    [HttpGet("/[controller]/GetWithDescription")]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseViewModel<IEnumerable<OrderWithDescriptionResponse>>>> GetWithDescription()
    {
        try
        {
            var response = await _mediator.Send(new GetOrderWithDescriptionQuery());

            if (response.IsFailed)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseViewModel<IEnumerable<OrderWithDescriptionResponse>>
                    {
                        Error = true,
                        ErrorMessages = response.Errors.Select(x => x.Message),
                        Data = null!
                    }
                );

            return StatusCode(StatusCodes.Status200OK,
                new ResponseViewModel<IEnumerable<OrderWithDescriptionResponse>>
                {
                    Data = response.ValueOrDefault.Adapt<IEnumerable<OrderWithDescriptionResponse>>()
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<IEnumerable<OrderWithDescriptionResponse>>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                    Data = null!
                }
            );
        }
    }

    [HttpGet("/[controller]/{orderId}")]
    public async Task<ActionResult<ResponseViewModel<OrderResponse>>> GetById([FromRoute] Guid orderId)
    {
        try
        {
            var response = await _mediator.Send(new GetOrderByIdQuery { OrderId = orderId });

            if (response.IsFailed)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseViewModel<OrderResponse>
                    {
                        Error = true,
                        ErrorMessages = response.Errors.Select(x => x.Message),
                        Data = null!
                    }
                );

            return StatusCode(StatusCodes.Status200OK,
                new ResponseViewModel<OrderResponse>
                {
                    Data = response.ValueOrDefault.Adapt<OrderResponse>()
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<OrderResponse>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                    Data = null!
                }
            );
        }
    }

    [HttpPost("/[controller]/{orderId}/status")]
    public async Task<ActionResult<ResponseViewModel<CreateOrderResponse>>> UpdateStatus(
        [FromBody] UpdateOrderStatusRequest request, [FromRoute] Guid orderId)
    {
        try
        {
            var response = await _mediator.Send(new UpdateOrderCommand { OrderId = orderId, Status = request.Status });

            if (response.IsFailed)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseViewModel<CreateOrderResponse>
                    {
                        Error = true,
                        ErrorMessages = response.Errors.Select(x => x.Message),
                        Data = null!
                    }
                );

            return StatusCode(StatusCodes.Status200OK,
                new ResponseViewModel<CreateOrderResponse>
                {
                    Data = response.ValueOrDefault.Adapt<CreateOrderResponse>()
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<CreateOrderResponse>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                    Data = null!
                }
            );
        }
    }

    [HttpPost("/[controller]/{orderId}/pay")]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseViewModel<OrderPaymentResponse>>> Pay([FromRoute] Guid orderId)
    {
        try
        {
            var response = await _mediator.Send(new PayCommand { OrderId = orderId });

            if (response.IsFailed)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseViewModel<OrderPaymentResponse>
                    {
                        Error = true,
                        ErrorMessages = response.Errors.Select(x => x.Message),
                        Data = null!
                    }
                );

            return StatusCode(StatusCodes.Status200OK,
                new ResponseViewModel<OrderPaymentResponse>
                {
                    Data = response.ValueOrDefault.Adapt<OrderPaymentResponse>()
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<OrderPaymentResponse>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                    Data = null!
                }
            );
        }
    }

    [HttpPost("/[controller]/process_payment")]
    public async Task<ActionResult<ResponseViewModel<string>>> UpdatePayment([FromBody] UpdatePaymentRequest request)
    {
        try
        {
            var pay = request.Adapt<ProcessPaymentCommand>();
            var response = await _mediator.Send(pay);

            if (response.IsFailed)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseViewModel<string>
                    {
                        Error = true,
                        ErrorMessages = response.Errors.Select(x => x.Message),
                        Data = null!
                    }
                );

            return StatusCode(StatusCodes.Status200OK,
                new ResponseViewModel<string>
                {
                    Data = response.ValueOrDefault
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<string>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                    Data = null!
                }
            );
        }
    }
}
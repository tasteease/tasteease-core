using Fiap.TasteEase.Api.ViewModels;
using Fiap.TasteEase.Api.ViewModels.Food;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Create;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Delete;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetAll;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetById;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Update;
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
public class FoodController : ControllerBase
{
    private readonly ILogger<FoodController> _logger;
    private readonly IMediator _mediator;

    public FoodController(
        ILogger<FoodController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseViewModel<IEnumerable<FoodResponseDto>>>> GetAll()
    {
        try
        {
            var response = await _mediator.Send(new GetFoodAllQuery());

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
                new ResponseViewModel<IEnumerable<FoodResponseDto>>
                {
                    Data = response.ValueOrDefault
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<IEnumerable<FoodResponseDto>>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                    Data = null!
                }
            );
        }
    }

    [HttpGet("GetById")]
    public async Task<ActionResult<ResponseViewModel<FoodResponseDto>>> GetById([FromQuery] Guid id)
    {
        try
        {
            var response = await _mediator.Send(new GetFoodByIdQuery
            {
                Id = id
            });

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
                new ResponseViewModel<FoodResponseDto>
                {
                    Data = response.ValueOrDefault
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseViewModel<FoodResponseDto>
                {
                    Error = true,
                    ErrorMessages = new List<string> { ex.Message },
                    Data = null!
                }
            );
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseViewModel<string>>> Post(CreateFoodRequest request)
    {
        try
        {
            var command = request.Adapt<CreateFoodCommand>();

            var response = await _mediator.Send(command);

            if (response.IsFailed)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseViewModel<string>
                    {
                        Error = true,
                        ErrorMessages = response.Errors.Select(x => x.Message),
                        Data = null!
                    }
                );

            return StatusCode(StatusCodes.Status201Created,
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

    [HttpPut]
    public async Task<ActionResult<ResponseViewModel<string>>> Put(UpdateFoodRequest request)
    {
        try
        {
            var command = request.Adapt<UpdateFoodCommand>();

            var response = await _mediator.Send(command);

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

    [HttpDelete]
    public async Task<ActionResult<ResponseViewModel<string>>> Delete(DeleteFoodRequest request)
    {
        try
        {
            var command = request.Adapt<DeleteFoodCommand>();

            var response = await _mediator.Send(command);

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
using Fiap.TasteEase.Api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fiap.TasteEase.Api.ViewModels.Client;
using Fiap.TasteEase.Application.UseCases.ClientUseCase;
using Mapster;

namespace Fiap.TasteEase.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public ClientController(
            ILogger<ClientController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseViewModel<Guid?>>> Post(CreateClientRequest request)
        {
            try
            {
                var command = request.Adapt<Create>();

                var response = await _mediator.Send(command);

                if (response.IsFailed)
                {
                    return StatusCode((int)StatusCodes.Status400BadRequest,
                        new ResponseViewModel<Guid?>
                        {
                            Error = true,
                            ErrorMessages = response.Errors.Select(x => x.Message),
                            Data = null!
                        }
                    );
                }

                return StatusCode((int)StatusCodes.Status201Created,
                    new ResponseViewModel<Guid?>
                    {
                        Data = response.ValueOrDefault
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode((int)StatusCodes.Status500InternalServerError,
                    new ResponseViewModel<Guid?>
                    {
                        Error = true,
                        ErrorMessages = new List<string> { ex.Message },
                        Data = null!
                    }
                );
            }
        }

    }
}
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Sample.Contracts;

namespace Sample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IRequestClient<SubmitOrderMessage> _submitOrderRequestClient;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IRequestClient<SubmitOrderMessage> submitOrderRequestClient, ILogger<OrderController> logger)
    {
        _submitOrderRequestClient = submitOrderRequestClient;
        _logger = logger;
    }

    [HttpPost(Name = "CreateOrder")]
    public async Task<IActionResult> CreateOrder(int id)
    {
        var (accepted, rejected) = await _submitOrderRequestClient.GetResponse<SubmitOrderMessageAccepted, SubmitOrderRejected>(new()
        {
            CustomerId = id
        });

        if (accepted.IsCompletedSuccessfully)
        {
            var response = await accepted;
            return Ok(response.Message);
        }
        else
        {
            var response = await rejected;
            return BadRequest(response.Message);
        }
    }
}
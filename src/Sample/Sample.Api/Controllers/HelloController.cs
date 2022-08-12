using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Sample.Contracts;

namespace Sample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    private readonly IRequestClient<HelloMessage> _helloMessageRequestClient;
    private readonly ILogger<HelloController> _logger;

    public HelloController(
        IRequestClient<HelloMessage> helloMessageRequestClient,
        ILogger<HelloController> logger)
    {
        _helloMessageRequestClient = helloMessageRequestClient;
        _logger = logger;
    }

    [HttpGet(Name = "HelloWorld")]
    public async Task<IActionResult> Get()
    {
        Response<HelloMessageAccepted> response = 
            await _helloMessageRequestClient.GetResponse<HelloMessageAccepted>(new HelloMessage()
        {
            Name = "Ozan"
        });

        return Ok(response.Message.Message);
    }
}
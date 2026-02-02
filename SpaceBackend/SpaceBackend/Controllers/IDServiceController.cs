using Microsoft.AspNetCore.Mvc;
using SpaceBackend.Models;
using SpaceBackend.Services;

namespace SpaceBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IDServiceController : ControllerBase
{
    private readonly IIDService _service;

    public IDServiceController(IIDService service)
    {
        _service = service;
    }

    [HttpGet] 
    public async Task<ActionResult> GetSessionInfo([FromBody]GetSessionRequest request)
    {
        var idservice = await _service.GetSessionAsync(request.SessionId);
        return Ok(idservice);
    }
}

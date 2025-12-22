using Microsoft.AspNetCore.Mvc;
using SpaceBackend.Services;

namespace SpaceBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceInfoController : ControllerBase
{
    private readonly IServiceInfoService _serviceInfoService;

    public ServiceInfoController(IServiceInfoService serviceInfoService)
    {
        _serviceInfoService = serviceInfoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetServiceInfo()
    {
        var serviceInfo = await _serviceInfoService.GetServiceInfoAsync();
        return Ok(serviceInfo);
    }
}

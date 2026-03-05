using Microsoft.AspNetCore.Mvc;
using SpaceBackend.Models;
using SpaceBackend.Services;

namespace SpaceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ISessionServicecs _serviceInfoService;

        public HomeController(ISessionServicecs serviceInfoService)
        {
            _serviceInfoService = serviceInfoService;
        }
        [HttpPost]
        public async Task<IActionResult> JoinPensil([FromBody] JoinSessionRequest Request)
        {
            await _serviceInfoService.JoinToRoom(Request);

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> LeavePensil([FromBody] LeaveSessionRequestcs Request)
        {
            await _serviceInfoService.LeaveToRoom(Request);

            return Ok();
        }
    }
}

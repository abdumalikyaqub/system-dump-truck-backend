using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitoringDumpTruck.Helpers;
using MonitoringDumpTruck.Models.Entities;

namespace MonitoringDumpTruck.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PredictionsController : ControllerBase
{
    private readonly FlaskApiService _flaskApiService;

    public PredictionsController(FlaskApiService flaskApiService)
    {
        _flaskApiService = flaskApiService;
    }

    [HttpPost("engine")]
    public async Task<IActionResult> GetEnginePrediction([FromBody] Pointer pointer)
    {
        var result = await _flaskApiService.GetEnginePrediction(pointer);
        return Ok(result);
    }

    [HttpPost("tire")]
    public async Task<IActionResult> GetTirePrediction([FromBody] Pointer pointer)
    {
        var result = await _flaskApiService.GetTirePrediction(pointer);
        return Ok(result);
    }
}

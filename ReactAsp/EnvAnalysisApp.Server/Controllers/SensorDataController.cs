using EnvAnalysisApp.Server.Data;
using EnvAnalysisApp.Server.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SensorDataController : ControllerBase
{
    private readonly SensorDbContext _context;

    public SensorDataController(SensorDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostSensorData([FromBody] SensorData data)
    {
        _context.SensorReadings.Add(data);
        await _context.SaveChangesAsync();
        return Ok();
    }
}

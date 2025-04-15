using EnvAnalysisApp.Server.Data;
using EnvAnalysisApp.Server.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Runtime.CompilerServices;
using LogManager = log4net.LogManager;


[ApiController]
[Route("api/[controller]")]
public class SensorDataController : ControllerBase
{
    private readonly SensorDbContext _context;
    private static readonly ILog log = LogManager.GetLogger(typeof(SensorDataController));

    public SensorDataController(SensorDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostSensorData([FromBody] SensorData data)
    {
        var ret = _context.SensorReadings.Add(data);
        log.Info(ret);
        log.Info("yooooooooooooo");
        await _context.SaveChangesAsync();
        return Ok();
    }
}

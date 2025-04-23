using EnvAnalysisApp.Server.Data;
using EnvAnalysisApp.Server.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
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
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("temperature-python")]
    public async Task<IActionResult> PredictWithPython()
    {
        var recentTemps = await _context.SensorReadings
            .OrderByDescending(d => d.Timestamp)
            .Take(10)
            .Select(d => new { d.Timestamp, d.Temperature })
            .ToListAsync();

        if (recentTemps.Count < 2)
            return BadRequest("Not enough data.");

        var json = JsonSerializer.Serialize(recentTemps);

        var psi = new ProcessStartInfo
        {
            FileName = "python",
            Arguments = "predict.py",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = Process.Start(psi);
        await process.StandardInput.WriteAsync(json);
        process.StandardInput.Close();

        var result = await process.StandardOutput.ReadToEndAsync();
        process.WaitForExit();

        var prediction = JsonSerializer.Deserialize<Dictionary<string, double>>(result);
        return Ok(prediction);
    }

}

using EnvAnalysisApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EnvAnalysisApp.Server.Data
{
    public class SensorDbContext : DbContext
    {
        public SensorDbContext(DbContextOptions<SensorDbContext> options)
            : base(options)
        {
        }

        public DbSet<SensorData> SensorReadings { get; set; }
    }
}

using System;

namespace EnvAnalysisApp.Server.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float AirQuality { get; set; }
        public float LightLevel { get; set; }
    }
}

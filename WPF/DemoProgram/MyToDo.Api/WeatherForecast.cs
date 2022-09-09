using System;

namespace MyToDo.Api
{
    /// <summary>
    /// ¹Ù·½Ê¾Àý,¿ÉÉ¾³ý
    /// </summary>
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}

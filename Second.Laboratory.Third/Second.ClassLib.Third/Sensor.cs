using System;
using System.Collections.Generic;
using System.Text;

namespace Second.ClassLib.Third;

public enum SensorType
{
    Temperature,
    Pressure,
    Humidity
}

public abstract class Sensor
{
    public string Id { get; set; } = string.Empty;
    public double Threshold { get; set; }
    public abstract SensorType Type { get; }
}

public class TemperatureSensor : Sensor
{
    public override SensorType Type => SensorType.Temperature;
    public string Unit { get; set; } = string.Empty;
}

public class PressureSensor : Sensor
{
    public override SensorType Type => SensorType.Pressure;
    public double MaxPressure { get; set; }
}

public class HumiditySensor : Sensor
{
    public override SensorType Type => SensorType.Humidity;
    public bool IsOutdoor { get; set; }
}

public static class SensorFactory
{
    public static Sensor Create(SensorType type) => type switch
    {
        SensorType.Temperature => new TemperatureSensor(),
        SensorType.Pressure => new PressureSensor(),
        SensorType.Humidity => new HumiditySensor(),
        _ => throw new ArgumentException()
    };
}
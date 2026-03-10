namespace Second.ClassLib.Second;

public interface ITemperatureSensor {
    double GetTemperatureCelsius();
}

public class FahrenheitSensor {
    public double ReadFahrenheit() => 100.0;
}

public class SensorAdapter : ITemperatureSensor {
    private readonly FahrenheitSensor fSensor;

    public SensorAdapter(FahrenheitSensor oldSensor) {
        fSensor = oldSensor;
    }

    public double GetTemperatureCelsius() {
        double f = fSensor.ReadFahrenheit();
        return Math.Round((f - 32) * 5 / 9, 2);
    }
}
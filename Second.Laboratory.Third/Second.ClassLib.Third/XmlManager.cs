using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Second.ClassLib.Third;

public interface IXmlManager
{
    void Save(List<Sensor> sensors, string filePath);
    List<Sensor> Load(string filePath);
}

public class DomXmlManager : IXmlManager
{
    public void Save(List<Sensor> sensors, string filePath)
    {
        var doc = new XDocument(
            new XElement("Sensors",
                sensors.Select(s =>
                {
                    var el = new XElement("Sensor",
                        new XAttribute("type", s.Type.ToString()),
                        new XElement("Id", s.Id),
                        new XElement("Threshold", s.Threshold.ToString(CultureInfo.InvariantCulture))
                    );

                    if (s is TemperatureSensor ts) el.Add(new XElement("Unit", ts.Unit));
                    else if (s is PressureSensor ps) el.Add(new XElement("MaxPressure", ps.MaxPressure.ToString(CultureInfo.InvariantCulture)));
                    else if (s is HumiditySensor hs) el.Add(new XElement("IsOutdoor", hs.IsOutdoor.ToString().ToLower()));

                    return el;
                })
            )
        );
        doc.Save(filePath);
    }

    public List<Sensor> Load(string filePath)
    {
        var list = new List<Sensor>();
        var doc = new XmlDocument();
        doc.Load(filePath);

        foreach (XmlNode node in doc.SelectNodes("//Sensor")!)
        {
            var type = Enum.Parse<SensorType>(node.Attributes!["type"]!.Value);
            var sensor = SensorFactory.Create(type);
            sensor.Id = node["Id"]!.InnerText;
            sensor.Threshold = double.Parse(node["Threshold"]!.InnerText, CultureInfo.InvariantCulture);

            if (sensor is TemperatureSensor ts && node["Unit"] != null) ts.Unit = node["Unit"]!.InnerText;
            else if (sensor is PressureSensor ps && node["MaxPressure"] != null) ps.MaxPressure = double.Parse(node["MaxPressure"]!.InnerText, CultureInfo.InvariantCulture);
            else if (sensor is HumiditySensor hs && node["IsOutdoor"] != null) hs.IsOutdoor = bool.Parse(node["IsOutdoor"]!.InnerText);

            list.Add(sensor);
        }
        return list;
    }
}

public class SaxXmlManager : IXmlManager
{
    public void Save(List<Sensor> sensors, string filePath)
    {
        using var writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true });
        writer.WriteStartDocument();
        writer.WriteStartElement("Sensors");

        foreach (var s in sensors)
        {
            writer.WriteStartElement("Sensor");
            writer.WriteAttributeString("type", s.Type.ToString());
            writer.WriteElementString("Id", s.Id);
            writer.WriteElementString("Threshold", s.Threshold.ToString(CultureInfo.InvariantCulture));

            if (s is TemperatureSensor ts) writer.WriteElementString("Unit", ts.Unit);
            else if (s is PressureSensor ps) writer.WriteElementString("MaxPressure", ps.MaxPressure.ToString(CultureInfo.InvariantCulture));
            else if (s is HumiditySensor hs) writer.WriteElementString("IsOutdoor", hs.IsOutdoor.ToString().ToLower());

            writer.WriteEndElement();
        }

        writer.WriteEndElement();
        writer.WriteEndDocument();
    }

    public List<Sensor> Load(string filePath)
    {
        var list = new List<Sensor>();
        using var reader = XmlReader.Create(filePath);

        while (reader.ReadToFollowing("Sensor"))
        {
            var type = Enum.Parse<SensorType>(reader.GetAttribute("type")!);
            var sensor = SensorFactory.Create(type);

            using var inner = reader.ReadSubtree();
            while (inner.Read())
            {
                if (inner.NodeType == XmlNodeType.Element)
                {
                    string name = inner.Name;
                    inner.Read();
                    if (inner.NodeType == XmlNodeType.Text)
                    {
                        string value = inner.Value;
                        if (name == "Id") sensor.Id = value;
                        else if (name == "Threshold") sensor.Threshold = double.Parse(value, CultureInfo.InvariantCulture);
                        else if (name == "Unit" && sensor is TemperatureSensor ts) ts.Unit = value;
                        else if (name == "MaxPressure" && sensor is PressureSensor ps) ps.MaxPressure = double.Parse(value, CultureInfo.InvariantCulture);
                        else if (name == "IsOutdoor" && sensor is HumiditySensor hs) hs.IsOutdoor = bool.Parse(value);
                    }
                }
            }
            list.Add(sensor);
        }
        return list;
    }
}

public static class XmlTools
{
    public static bool Validate(string filePath, string xsdPath, out string message)
    {
        bool isValid = true;
        string errors = string.Empty;

        var settings = new XmlReaderSettings();
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationEventHandler += (s, e) =>
        {
            isValid = false;
            errors += e.Message + "\n";
        };

        using (var reader = XmlReader.Create(filePath, settings))
        {
            while (reader.Read()) { }
        }

        message = isValid ? "OK" : errors;
        return isValid;
    }

    public static List<Sensor> Filter(string filePath, double minThreshold)
    {
        var doc = XDocument.Load(filePath);
        return doc.Descendants("Sensor")
            .Where(e => double.Parse(e.Element("Threshold")!.Value, CultureInfo.InvariantCulture) > minThreshold)
            .Select(e =>
            {
                var type = Enum.Parse<SensorType>(e.Attribute("type")!.Value);
                var sensor = SensorFactory.Create(type);
                sensor.Id = e.Element("Id")!.Value;
                sensor.Threshold = double.Parse(e.Element("Threshold")!.Value, CultureInfo.InvariantCulture);

                if (sensor is TemperatureSensor ts && e.Element("Unit") != null) ts.Unit = e.Element("Unit")!.Value;
                else if (sensor is PressureSensor ps && e.Element("MaxPressure") != null) ps.MaxPressure = double.Parse(e.Element("MaxPressure")!.Value, CultureInfo.InvariantCulture);
                else if (sensor is HumiditySensor hs && e.Element("IsOutdoor") != null) hs.IsOutdoor = bool.Parse(e.Element("IsOutdoor")!.Value);

                return sensor;
            }).ToList();
    }
}

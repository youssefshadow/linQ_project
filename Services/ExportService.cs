using LinQ_project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

namespace LinQ_project.Services
{
    public class ExportService
    {
        // Exporter les résultats au format JSON
        public void ExportToJson(ConversionResult result, string outputPath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(result.Records, options);

            var finalPath = Path.Combine(outputPath, $"export_{DateTime.Now:yyyyMMddHHmmss}.json");
            File.WriteAllText(finalPath, json);
            Console.WriteLine($"✅ Les résultats ont été exportés vers : {finalPath}");
        }

        // Exporter les résultats au format XML
        public void ExportToXml(ConversionResult result, string outputPath)
        {
            // Convertir les dictionnaires en objets ExportItem
            var exportList = result.Records.Select(record =>
            {
                var item = new ExportItem();
                foreach (var field in result.Fields)
                {
                    var value = record.ContainsKey(field.Name) ? record[field.Name]?.ToString() ?? "" : "";
                    item.Values.Add(field.Name, value);
                }
                return item;
            }).ToList();

            var finalPath = Path.Combine(outputPath, $"export_{DateTime.Now:yyyyMMddHHmmss}.xml");

            var serializer = new XmlSerializer(typeof(List<ExportItem>), new XmlRootAttribute("Export"));
            using (var stream = new FileStream(finalPath, FileMode.Create))
            {
                serializer.Serialize(stream, exportList);
            }

            Console.WriteLine($"✅ Les résultats ont été exportés vers : {finalPath}");
        }

        // Exporter les résultats en base de données (placeholder)
        public void ExportToDb(ConversionResult result, string connectionString)
        {
            Console.WriteLine($"Les résultats ont été exportés vers la base de données.");
        }
    }

    public class ExportItem
    {
        [XmlIgnore]
        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();

        // Sérialisation manuelle (clé/valeurs dynamiques)
        [XmlAnyElement]
        public System.Xml.XmlElement[] XmlElements
        {
            get
            {
                var doc = new System.Xml.XmlDocument();
                return Values.Select(kv =>
                {
                    var el = doc.CreateElement(kv.Key);
                    el.InnerText = kv.Value;
                    return el;
                }).ToArray();
            }
            set { /* ignore on deserialize */ }
        }
    }
}

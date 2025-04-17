using LinQ_project.Models;
using LinQ_project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ_project.Views
{
    public class ExportOptionsForm
    {
        private readonly ConversionResult _result;
        private readonly ExportService _exportService;

        public ExportOptionsForm(ConversionResult result)
        {
            _result = result;
            _exportService = new ExportService();
        }

        // Afficher les champs disponibles à l'export
        private void DisplayFields()
        {
            Console.WriteLine("\nSélectionnez les champs à exporter (Entrez les numéros séparés par une virgule) :");
            for (int i = 0; i < _result.Fields.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_result.Fields[i].Name}");
            }
        }

        // Choisir les champs à exporter
        private List<DataField> GetSelectedFields()
        {
            DisplayFields();
            var input = Console.ReadLine();
            var selectedIndexes = input.Split(',').Select(x => int.Parse(x.Trim()) - 1).ToList();

            return selectedIndexes.Select(index => _result.Fields[index]).ToList();
        }

        // Choisir le format d'export
        private string GetExportFormat()
        {
            Console.WriteLine("\nChoisissez le format d'export :");
            Console.WriteLine("1. JSON");
            Console.WriteLine("2. XML");
            Console.WriteLine("3. Base de données");

            var choice = Console.ReadLine();
            return choice switch
            {
                "1" => "JSON",
                "2" => "XML",
                "3" => "DB",
                _ => throw new ArgumentException("Format non valide")
            };
        }

        // Exécuter l'export
        public void ExecuteExport(string outputPath)
        {
            // Sélectionner les champs à exporter
            var selectedFields = GetSelectedFields();
            _result.Fields = selectedFields;

            // Choisir le format d'export
            var exportFormat = GetExportFormat();

            switch (exportFormat)
            {
                case "JSON":
                    _exportService.ExportToJson(_result, outputPath);
                    break;
                case "XML":
                    _exportService.ExportToXml(_result, outputPath);
                    break;
                case "DB":
                    Console.WriteLine("Export vers DB non implémenté dans cette version.");
                    break;
                default:
                    Console.WriteLine("Format non supporté.");
                    break;
            }
        }
    }
}

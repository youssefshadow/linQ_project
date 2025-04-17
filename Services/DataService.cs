using LinQ_project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LinQ_project.Services
{
    public class DataService
    {
        // Charger les données depuis un fichier JSON
        public ConversionResult LoadFromJson(string path)
        {
            var result = new ConversionResult();

            if (!File.Exists(path))
                throw new FileNotFoundException($"Le fichier {path} est introuvable.");

            var json = File.ReadAllText(path);
            var records = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

            if (records != null && records.Any())
            {
                // Extraire les champs
                var fields = records.First().Keys.Select(k => new DataField(k, records.First()[k].GetType())).ToList();
                result.Fields = fields;
                result.Records = records;
            }

            return result;
        }

        // Rechercher dans les enregistrements
        public List<Dictionary<string, object>> Search(ConversionResult result, string searchTerm)
        {
            return result.Records
                        .Where(record => record.Values.Any(value => value.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
        }

        // Trier les enregistrements
        public List<Dictionary<string, object>> Sort(ConversionResult result, string fieldName, bool ascending = true)
        {
            try
            {
                var sortedRecords = ascending
                    ? result.Records.OrderBy(record => ConvertToComparable(record[fieldName])).ToList()
                    : result.Records.OrderByDescending(record => ConvertToComparable(record[fieldName])).ToList();

                return sortedRecords;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du tri des données : {ex.Message}");
                return result.Records; // Retourner la liste non triée en cas d'erreur
            }
        }

        // Convertir les valeurs en types comparables
        private object ConvertToComparable(object value)
        {
            if (value == null)
                return string.Empty; // Retourne une valeur par défaut si l'élément est nul

            // Si c'est un type qui peut être comparé directement, retourne-le
            if (value is IComparable comparableValue)
            {
                return comparableValue;
            }

            // Sinon, si c'est une chaîne ou un nombre, retourne la valeur sous forme de chaîne
            return value.ToString();
        }

        // Grouper les enregistrements par un champ
        public List<IGrouping<object, Dictionary<string, object>>> GroupByField(ConversionResult result, string fieldName)
        {
            return result.Records.GroupBy(record => record[fieldName]).ToList();
        }
    }
}

using LinQ_project.Models;
using LinQ_project.Services;
using System;

namespace LinQ_project.Views
{
    // Views/MainForm.cs
    public class MainForm
    {
        private readonly DataService _dataService;
        private ConversionResult _currentResult;

        public MainForm()
        {
            _dataService = new DataService();
        }

        // Afficher le menu principal
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("===== Menu Principal =====");
            Console.WriteLine("1. Charger des données");
            Console.WriteLine("2. Rechercher des données");
            Console.WriteLine("3. Trier des données");
            Console.WriteLine("4. Grouper des données");
            Console.WriteLine("5. Exporter les résultats");
            Console.WriteLine("6. Quitter");

            Console.Write("\nChoisissez une option : ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    LoadData();
                    break;
                case "2":
                    SearchData();
                    break;
                case "3":
                    SortData();
                    break;
                case "4":
                    GroupData();
                    break;
                case "5":
                    ExportData();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Choix invalide, essayez à nouveau.");
                    Pause();
                    DisplayMenu();
                    break;
            }
        }

        // Charger les données depuis un fichier
        private void LoadData()
        {
            Console.Write("\nEntrez le chemin du fichier JSON : ");
            var filePath = Console.ReadLine();
            try
            {
                _currentResult = _dataService.LoadFromJson(filePath);
                Console.WriteLine("Données chargées avec succès.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }
            Pause();
            DisplayMenu();
        }

        // Rechercher dans les données
        private void SearchData()
        {
            Console.Write("\nEntrez le terme de recherche : ");
            var searchTerm = Console.ReadLine();
            if (_currentResult != null)
            {
                var searchResults = _dataService.Search(_currentResult, searchTerm);
                Console.WriteLine("\nRésultats de la recherche :");
                foreach (var record in searchResults)
                {
                    Console.WriteLine(string.Join(", ", record.Values));
                }
            }
            else
            {
                Console.WriteLine("Aucune donnée chargée.");
            }
            Pause();
            DisplayMenu();
        }

        // Trier les données
        private void SortData()
        {
            Console.Write("\nEntrez le nom du champ pour trier : ");
            var fieldName = Console.ReadLine();
            Console.Write("\nTrier ascendant (y/n) ? ");
            var ascending = Console.ReadLine()?.ToLower() == "y";
            if (_currentResult != null)
            {
                var sortedRecords = _dataService.Sort(_currentResult, fieldName, ascending);
                Console.WriteLine("\nDonnées triées :");
                foreach (var record in sortedRecords)
                {
                    Console.WriteLine(string.Join(", ", record.Values));
                }
            }
            else
            {
                Console.WriteLine("Aucune donnée chargée.");
            }
            Pause();
            DisplayMenu();
        }

        // Grouper les données
        private void GroupData()
        {
            Console.Write("\nEntrez le nom du champ pour grouper : ");
            var fieldName = Console.ReadLine();
            if (_currentResult != null)
            {
                var groupedRecords = _dataService.GroupByField(_currentResult, fieldName);
                Console.WriteLine("\nDonnées groupées :");
                foreach (var group in groupedRecords)
                {
                    Console.WriteLine($"{fieldName}: {group.Key}");
                    foreach (var record in group)
                    {
                        Console.WriteLine(string.Join(", ", record.Values));
                    }
                }
            }
            else
            {
                Console.WriteLine("Aucune donnée chargée.");
            }
            Pause();
            DisplayMenu();
        }

        // Exporter les résultats
        private void ExportData()
        {
            if (_currentResult != null)
            {
                var exportForm = new ExportOptionsForm(_currentResult);
                Console.Write("\nEntrez le chemin pour l'export : ");
                var outputPath = Console.ReadLine();
                exportForm.ExecuteExport(outputPath);
            }
            else
            {
                Console.WriteLine("Aucune donnée chargée.");
            }
            Pause();
            DisplayMenu();
        }

        // Pause pour garder la console ouverte
        private void Pause()
        {
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadLine();
        }
    }
}

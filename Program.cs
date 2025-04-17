using LinQ_project.Views;

namespace LinQ_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mainForm = new MainForm();
            mainForm.DisplayMenu();

            // Empêche la fermeture immédiate de la console
            Console.WriteLine("\nAppuyez sur une touche pour quitter...");
            Console.ReadLine();
        }
    }
}

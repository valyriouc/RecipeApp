using System;
using System.Windows;

namespace RecipeClient
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();

            MainWindow window = new MainWindow();

            app.Run(window);
        }
    }
}

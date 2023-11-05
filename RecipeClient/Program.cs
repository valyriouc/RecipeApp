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

            app.DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show(window, args.Exception.Message);
            };

            app.Run(window);
        }
    }
}

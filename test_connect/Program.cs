using System;
using System.Windows.Forms;

namespace cinema
{
    static class Program
    {
        public static string ConnString = @"Server=DESKTOP-TJFO5C6\MSSQLSERVER01;Database=cinema;Trusted_Connection=True";
        public static string Hall = "";
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CinemaForm());
        }
    }
}

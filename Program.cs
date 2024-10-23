using System.Runtime.InteropServices;
using WinFormium;

namespace CentralControl {
    internal static class Program {
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            //AllocConsole();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            var builder = WinFormiumApp.CreateBuilder();

            builder.UseWinFormiumApp<WindowsSuperConsole>();

            var app = builder.Build();

            app.Run();
            //FreeConsole();

        }
    }
}
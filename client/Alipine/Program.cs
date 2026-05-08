using Alpine;

namespace Alipine
{
    internal static class Program
    {
        // program icon source: Flaticon.com
        // Microsoft.Web.WebView2

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        // automatically generated winforms code
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
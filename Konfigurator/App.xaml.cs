using System;
using System.Runtime.InteropServices;
using System.Windows;
using Konfigurator.ViewModels;
using Konfigurator.Windows;

namespace Konfigurator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application 
    {
        private const int ATTACH_PARENT_PROCESS = -1;

        public App()
        {
            AttachToParentConsole();
        }
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);

        /// <summary>
        ///     Redirects the console output of the current process to the parent process.
        /// </summary>
        /// <remarks>
        ///     Must be called before calls to <see cref="Console.WriteLine()" />.
        /// </remarks>
        private static void AttachToParentConsole() {
            AttachConsole(ATTACH_PARENT_PROCESS);
        }
    }
    
    
}
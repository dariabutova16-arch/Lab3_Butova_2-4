using Microsoft.Extensions.DependencyInjection;
using StudentCabinet.Services;

namespace StudentCabinet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            ThemeService.Apply();

            var window = new Window(new AppShell());

            window.Created += (s, e) => System.Diagnostics.Debug.WriteLine("[App] Created");
            // ... решта рядків без змін
            return window;
        }
    }
}
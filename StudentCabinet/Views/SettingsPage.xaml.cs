using StudentCabinet.Services;

namespace StudentCabinet.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        ThemeSwitch.IsToggled = ThemeService.IsDark;
    }

    private void OnThemeToggled(object? sender, ToggledEventArgs e)
    {
        ThemeService.Set(e.Value ? AppTheme.Dark : AppTheme.Light);
    }
}
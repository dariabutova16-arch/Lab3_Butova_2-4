using StudentCabinet.Services;

namespace StudentCabinet.Views;

public class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        Title = "Налаштування";

        var themeSwitch = new Switch
        {
            IsToggled = ThemeService.IsDark
        };

        themeSwitch.Toggled += (s, e) =>
        {
            ThemeService.Set(e.Value ? AppTheme.Dark : AppTheme.Light);
        };

        Content = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 16,
            Children =
            {
                new Label
                {
                    Text = "Тема оформлення",
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold
                },
                new HorizontalStackLayout
                {
                    Spacing = 12,
                    Children =
                    {
                        new Label { Text = "Темна тема", VerticalOptions = LayoutOptions.Center },
                        themeSwitch
                    }
                }
            }
        };
    }
}
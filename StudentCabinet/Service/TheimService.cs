namespace StudentCabinet.Services;

public static class ThemeService
{
    private const string Key = "app_theme";

    public static void Apply()
    {
        var saved = Preferences.Get(Key, "Light");
        Set(saved == "Dark" ? AppTheme.Dark : AppTheme.Light, false);
    }

    public static void Set(AppTheme theme, bool save = true)
    {
        if (Application.Current is not null)
            Application.Current.UserAppTheme = theme;

        if (save)
            Preferences.Set(Key, theme == AppTheme.Dark ? "Dark" : "Light");
    }

    public static bool IsDark
    {
        get { return Preferences.Get(Key, "Light") == "Dark"; }
    }
}
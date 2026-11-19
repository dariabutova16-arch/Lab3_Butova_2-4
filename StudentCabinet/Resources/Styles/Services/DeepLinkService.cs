using Microsoft.Extensions.DependencyInjection;
using StudentCabinet.ViewModels;

namespace StudentCabinet.Services;

public static class DeepLinkService
{
    public static async Task HandleAsync(string? link)
    {
        if (string.IsNullOrWhiteSpace(link))
            return;

        if (!Uri.TryCreate(link.Trim(), UriKind.Absolute, out var uri))
        {
            await ShowAsync("Некоректне посилання");
            return;
        }

        bool okScheme = string.Equals(uri.Scheme, "studentcabinet", StringComparison.OrdinalIgnoreCase);
        bool okHost = string.Equals(uri.Host, "student", StringComparison.OrdinalIgnoreCase);
        if (!okScheme || !okHost)
        {
            await ShowAsync("Невідома адреса. Приклад: studentcabinet://student/Іванов");
            return;
        }

        var name = Uri.UnescapeDataString(uri.AbsolutePath.Trim('/'));

        var vm = IPlatformApplication.Current?.Services.GetService<StudentViewModel>();
        var student = vm?.Students.FirstOrDefault(s =>
            string.Equals(s.FullName?.Trim(), name, StringComparison.OrdinalIgnoreCase));

        if (student is null)
        {
            await ShowAsync("Студента «" + name + "» не знайдено у списку");
            return;
        }

        System.Diagnostics.Debug.WriteLine("[DeepLink] Відкриваю студента: " + name);

        var parameters = new Dictionary<string, object>
        {
            { "SelectedStudent", student }
        };
        await Shell.Current.GoToAsync("studentdetail", parameters);
    }

    private static async Task ShowAsync(string message)
    {
        if (Shell.Current is not null)
            await Shell.Current.DisplayAlert("Посилання", message, "OK");
    }
}
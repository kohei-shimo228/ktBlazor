using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor1.Services;

public class ThemeService
{
    private bool _isDarkMode = false;
    private readonly IJSRuntime _jsRuntime;

    public event Action? OnThemeChanged;

    public bool IsDarkMode => _isDarkMode;

    public ThemeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        // localStorageからテーマ設定を読み込む
        var savedTheme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "theme");
        if (savedTheme == "dark")
        {
            _isDarkMode = true;
        }
        else if (savedTheme == null)
        {
            // システム設定を確認
            var prefersDark = await _jsRuntime.InvokeAsync<bool>("eval", "window.matchMedia('(prefers-color-scheme: dark)').matches");
            _isDarkMode = prefersDark;
        }

        await ApplyThemeAsync();
    }

    public async Task ToggleThemeAsync()
    {
        _isDarkMode = !_isDarkMode;
        await ApplyThemeAsync();
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "theme", _isDarkMode ? "dark" : "light");
        OnThemeChanged?.Invoke();
    }

    private async Task ApplyThemeAsync()
    {
        await _jsRuntime.InvokeVoidAsync("eval", 
            $"document.documentElement.setAttribute('data-theme', '{(_isDarkMode ? "dark" : "light")}')");
    }
}

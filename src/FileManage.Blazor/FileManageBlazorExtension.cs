using Microsoft.Extensions.DependencyInjection;

namespace FileManage.Blazor;

public static class FileManageBlazorExtension
{
    public static IServiceCollection AddFileManageBlazor(this IServiceCollection service)
    {

        service.AddMasaBlazor(options =>
        {
            options.ConfigureTheme(theme =>
            {
                theme.Dark = true;
                theme.Themes.Light.Primary = "#4318FF";
                theme.Themes.Light.Secondary = "#A18BFF";
                theme.Themes.Light.Accent = "#005CAF";
                theme.Themes.Light.UserDefined["Tertiary"] = "#e57373";
            });
        });
        
        return service;
    }
}
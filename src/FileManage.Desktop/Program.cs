using FileManage.Blazor;
using Photino.Blazor;

namespace FileManage.Desktop;

public class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");

        builder.Services.AddFileManageBlazor();

        var app = builder.Build();

        app.MainWindow
            .SetIconFile("favicon.ico")
            .SetTitle("文件管理器")
            .SetDevToolsEnabled(true)
            .SetContextMenuEnabled(false);

        app.MainWindow.RegisterWebMessageReceivedHandler((s, m) =>
        {
            switch (m)
            {
                case "close":
                    app.MainWindow.Close();
                    break;
            }
        });

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {

        };

        app.Run();
    }
}
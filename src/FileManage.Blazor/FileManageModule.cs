using BlazorComponent.JSInterop;
using Microsoft.JSInterop;

namespace FileManage.Blazor;

public class FileManageModule : JSModule
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public FileManageModule(IJSRuntime js) : base(js, "./_content/FileManage.Blazor/js/fileManageModule.js")
    {
    }

}

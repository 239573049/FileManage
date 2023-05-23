using FileManage.Core;

namespace FileManage.Blazor.Components;
public partial class FileTree
{
    private string Root = "/";

    static Dictionary<string, string> files = new()
    {
        {"html", "mdi-language-html5"},
        {"js", "mdi-nodejs"},
        {"json", "mdi-code-json"},
        {"md", "mdi-language-markdown"},
        {"pdf", "mdi-file-pdf"},
        {"png", "mdi-file-image"},
        {"txt", "mdi-file-document-outline"},
        {"xls", "mdi-file-excel"}
    };

    static List<FileTreeModule> items = new();

    List<string> initiallyOpen = new() {};

    private List<FileTreeModule> LoadTree(string root)
    {
        var tree = new List<FileTreeModule>();

        var files = Directory.GetFiles(root);
        var paths = Directory.GetDirectories(root);
        foreach (var file in files)
        {
            try
            {

                var info = new FileInfo(file);
                tree.Add(new FileTreeModule(info.Name, info.FullName, info.Length, "txt"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        foreach (var path in paths)
        {
            try
            {
                var info = new DirectoryInfo(path);
                tree.Add(new FileTreeModule(info.Name)
                {
                    IsDirectory = true,
                    FullName = info.FullName,
                    Children = new List<FileTreeModule>()
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return tree;
    }

    private async Task LoadItem(FileTreeModule module)
    {
        module.Children.Clear();
        module.Children.AddRange(LoadTree(module.FullName));
        await Task.CompletedTask;
    }

    protected override void OnInitialized()
    {
        items = LoadTree(Root);
    }
}

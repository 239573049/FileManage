using System.Linq;

namespace FileManage.Blazor.Shared;

public partial class MainLayout
{
    public string Root { get; set; } = "/";

    private string _search;

    public List<FileTreeModule> items { get; set; } = new();

    public Action<FileTreeModule>? OnClick { get; set; }

    private void OnPageSearch()
    {
        if (string.IsNullOrEmpty(Root))
        {
            Root = "/";
        }

        var info = new DirectoryInfo(Root);
        if (!info.Exists)
        {
            Root = "/";
            items = LoadTree(Root);

            return;
        }

        items = LoadTree(Root);
    }

    private void OnSearch()
    {
        if (string.IsNullOrEmpty(Root))
        {
            Root = "/";
        }

        var info = new DirectoryInfo(Root);
        if (!info.Exists)
        {
            Root = "/";
            items = LoadTree(Root, _search);

            return;
        }

        items = LoadTree(Root, _search);
    }

    private void OnRetreat()
    {
        if (Root == "/")
        {
            return;
        }

        var roots = Root.Replace("\\", "/").TrimEnd('/').Split('/');

        if (roots.Length >= 2)
        {
            Array.Resize(ref roots, roots.Length - 1);
            Root = string.Join('/', roots);
        }

        if (string.IsNullOrEmpty(Root))
        {
            Root = "/";
        }

        if (!Root.EndsWith('/'))
        {
            Root += '/';
        }

        items = LoadTree(Root);
    }


    public List<FileTreeModule> LoadTree(string root, string? search = null)
    {
        var tree = new List<FileTreeModule>();

        var files = string.IsNullOrEmpty(search) ? Directory.GetFiles(root) : Directory.GetFiles(root, search);
        var paths = string.IsNullOrEmpty(search) ? Directory.GetDirectories(root) : Directory.GetDirectories(root, search);
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


    protected override void OnInitialized()
    {
        OnClick += (module) =>
        {
            Root = module.FullName ?? Root;
            StateHasChanged();
        };

        items = LoadTree(Root);
    }
}
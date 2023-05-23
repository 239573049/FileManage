namespace FileManage.Core;

public class FileTreeModule
{
    public FileTreeModule(string name, string? icon = null)
    {
        Name = name;
        Icon = icon;
    }

    public FileTreeModule(string name, string? fullName, long length, string? icon) : this(name, icon)
    {
        Length = length;
        FullName = fullName;
    }

    public FileTreeModule(string name, string? fullName, long length, string? icon, bool isDirectory, List<FileTreeModule> children) : this(name, fullName, length, icon)
    {
        IsDirectory = isDirectory;
        Children = children;
    }

    public string Name { get; set; }

    public string? FullName { get; set; }

    public long Length { get; set; } = 0;

    public string? Icon { get; set; }

    public bool IsDirectory { get; set; } = false;

    public List<FileTreeModule> Children { get; set; } 
}

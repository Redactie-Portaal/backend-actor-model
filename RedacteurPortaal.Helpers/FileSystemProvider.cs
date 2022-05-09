namespace RedacteurPortaal.Helpers;

public class FileSystemProvider
{
    public virtual System.IO.Abstractions.IFileSystem FileSystem { get; set; }

    public FileSystemProvider()
    {
        FileSystem = new System.IO.Abstractions.FileSystem();
    }
}
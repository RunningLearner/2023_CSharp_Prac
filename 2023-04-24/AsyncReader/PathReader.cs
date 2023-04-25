namespace AsyncReader;

public class PathReader
{
    public List<string> GetFileList(string rootPath)
    {
        string[] fileList = Directory.GetFiles(rootPath, "*.cs");
        return new(fileList);
    }

    public List<string> GetFolderList(string rootPath)
    {
        string[] folderList = Directory.GetDirectories(rootPath, "*");
        return new(folderList);
    }
}

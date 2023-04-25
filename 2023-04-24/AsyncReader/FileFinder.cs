namespace AsyncReader;

public class FileFinder
{
    private readonly List<string> _asyncFileList = new();

    private readonly PathReader _pathReader;
    private readonly FileChecker _fileChecker;

    public FileFinder(PathReader pathReader, FileChecker fileChecker)
    {
        _fileChecker = fileChecker;
        _pathReader = pathReader;
    }

    public List<string> Explorer(string rootPath)
    {
        var folderList = new List<string> { rootPath };

        FindEachDir(folderList);

        return _asyncFileList;
    }
    private void FindEachDir(List<string> folderList)
    {
        Console.WriteLine("Invoke FindEachDir()");

        while (folderList.Count > 0)
        {
            var currentFolder = PopLast(folderList);
            List<string> newFolderList = _pathReader.GetFolderList(currentFolder);
            var fileList = _pathReader.GetFileList(currentFolder);
            folderList.AddRange(newFolderList);

            var selectedFiles = fileList.Where(file => _fileChecker.HasAsyncAwait(file));
            _asyncFileList.AddRange(selectedFiles);
        }
    }
    private static string PopLast(List<string> stringList)
    {
        var lastString = stringList.Last();
        stringList.Remove(stringList.Last());
        return lastString;
    }
}
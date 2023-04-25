namespace AsyncReader;
using System.Collections.Concurrent;
public class AsyncFileFinder
{
    private readonly ConcurrentBag<string> _asyncFileList = new();
    private readonly PathReader _pathReader;
    private readonly FileChecker _fileChecker;

    public AsyncFileFinder(PathReader pathReader, FileChecker fileChecker)
    {
        _fileChecker = fileChecker;
        _pathReader = pathReader;
    }

    public async Task<List<string>> ExplorerAsync(string rootPath)
    {
        var firstFolderList = _pathReader.GetFolderList(rootPath);
        var tasks = new List<Task>();

        foreach (var currentFolder in firstFolderList)
        {
            List<string> newFolderList = new() { currentFolder };
            tasks.Add(FindEachDirAsync(newFolderList));
        }

        await Task.WhenAll(tasks);

        return _asyncFileList.ToList();
    }

    private async Task FindEachDirAsync(List<string> folderList)
    {
        Console.WriteLine("Invoke FindEachDirAsync()");

        while (folderList.Count > 0)
        {
            var currentFolder = PopLast(folderList);
            List<string> newFolderList = _pathReader.GetFolderList(currentFolder);
            var fileList = _pathReader.GetFileList(currentFolder);
            folderList.AddRange(newFolderList);

            var tasks = fileList.Select(async file =>
            {
                if (await _fileChecker.HasAsyncAwaitAsync(file))
                {
                    return file;
                }
                return null;
            });
            var results = await Task.WhenAll(tasks);
            var resultsList = results.Where(file => file is not null);

            foreach (var result in resultsList)
            {
                _asyncFileList.Add(result!);
            }
        }
    }

    private static string PopLast(List<string> stringList)
    {
        var lastString = stringList.Last();
        stringList.Remove(lastString);
        return lastString;
    }
}
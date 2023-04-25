using System.Diagnostics;

namespace AsyncReader;

static class Program
{
    static async Task Main(string[] args)
    {
        const int maxDepth = 6;
        const int maxFolders = 5;
        const string rootPath = "maze";
        var creater = new MazeCreater(rootPath);

        creater.CreateRandomFolders(rootPath, maxDepth, maxFolders);
        var fileChecker = new FileChecker();
        var pathReader = new PathReader();
        var asyncFileFinder = new AsyncFileFinder(pathReader, fileChecker);
        var fileFinder = new FileFinder(pathReader, fileChecker);
        Stopwatch stopwatch = new();

        stopwatch.Start();
        List<string> asyncFilesList = await asyncFileFinder.ExplorerAsync(rootPath);
        stopwatch.Stop();
        var asyncElapsedTime = stopwatch.ElapsedMilliseconds;
        stopwatch.Reset();

        stopwatch.Start();
        List<string> filesList = fileFinder.Explorer(rootPath);
        stopwatch.Stop();
        var syncElapsedTime = stopwatch.ElapsedMilliseconds;

        var isSequenceEqual = asyncFilesList.SequenceEqual(filesList);
        var isElementEqual = asyncFilesList.Intersect(filesList).Count() == asyncFilesList.Count;
        asyncFilesList.ForEach(file => Console.WriteLine($"비동기적으로 찾은 파일이름: {file}"));
        filesList.ForEach(file => Console.WriteLine($"동기적으로 찾은 파일이름: {file}"));
        Console.WriteLine("두 리스트의 개수: {0}   {1}", filesList.Count, asyncFilesList.Count);
        Console.WriteLine("두 리스트의 순서가 같은지: {0}", isSequenceEqual);
        Console.WriteLine("두 리스트의 구성 요소가 같은지: {0}", isElementEqual);
        Console.WriteLine("비동기 경과시간: {0} milliseconds", asyncElapsedTime);
        Console.WriteLine("동기 경과시간: {0} milliseconds", syncElapsedTime);
    }
}
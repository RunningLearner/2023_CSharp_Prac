namespace AsyncReader;

public class FileChecker
{
    public async Task<bool> HasAsyncAwaitAsync(string filePath)
    {
        int asyncCount = 0;
        int awaitCount = 0;
        using StreamReader reader = new(filePath);
        string? line = await reader.ReadLineAsync();

        while (line != null)
        {
            asyncCount += CountAsync(line);
            awaitCount += CountAwait(line);
            if ((asyncCount > 0) && (awaitCount > 0)) return true;
            line = await reader.ReadLineAsync();
        }

        return false;
    }

    public bool HasAsyncAwait(string filePath)
    {
        int asyncCount = 0;
        int awaitCount = 0;
        using StreamReader reader = new(filePath);
        string? line = reader.ReadLine();

        while (line != null)
        {
            asyncCount += CountAsync(line);
            awaitCount += CountAwait(line);
            if ((asyncCount > 0) && (awaitCount > 0)) return true;
            line = reader.ReadLine();
        }

        return false;
    }

    private static int CountAwait(string inputString)
    {
        return inputString.Split("await").Length - 1;
    }

    private static int CountAsync(string inputString)
    {
        return inputString.Split("async").Length - 1;
    }
}
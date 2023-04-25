namespace AsyncReader;

public class MazeCreater
{
    private static readonly Random _random = new();

    public MazeCreater(string rootPath)
    {
        if (Directory.Exists("maze"))
        {
            Directory.Delete(rootPath, true);
        }
        Directory.CreateDirectory(rootPath);
    }

    public void CreateRandomFolders(string rootPath, int depth, int maxFolders)
    {
        if (depth == 0)
            return;

        for (int i = 0; i < maxFolders; i++)
        {
            string folderName = GetRandomName();
            string folderPath = Path.Combine(rootPath, folderName);

            Directory.CreateDirectory(folderPath);
            MakeRandomAsyncFile(folderPath);

            int remainingDepth = depth - 1;

            CreateRandomFolders(folderPath, remainingDepth, maxFolders);
        }
    }

    static string GetRandomName()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        int length = _random.Next(5, 15);
        char[] nameChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            nameChars[i] = chars[_random.Next(chars.Length)];
        }

        return new string(nameChars);
    }

    static void MakeRandomAsyncFile(string currentFolder)
    {
        string filePath = Path.Combine(currentFolder, GetRandomName() + ".cs");

        if (_random.Next(0, 3) == 2)
        {
            using (File.Create(filePath)) { }
            TextWriter.WriteFile(filePath);
        }
    }
}
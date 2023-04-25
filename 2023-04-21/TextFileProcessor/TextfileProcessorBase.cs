namespace TextfileServices;

public class TextFileProcessor
{
    private readonly ITextFileService _service;

    public TextFileProcessor(ITextFileService service)
    {
        _service = service;
    }

    public void Run(string fileName)
    {
        _service.Initialize(fileName);

        using var sr = new StreamReader(fileName);
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? string.Empty;
            _service.Excute(line);
        }
        _service.Terminate();
    }
}

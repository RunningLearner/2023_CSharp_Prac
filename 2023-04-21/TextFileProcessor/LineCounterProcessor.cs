namespace TextfileServices;

class LineCounterProcessor : ITextFileService
{
    private int _count;

    public void Initialize(string fileName)
    {
        _count = 0;
    }

    public void Excute(string line)
    {
        _count++;
    }

    public void Terminate()
    {
        Console.WriteLine("{0}줄", _count);
    }
}
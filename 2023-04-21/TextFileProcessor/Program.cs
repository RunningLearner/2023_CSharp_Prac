namespace TextfileServices;

static class Program
{
    static void Main(string[] args)
    {
        var textfileprocessor = new TextFileProcessor(new ToHalfWidthProcessor());

        textfileprocessor.Run(args[0]);
    }
}
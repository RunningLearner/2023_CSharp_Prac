namespace TextfileServices;
using System.Globalization;
using System.Text;

class ToHalfWidthProcessor : ITextFileService
{
    private string? _line;

    public void Initialize(string fileName)
    {
        Console.WriteLine("바꾸기 전 문자열:");
        using var sr = new StreamReader(fileName);

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? string.Empty;
            Console.WriteLine(line);
        }
    }

    public void Excute(string line)
    {
        var cultureInfo = CultureInfo.CurrentCulture;
        var result = new StringBuilder();

        foreach (char c in line)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);

            if (category == UnicodeCategory.DecimalDigitNumber)
            {
                var halfwidth = CharUnicodeInfo.GetDigitValue(c).ToString(cultureInfo);
                result.Append(halfwidth);
            }
            else
            {
                result.Append(c);
            }
        }

        _line += result.Append('\n');
    }

    public void Terminate()
    {
        Console.WriteLine("바뀐 후 문자열:");
        Console.WriteLine(_line);
    }
}
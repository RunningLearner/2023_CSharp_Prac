namespace DistanceConverter;

static class Program
{
    static void Main(string[] args)
    {
        while(true)
        {
            var from = GetConverter("변환할 단위를 입력하세요.");
            var to = GetConverter("변환 결과 단위를 입력하세요.");
            var distance = GetDistance(from);
            var converter = new DistanceConverter(from, to);
            var result = converter.Convert(distance);

            Console.WriteLine($"{distance}{from.UnitName}는 {result:0.000}{to.UnitName}입니다.\n");
        }
    }

    static double GetDistance(ConverterBase from)
    {
        double? value;

        do
        {
            Console.Write($"변환하려는 거리(단위:{from.UnitName})를 입력하세요 => ");

            var line = Console.ReadLine();
            value = double.TryParse(line, out double temp) ? (double?)temp : null;
        } while (value == null);

        return value.Value;
    }

    static ConverterBase GetConverter(string msg)
    {
        ConverterBase? converter;

        do
        {
            Console.Write(msg + " => ");

            var unit = Console.ReadLine();
            converter = ConverterFactory.GetInstance(unit!);
        } while (converter is null);

        return converter;
    }
}

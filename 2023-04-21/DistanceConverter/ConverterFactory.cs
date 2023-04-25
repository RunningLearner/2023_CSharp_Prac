namespace DistanceConverter;

static class ConverterFactory
{
    // 미리 인스턴스를 생성하고 배열에 넣어둔다
    private static readonly ConverterBase[] _converters = new ConverterBase[]{
        new MeterConverter(),
        new FeetConverter(),
        new YardConverter(),
        new InchConverter(),
        new MileConverter()
    };

    public static ConverterBase GetInstance(string name)
    {
        return _converters.FirstOrDefault(x => x.IsMyUnut(name));
    }
}
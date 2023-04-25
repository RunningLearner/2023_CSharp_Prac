namespace DistanceConverter;

public class DistanceConverter
{
    public ConverterBase From { get; }
    public ConverterBase To { get; }

    public DistanceConverter(ConverterBase from, ConverterBase to)
    {
        From = from;
        To = to;
    }

    public double Convert(double value)
    {
        var meter = From.ToMeter(value);
        return To.FromMeter(meter);
    }
}
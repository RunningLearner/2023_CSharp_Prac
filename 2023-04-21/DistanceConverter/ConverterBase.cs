namespace DistanceConverter;

public abstract class ConverterBase
{
    public abstract bool IsMyUnut(string name);
    protected abstract double Ratio { get; }
    public abstract string UnitName { get; }

    // 미터로부터 변환
    public double FromMeter(double meter) => meter / Ratio;

    // 미터로 변환
    public double ToMeter(double feet) => feet * Ratio;
}

public class MeterConverter : ConverterBase
{
    protected override double Ratio { get { return 1; } }
    public override string UnitName { get { return "미터"; } }

    public override bool IsMyUnut(string name)
    {
        return string.Equals(name, "meter", StringComparison.OrdinalIgnoreCase) || name.ToLower() == UnitName;
    }
}

public class FeetConverter : ConverterBase
{
    protected override double Ratio { get { return 0.3048; } }
    public override string UnitName { get { return "피트"; } }

    public override bool IsMyUnut(string name)
    {
        return string.Equals(name, "feet", StringComparison.OrdinalIgnoreCase) || name.ToLower() == UnitName;
    }
}

public class InchConverter : ConverterBase
{
    protected override double Ratio { get { return 0.0254; } }
    public override string UnitName { get { return "인치"; } }

    public override bool IsMyUnut(string name)
    {
        return string.Equals(name, "inch", StringComparison.OrdinalIgnoreCase) || name.ToLower() == UnitName;
    }
}

public class YardConverter : ConverterBase
{
    protected override double Ratio { get { return 0.9144; } }
    public override string UnitName { get { return "야드"; } }

    public override bool IsMyUnut(string name)
    {
        return string.Equals(name, "yard", StringComparison.OrdinalIgnoreCase) || name.ToLower() == UnitName;
    }
}

public class MileConverter : ConverterBase
{
    protected override double Ratio { get { return 1609; } }
    public override string UnitName { get { return "마일"; } }

    public override bool IsMyUnut(string name)
    {
        return string.Equals(name, "mile", StringComparison.OrdinalIgnoreCase) || name.ToLower() == UnitName;
    }
}
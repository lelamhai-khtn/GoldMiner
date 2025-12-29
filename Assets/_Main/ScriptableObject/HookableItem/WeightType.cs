using System.Collections.Generic;

public enum WeightType
{
    Light,
    Medium,
    HeavyMedium,
    Heavy,
    VeryHeavy,
    Immovable
}

public static class WeightTable
{
    public static readonly Dictionary<WeightType, float> Factor = new()
    {
        { WeightType.Light,         0.8f },
        { WeightType.Medium,        1.0f },
        { WeightType.HeavyMedium,   2.8f},
        { WeightType.Heavy,         5.3f },
        { WeightType.VeryHeavy,     7.2f },
        { WeightType.Immovable,     999f },
    };
}
#nullable enable
using System.Collections.Generic;
using System.Linq;

public static class MathUtils
{
    public static float Mod(float a, float b)
    {
        var c = a % b;
        if ((c < 0 && b > 0) || (c > 0 && b < 0))
        {
            c += b;
        }
        return c;
    }

    public static float WeightedAverage(List<float> nums, List<float> weights)
    {
        return nums.Select((t, i) => t * weights[i]).Sum() / weights.Sum();
    }
}

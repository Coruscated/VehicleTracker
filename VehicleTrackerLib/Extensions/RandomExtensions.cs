using System;

public static class RandomExtensions
{
    public static double NextDouble(
        this Random random,
        double minVal,
        double maxVal)
    {
        if (minVal >= maxVal)
        {
            throw new ArgumentOutOfRangeException();
        }
        return random.NextDouble() * (maxVal - minVal) + minVal;
    }
}
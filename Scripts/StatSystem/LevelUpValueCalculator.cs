using System;
using UnityEngine;

public static class LevelUpValueCalculator 
{
    public static int CalculatedValue(int level, int baseValue, float increaseRatio)
    {
        float finalValue;

        finalValue = baseValue + SumOfSeries(level) * increaseRatio -
            MathExtention.IntegerPartOfValue((level - 1) / 10) * SumOfFractionalFor10Levels(baseValue, increaseRatio) -
            SumOfFractionalForSeries(baseValue, increaseRatio, level) * 
            (MathExtention.FractionalPartOfValue((level - 1) / 10) * 10);

        return Mathf.RoundToInt(finalValue);
    }

    private static float SumOfFractionalFor10Levels(int baseValue, float increaseRatio)
    {
        float finalValue = baseValue;

        for (int i = 0; i < 10; i++)
        {
            finalValue += MathExtention.FractionalPartOfValue(finalValue * increaseRatio);
        }

        return finalValue;
    }

    private static float SumOfFractionalForSeries(int baseValue, float increaseRatio, int level)
    {
        float finalValue = baseValue;
        int steps = level < 10 ? level : (int)Math.Truncate(MathExtention.FractionalPartOfValue(level) * 10);

        for (int i = 0; i < steps; i++)
        {
            finalValue += MathExtention.FractionalPartOfValue(finalValue * increaseRatio);
        }

        return finalValue;
    }

    private static int SumOfSeries(int level) => level * ((level - 1) / 2);
}

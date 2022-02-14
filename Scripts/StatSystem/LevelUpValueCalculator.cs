using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpValueCalculator 
{
    public int CalculatedValue(int level, int baseValue, float increaseRatio)
    {
        float finalValue;

        finalValue = baseValue + SumOfSeries(level) * increaseRatio -
            IntegerPartOfValue((level - 1) / 10) * SumOfFractionalFor10Levels(baseValue, increaseRatio) -
            SumOfFractionalForSeries(baseValue, increaseRatio, level) * (FractionalPartOfValue((level - 1) / 10) * 10);

        return Mathf.RoundToInt(finalValue);
    }

    private float SumOfFractionalFor10Levels(int baseValue, float increaseRatio)
    {
        float finalValue = baseValue;

        for (int i = 0; i < 10; i++)
        {
            finalValue += FractionalPartOfValue(finalValue * increaseRatio);
        }

        return finalValue;
    }

    private float SumOfFractionalForSeries(int baseValue, float increaseRatio, int level)
    {
        float finalValue = baseValue;
        int steps = level < 10 ? level : (int)Math.Truncate(FractionalPartOfValue(level) * 10);

        for (int i = 0; i < steps; i++)
        {
            finalValue += FractionalPartOfValue(finalValue * increaseRatio);
        }

        return finalValue;
    }

    private int IntegerPartOfValue(float value) => (int)Math.Truncate((decimal)value);

    private float FractionalPartOfValue(float value) => (value) - IntegerPartOfValue(value);

    private int SumOfSeries(int level) => level * ((level - 1) / 2);
}

using System.Collections.Generic;

public enum ModifierType { Flat, PercentAdd, PercentMultiply }
public class StatModifier 
{
    public readonly ModifierType Type;
    public readonly List<StatType> StatsToAffect;
    public float Value;
    public object Source;

    public int Duration { get; private set; }

    public StatModifier(ModifierType type, List<StatType> stats, float value, int duration, object source)
    {
        Type = type;
        StatsToAffect = stats;
        Value = value;
        Duration = duration;
        Source = source;
    }

    public StatModifier(ModifierType type, List<StatType> stats, float value, int duration) : 
        this(type, stats, value, duration, null) { }

    public void ChangeDuration(int value) => Duration = value;

    public void DecreaseDuration()
    {
        if (Duration > 0) Duration--;
    }
}

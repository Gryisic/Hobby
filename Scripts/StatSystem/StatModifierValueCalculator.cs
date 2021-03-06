public static class StatModifierValueCalculator 
{
    public static void UpdateValues(StatModifier modifier, IStats stats)
    {
        ApplyValue(modifier, stats);
    }

    private static void ApplyValue(StatModifier modifier, IStats stats)
    {
        foreach (var stat in stats.GetStats())
        {
            if (modifier.StatsToAffect.Contains(stat.GetTypeOfStat()))
                stat.AddAdditionalValue(CalculatedValue(modifier, stat), modifier);
        }
    }

    private static int CalculatedValue(StatModifier modifier, Stat stat)
    {
        int finalValue = 0;

        if (modifier.Type == ModifierType.Flat) finalValue += (int)modifier.Value;
        else finalValue += (int)(stat.GetBaseValue() * modifier.Value);

        return finalValue;
    }
}

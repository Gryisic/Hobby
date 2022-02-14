public class StatModifierValueCalculator 
{
    public void UpdateValues(StatModifier modifier, IStats stats)
    {
        ApplyValue(modifier, stats);
    }

    private void ApplyValue(StatModifier modifier, IStats stats)
    {
        foreach (var stat in stats.GetStats())
        {
            if (modifier.StatsToAffect.Contains(stat.GetTypeOfStat()))
                stat.AddAdditionalValue(CalculatedValue(modifier, stat), modifier);
        }
    }

    private int CalculatedValue(StatModifier modifier, Stat stat)
    {
        int finalValue = 0;

        if (modifier.Type == ModifierType.Flat) finalValue += (int)modifier.Value;
        else finalValue += (int)(stat.GetBaseValue() * modifier.Value);

        return finalValue;
    }
}

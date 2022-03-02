using System.Collections.Generic;
using UnityEngine;

public class StatSystem 
{
    private Stats _stats = new Stats();
    private List<StatModifier> _modifiers = new List<StatModifier>();

    private int _level;

    public List<Stat> GetStats() => (List<Stat>)_stats.GetStats();

    public void AddModifier(StatModifier modifier)
    {
        _modifiers.Add(modifier);

        StatModifierValueCalculator.UpdateValues(modifier, _stats);
    }

    public bool RemoveModifier(StatModifier modifier)
    {
        if (_modifiers.Remove(modifier)) return true;

        return false;
    }

    public void RemoveAllModifiers()
    {
        _modifiers.Clear();
    }

    private void LevelUp()
    {
        foreach (var stat in _stats.GetStats())
        {
            stat.UpdateRawValue(LevelUpValueCalculator.CalculatedValue(_level, stat.GetBaseValue(), stat.GetIncreaseRatio()));

            Debug.Log(stat.Value);
        }
    }
}

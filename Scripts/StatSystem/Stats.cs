using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType { MaxHealth, Health, MaxEnergy, Energy, Strength, Intelligence, Endurance, Resistance, Agility, Luck }

[ExecuteInEditMode]
[Serializable]
public class Stats : IStats
{
    [SerializeField] private List<Stat> _stats;

    public Stats()
    {
        _stats = new List<Stat>()
        {
            new Stat(StatType.MaxHealth, 0, 0), new Stat(StatType.Health, 0, 0),
            new Stat(StatType.MaxEnergy, 0, 0), new Stat(StatType.Energy, 0, 0),
            new Stat(StatType.Strength, 0, 0), new Stat(StatType.Intelligence, 0, 0),
            new Stat(StatType.Endurance, 0, 0), new Stat(StatType.Resistance, 0, 0),
            new Stat(StatType.Agility, 0, 0), new Stat(StatType.Luck, 0, 0)
        };
    }

    public Stat GetNeededStat(StatType type)
    {
        foreach (var stat in _stats)
        {
            if (stat.GetTypeOfStat() == type) return stat;
        }

        throw new ArgumentException("Нужный стат не найден");
    }

    public IEnumerable<Stat> GetStats()
    {
        return _stats;
    }
}

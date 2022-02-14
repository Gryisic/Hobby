using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat 
{
    [SerializeField] private int _baseValue;
    [SerializeField] private float _increaseRatio;
    private int _rawValue;
    private int _additionalValue;

    private Dictionary<StatModifier, int> _additionalValueSources = new Dictionary<StatModifier, int>();

    [SerializeField] private StatType _type;

    public int Value => _rawValue + _additionalValue;

    public Stat(StatType type, int baseValue, float increaseRatio)
    {
        _type = type;
        _baseValue = baseValue;
        _increaseRatio = increaseRatio;
        _rawValue = _baseValue;
    }

    public int GetBaseValue() => _baseValue;
    public float GetIncreaseRatio() => _increaseRatio;

    public void UpdateRawValue(int value) => _rawValue = value;

    public void AddAdditionalValue(int value, StatModifier source)
    {
        _additionalValue += value;
        _additionalValueSources.Add(source, value);
    }

    public void RemoveAdditionalValue(StatModifier source)
    {
        _additionalValue -= _additionalValueSources[source];
        _additionalValueSources.Remove(source);
    }

    public StatType GetTypeOfStat()
    {
        return _type;
    }
}

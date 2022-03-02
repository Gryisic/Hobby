using System.Collections.Generic;

public class Stat 
{
    private int _baseValue;
    private float _increaseRatio;
    private int _rawValue;
    private int _additionalValue;

    private Dictionary<StatModifier, int> _additionalValueSources = new Dictionary<StatModifier, int>();

    private StatType _type;

    public int Value => _rawValue + _additionalValue;

    public StatType GetTypeOfStat() => _type;

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
}

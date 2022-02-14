using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamagable
{
    [SerializeField] protected Stats _stats = new Stats();
    protected List<StatModifier> _modifiers = new List<StatModifier>();
    protected StatModifierValueCalculator _modifierCalculator = new StatModifierValueCalculator();
    protected LevelUpValueCalculator _levelUpCalculator = new LevelUpValueCalculator();

    protected UnitMovement _movement;
    protected StateMachine _stateMachine;

    [SerializeField] protected int _level;

    public List<Stat> GetStats() => (List<Stat>)_stats.GetStats();

    public void AddModifier(StatModifier modifier)
    {
        _modifiers.Add(modifier);

        _modifierCalculator.UpdateValues(modifier, _stats);
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

    public void TakeDamage(int damage)
    {
        
    }

    protected void LevelUp()
    {
        foreach (var stat in _stats.GetStats())
        {
            stat.UpdateRawValue(_levelUpCalculator.CalculatedValue(_level, stat.GetBaseValue(), stat.GetIncreaseRatio()));

            Debug.Log(stat.Value);
        }
    }
}

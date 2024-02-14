#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Subsystems/Morality")]
public class MoralitySubsystem : ScriptableObject
{
    [SerializeField] private float goodThreshold;
    [SerializeField] private float badThreshold;
    [SerializeField] private float maxBonusAttack;
    [SerializeField] private float maxBonusDefence;
    [NonSerialized] private float _morality;

    public float GetMorality() => _morality;

    public void SetMorality(float value) => _morality = value;

    public void ChangeMorality(float delta) => _morality += delta;

    public EndingType GetEndingType()
    {
        if (_morality >= goodThreshold) return EndingType.Good;
        if (_morality <= badThreshold) return EndingType.Bad;
        return EndingType.Neutral;
    }

    public float GetBonusAttack() => Mathf.Clamp01(_morality / badThreshold) * maxBonusAttack;
    
    public float GetBonusDefence() => Mathf.Clamp01(_morality / goodThreshold) * maxBonusDefence;
}

public enum EndingType
{
    Good,
    Neutral,
    Bad
}

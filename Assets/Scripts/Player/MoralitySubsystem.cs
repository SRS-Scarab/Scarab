#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Subsystems/Morality")]
public class MoralitySubsystem : ScriptableObject
{
    [SerializeField] private float goodThreshold;
    [SerializeField] private float badThreshold;
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
}

public enum EndingType
{
    Good,
    Neutral,
    Bad
}

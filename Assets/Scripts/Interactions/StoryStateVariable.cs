#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Story State")]
public class StoryStateVariable : ScriptableVariable<int>
{
    public string stateName = string.Empty;
    
    [SerializeField]
    private int state;

    [NonSerialized]
    private int? _runtimeState;
    
    public override int Provide()
    {
        return _runtimeState ??= state;
    }

    public override void Consume(int value)
    {
        _runtimeState = value;
    }

    public void NextStage()
    {
        _runtimeState++;
    }
}

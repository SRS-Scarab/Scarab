#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Actions")]
public class ActionsVariable : ScriptableVariable<Actions>
{
    [SerializeField] private InputSubsystem? inputSubsystem;
    [NonSerialized] private Actions? _actions;

    public override Actions Provide()
    {
        if (_actions == null)
        {
            _actions = new Actions();
            _actions.Enable();

            if (inputSubsystem != null) inputSubsystem.PushMap(nameof(_actions.Gameplay));
        }

        return _actions;
    }

    public override void Consume(Actions value) => _actions = value;
}

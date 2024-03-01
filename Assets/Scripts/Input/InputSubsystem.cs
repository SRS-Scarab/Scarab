#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Subsystems/Input")]
public class InputSubsystem : ScriptableObject
{
    [SerializeField] private ActionsVariable? actionsVar;
    [NonSerialized] private readonly Stack<string> _stack = new();
    [NonSerialized] private readonly List<ActionMapMetadata> _metadata = new();
    [NonSerialized] private bool _isMetadataInitialized;
    [NonSerialized] private bool _isMouseOverInterface;

    public bool IsConsumedByInterface() => _isMouseOverInterface;

    public bool IsConsumedByInterface(InputAction.CallbackContext context) => context.control.device == Pointer.current && _isMouseOverInterface;
    
    public void PollMouseStatus() => _isMouseOverInterface = EventSystem.current.IsPointerOverGameObject();
    
    public void PushMap(string mapName)
    {
        TryInitializeMetadata();
        TryDisableCurrent();
        _stack.Push(mapName);
        TryEnableCurrent();
    }

    public void PopMap()
    {
        TryDisableCurrent();
        _stack.Pop();
        TryEnableCurrent();
    }

    private void TryInitializeMetadata()
    {
        if (!_isMetadataInitialized)
        {
            _isMetadataInitialized = true;
            if (actionsVar != null)
            {
                var actions = actionsVar.Provide();
                _metadata.Add(new ActionMapMetadata(nameof(Actions.Gameplay), actions.Gameplay.Enable, actions.Gameplay.Disable));
                _metadata.Add(new ActionMapMetadata(nameof(Actions.UI), actions.UI.Enable, actions.UI.Disable));

                PushMap(nameof(Actions.Gameplay));
                PopMap();
                PushMap(nameof(Actions.UI));
                PopMap();
                // first button input seems to be processed twice if maps are not enabled like so
            }
        }
    }

    private ActionMapMetadata? GetMetadata(string mapName) => _metadata.FirstOrDefault(e => e.MapName == mapName);

    private void TryEnableCurrent()
    {
        if (_stack.TryPeek(out var current))
        {
            var metadata = GetMetadata(current);
            if (metadata != null) metadata.Enable();
        }
    }
    
    private void TryDisableCurrent()
    {
        if (_stack.TryPeek(out var current))
        {
            var metadata = GetMetadata(current);
            if (metadata != null) metadata.Disable();
        }
    }

    private record ActionMapMetadata(string MapName, Action EnableAction, Action DisableAction)
    {
        public void Enable() => EnableAction.Invoke();

        public void Disable() => DisableAction.Invoke();
    }
}

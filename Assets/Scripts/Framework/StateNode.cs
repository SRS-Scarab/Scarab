#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StateNode : MonoBehaviour
{
    [Tooltip("This is set automatically on Awake, change the children list to set up a hierarchy")]
    [SerializeField]
    private StateNode? parent;

    [SerializeField]
    private List<StateNode> children = new();

    [SerializeField]
    private StateNode? current;
    
    [SerializeField]
    private List<Blackboard> blackboards = new();

    [SerializeField]
    private bool isBlocking;
    
    private void Awake()
    {
        foreach (var child in children)
        {
            child.parent = this;
        }
    }

    public StateNode? GetParent()
    {
        return parent;
    }

    public IEnumerable<StateNode> GetChildren()
    {
        return children;
    }

    public StateNode? GetCurrent()
    {
        return current;
    }

    public void SetCurrent(StateNode? node)
    {
        if (current != null && current.isBlocking) return;
        if (node != null && !children.Contains(node)) return;

        if (current != null)
        {
            current.OnExit();
        }
        current = node;
        if (current != null)
        {
            current.OnEnter();
        }
    }
    
    public T? GetBlackboard<T>() where T : Blackboard
    {
        var blackboard = (T?)blackboards.FirstOrDefault(e => e is T);
        if (blackboard == null && parent != null)
        {
            blackboard = parent.GetBlackboard<T>();
        }
        return blackboard;
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    public void SetBlocking(bool target)
    {
        isBlocking = target;
    }
    
    protected virtual void OnEnter()
    {
        if (current != null)
        {
            current.OnEnter();
        }
    }
    
    protected virtual void OnTick(float delta)
    {
        if (current != null)
        {
            current.OnTick(delta);
        }
    }
    
    protected virtual void OnExit()
    {
        if (current != null)
        {
            current.OnExit();
        }
    }
}

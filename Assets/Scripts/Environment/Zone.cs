#nullable enable
using UnityEngine;

public abstract class Zone : MonoBehaviour
{
    public virtual void OnEnter(ZoneChecker checker)
    {
    }

    public virtual void OnTick(ZoneChecker checker)
    {
    }

    public virtual void OnExit(ZoneChecker checker)
    {
    }
}

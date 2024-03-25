#nullable enable
using System;

[Serializable]
public abstract class CreationalFragment : SerializationFragment
{
    protected CreationalFragment() : base(int.MinValue)
    {
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class GeneratorStage : ScriptableObject
{
    public abstract void Generate(Tilemap target);
}

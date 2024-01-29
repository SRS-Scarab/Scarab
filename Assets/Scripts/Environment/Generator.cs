#nullable enable
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private Tilemap? target;
    [SerializeField] private GeneratorStage[] stages = Array.Empty<GeneratorStage>();

    private void Start()
    {
        if (target != null)
        {
            foreach (var stage in stages)
            {
                stage.Generate(target);
            }
        }
    }
}

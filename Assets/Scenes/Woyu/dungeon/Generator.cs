using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private Tilemap target;
    [SerializeField] private GeneratorStage[] stages;

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

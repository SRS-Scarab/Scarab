#nullable enable
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObjectVariable? playerVar;

    [SerializeField]
    private Transform? target;
    
    private void Teleport()
    {
        if (playerVar != null && playerVar.Provide() != null && target != null)
        {
            playerVar.Provide()!.transform.position = target.position;
        }
    }
}

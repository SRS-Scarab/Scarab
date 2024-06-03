#nullable enable
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObjectVariable? playerVar;

    [SerializeField]
    private Transform? target;
    
    public void Teleport()
    {
        if (target != null)
        {
            if (playerVar == null)
            {
                transform.position = target.position;
            }
            else
            {
                if (playerVar.Provide() != null)
                {
                    playerVar.Provide()!.transform.position = target.position;
                }
            }
        }
    }
}

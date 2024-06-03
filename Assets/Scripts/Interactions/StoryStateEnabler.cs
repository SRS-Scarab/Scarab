#nullable enable
using UnityEngine;

public class StoryStateEnabler : MonoBehaviour
{
    [SerializeField]
    private StoryStateVariable? stateVar;

    [SerializeField]
    private int targetState;

    private void Update()
    {
        if (stateVar == null)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(stateVar.Provide() == targetState);
        }
    }
}

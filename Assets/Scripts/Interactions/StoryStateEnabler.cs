#nullable enable
using UnityEngine;

public class StoryStateEnabler : MonoBehaviour
{
    [SerializeField]
    private StoryStateVariable? stateVar;

    [SerializeField]
    private int targetState;

    [SerializeField]
    private EnableCondition condition;

    private void Update()
    {
        if (stateVar == null)
        {
            gameObject.SetActive(true);
        }
        else
        {
            if (condition == EnableCondition.Exact)
            {
                gameObject.SetActive(stateVar.Provide() == targetState);
            }
            else if (condition == EnableCondition.AtMost)
            {
                gameObject.SetActive(stateVar.Provide() <= targetState);
            }
            else if (condition == EnableCondition.AtLeast)
            {
                gameObject.SetActive(stateVar.Provide() >= targetState);
            }
        }
    }
}

public enum EnableCondition
{
    Exact,
    AtMost,
    AtLeast
}

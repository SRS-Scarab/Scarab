#nullable enable
using System;
using UnityEngine;

public class StoryStateEnablerTicker : MonoBehaviour
{
    [SerializeField]
    private StoryStateEnabler[] enablers = Array.Empty<StoryStateEnabler>();
    
    private void Start()
    {
        enablers = FindObjectsOfType<StoryStateEnabler>();
    }

    private void Update()
    {
        foreach (var enabler in enablers)
        {
            if (enabler != null)
            {
                enabler.Tick();
            }
        }
    }
}

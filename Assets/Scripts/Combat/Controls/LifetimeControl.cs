#nullable enable
using UnityEngine;

public class LifetimeControl : MonoBehaviour
{
    public float Lifetime => lifetime;
    
    public float CurrentLifetime
    {
        get => currentLifetime;
        set => currentLifetime = value;
    }
    
    [SerializeField]
    private float lifetime;

    [SerializeField]
    private float currentLifetime;

    private void Start()
    {
        currentLifetime = lifetime;
    }

    private void Update()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}

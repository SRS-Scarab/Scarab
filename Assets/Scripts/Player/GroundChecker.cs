#nullable enable
using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private int groundCounter;

    public bool IsGrounded()
    {
        return groundCounter > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        groundCounter++;
    }

    private void OnTriggerExit(Collider other)
    {
        groundCounter--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        groundCounter++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        groundCounter--;
    }
}
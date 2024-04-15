using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WalkIntoViewAndFollow : MonoBehaviour
{
  private Rigidbody2D rb;
  [SerializeField] private float xDir;
  [SerializeField] private float yDir;
  [SerializeField] private float speed;
  [SerializeField] private float timeToMove;
  private float remainingTime;
  [SerializeField] private GameObject initTarget;
  [SerializeField] private GameObject target;
  [SerializeField] private float distance;
  private AIDestinationSetter destinationSetter;
  private AIPath aipath;

  void Start()
  {
    remainingTime = timeToMove;
    gameObject.SetActive(false);
    destinationSetter = GetComponent<AIDestinationSetter>();
    aipath = GetComponent<AIPath>();
    aipath.maxSpeed = speed;
    destinationSetter.target = initTarget.transform;
  }
  public void FixedUpdate()
  {
    if (remainingTime > 0)
    {
      remainingTime -= Time.deltaTime;
    }
    else
    {
      destinationSetter.target = target.transform;
    }
  }
}

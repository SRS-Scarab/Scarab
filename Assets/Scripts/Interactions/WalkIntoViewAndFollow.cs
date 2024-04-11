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
  }
  public void FixedUpdate()
  {
    if (remainingTime > 0)
    {
      rb = GetComponent<Rigidbody2D>();
      rb.velocity = new Vector2(xDir, yDir).normalized * speed;
      remainingTime -= Time.deltaTime;
    }
    else
    {
      destinationSetter.target = target.transform;
      aipath.maxSpeed = speed;
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  void Start()
  {
    remainingTime = timeToMove;
    gameObject.SetActive(false);
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
      Vector3 difference = (target.transform.position - transform.position);
      Vector2 diff2D = new Vector2(difference.x, difference.y);
      if (diff2D.magnitude >= distance)
      {
        rb.velocity = diff2D.normalized * speed;
      }
      else
      {
        rb.velocity = new Vector2(0, 0);
      }
    }
  }
}

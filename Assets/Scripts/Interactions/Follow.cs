using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
  [SerializeField] private GameObject target;
  [SerializeField] private float distance;
  [SerializeField] private float speed;
  private Rigidbody2D rb;
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    Vector3 difference = (target.transform.position - transform.position);
    Vector2 diff2D = new Vector2(difference.x, difference.y);
    if (diff2D.magnitude >= distance)
    {
      rb.velocity = diff2D.normalized * speed;
    }
  }
}

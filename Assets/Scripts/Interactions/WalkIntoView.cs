using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIntoView : MonoBehaviour
{
  private Rigidbody2D rb;
  [SerializeField] private float xDir;
  [SerializeField] private float yDir;
  [SerializeField] private float speed;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.velocity = new Vector2(xDir, yDir).normalized * speed;
  }
}

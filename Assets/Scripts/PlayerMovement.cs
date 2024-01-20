using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int position;
    // Start is called before the first frame update
    void Start()
    {
        position = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(position, position, position);
        position += 1;
    }
}

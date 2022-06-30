using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;

    private float movement;

    public Vector3 StartPosition;
    private void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayer1)
        {
            movement = Input.GetAxis("Vertical");
        }
        else
        {
            movement = Input.GetAxis("Vertical2");
        }
        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }
    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = StartPosition;
    }
}

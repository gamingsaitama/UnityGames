using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingBounce : MonoBehaviour
{
   // public float BounceForce;
    public Rigidbody2D rb;
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.transform.tag == "Strikers")
         {
             if (Input.touchCount>0)
             {
                 Touch _touch = Input.GetTouch(0);
                 if (_touch.phase == TouchPhase.Ended)
                     collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BounceForce,ForceMode2D.Impulse);
             }
         }
     }*/
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "sling")
        {
            if (Input.touchCount > 0)
            {
                rb.AddForce(new Vector2(0, 100));
                Debug.Log("it works");
            }
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == "Strikers")
        {
            if (collision.transform.tag == "sling")
            {
                if (Input.touchCount > 0)
                {
                    rb.AddForce(new Vector2(0, 100));
                }
            }
        }
        if (collision.transform.tag == "slingoppo")
        {
            if (Input.touchCount > 0)
            {
                rb.AddForce(new Vector2(0, -100));
            }
        }
    }
}

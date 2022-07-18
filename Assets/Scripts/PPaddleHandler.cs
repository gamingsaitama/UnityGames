using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RecInfo.Game.Pingpong.Ball
{
    public class PPaddleHandler : MonoBehaviour
    {
        public bool isPlayer1;
        [SerializeField] float speed;
        public Rigidbody2D rb;
        public Vector3 StartPosition;
        public Transform player1;
        public Transform player2;

        private float movement;
        
        [SerializeField] float touchspeed;

        private void Start()
        {
            StartPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPlayer1)
            {
                movement = Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(rb.velocity.y, movement * speed);
            }

            else
            {
                movement = Input.GetAxisRaw("Vertical2");
                rb.velocity = new Vector2(rb.velocity.y, movement * speed);
            }

            if (Input.touches.Length > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    // Debug.Log("i value>>>> " + Input.touches.Length + i);
                    if (touch.position.x < Screen.width / 2)
                    {
                        player1.position = new Vector2(player1.position.x, player1.position.y + touch.deltaPosition.y * touchspeed * Time.deltaTime);
                    }

                    else
                    {
                        player2.position = new Vector2(player2.position.x, player2.position.y + touch.deltaPosition.y * touchspeed * Time.deltaTime);
                    }
                }
            }
        }

        public void Reset()
        {
            rb.velocity = Vector2.zero;
            transform.position = StartPosition;
        }
    }
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RecInfo.Game.Pingpong.Ball
{
    public class PPaddleHandler : MonoBehaviour
    {
        public bool isPlayer1;
        private float speed;
        public Rigidbody2D rb;
        public Vector3 StartPosition;
        public Transform player1;
        public Transform player2;

        private float movement;
        private Touch touch;

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

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2) 
                {
                    touch = Input.GetTouch(0);
                    player1.position = new Vector2(player1.position.x, player1.position.y + touch.deltaPosition.y * 0.2f * Time.deltaTime);
                }

                else
                {
                    touch = Input.GetTouch(0);
                    player2.position = new Vector2(player2.position.x, player2.position.y + touch.deltaPosition.y * 0.2f * Time.deltaTime);
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

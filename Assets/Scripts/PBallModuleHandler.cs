using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RecInfo.Game.Pingpong.Ball
{
    public class PBallModuleHandler : MonoBehaviour
    {
        public float speed;
        public Rigidbody2D rb;
        public Vector3 StartPosition;

        // Start is called before the first frame update
        void Start()
        {
            StartPosition = transform.position;
            Launch();
        }

        public void Reset()
        {
            rb.velocity = Vector2.zero;
            transform.position = StartPosition;
            Launch();
        }

        private void Launch()
        {
            float x = Random.Range(0, 2) == 0 ? -1 : 1;
            float y = Random.Range(0, 2) == 0 ? -1 : 1;
            rb.velocity = new Vector2(speed * x, speed * y);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RecInfo.Game.Pingpong.Ball
{
    public class PGoals : MonoBehaviour
    {
        public bool isPlayer1Goal;
        
        [SerializeField]
        PPaddleHandler PlayerPaddle;
        [SerializeField]
        PBallModuleHandler ball;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                if (!isPlayer1Goal)
                {
                    ResetAll();
                    Player2LifeCounter.health--; 
                }

                else
                {
                    ResetAll();
                    Player1LifeCounter.health--;
                }
            }

        }

        private void ResetAll()
        {
            PlayerPaddle.Reset();
            ball.Reset();
        }
    }
}


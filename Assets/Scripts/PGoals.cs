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
                    PScoreViewItem.Instance.Player1Scored();
                    ResetAll();
                   // Player2LifeCounter.health--; 
                }

                else
                {
                    PScoreViewItem.Instance.Player2Scored();
                    ResetAll();
                   // Player1LifeCounter.health--;
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


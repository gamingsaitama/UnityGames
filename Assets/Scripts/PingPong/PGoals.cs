using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RecInfo.Game.Pingpong.Ball
{
    public class PGoals : MonoBehaviour
    {
        public bool isPlayer1Goal;
        public static PGoals Instance;
        
        [SerializeField]
        PPaddleHandler PlayerPaddle;
        [SerializeField]
        PBallModuleHandler ball;

        public void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                if (!isPlayer1Goal)
                {
                    PScoreViewItem.Instance.Player1Scored();
                    ResetAll();
                    Player2LifeCounter.health--;
                }

                else
                {
                    PScoreViewItem.Instance.Player2Scored();
                    ResetAll();
                    Player1LifeCounter.health--;
                }
            }

        }

        public void ResetAll()
        {
            PlayerPaddle.Reset();
            ball.Reset();
        }
    }
}


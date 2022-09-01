using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RecInfo.Game.Pingpong.Ball
{
    public class PScoreViewItem : MonoBehaviour
    {
        [Header("Score UI")]
        [SerializeField] TextMeshProUGUI Player1Text;
        [SerializeField] TextMeshProUGUI Player2Text;
        public static PScoreViewItem Instance;

        private int _Player1Score;
        private int _Player2Score;

        private void Start()
        {
            if(Instance==null)
            {
                Instance = this;
            }
        }

        public void Player1Scored()
        {
            _Player1Score++;
            AssignScore(_Player1Score, Player1Text);
        }

        public void Player2Scored()
        {
            _Player2Score++;
            AssignScore(_Player2Score, Player2Text);
        }
        public void ScoreReset()
        {
            _Player1Score = 0;
            AssignScore(_Player1Score, Player1Text);
            _Player2Score = 0;
            AssignScore(_Player2Score, Player2Text);
        }

        private void AssignScore(int score, TextMeshProUGUI scorefield)
        {
            scorefield.text = score.ToString();
        }
    }
   
}
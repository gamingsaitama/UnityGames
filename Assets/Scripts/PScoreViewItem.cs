using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        private void Awake()
        {
            if (Instance==null)
            {
                Instance = this;
            }
        }
        public void Player1Scored()
        {
            _Player1Score++;
            Player1Text.text = _Player1Score.ToString();
        }

        public void Player2Scored()
        {
            _Player2Score++;
            Player2Text.text = _Player2Score.ToString();
        }
    }
}
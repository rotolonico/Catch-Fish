using UnityEngine;
using UnityEngine.UI;

namespace Bars
{
    public class ScoreBar : MonoBehaviour
    {
        private Text score;
        private void Start()
        {
            score = GetComponent<Text>();
        }

        public void UpdateScore()
        {
            score.text = $"SCORE: {Global.Score}";
        }
    }
}

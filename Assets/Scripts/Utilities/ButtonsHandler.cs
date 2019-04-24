using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities
{
    public class ButtonsHandler : MonoBehaviour
    {
        public Transform easyScoresContent;
        public Transform mediumScoresContent;
        public Transform hardScoresContent;
        public Transform progressiveScoresContent;
        public Transform easyScores;
        public Transform mediumScores;
        public Transform hardScores;
        public Transform progressiveScores;
        
        public GameObject scoreGameObject;
        public Canvas mainCanvas;
        public Canvas scoreCanvas;
        public Canvas fallbackCanvas;
        public Canvas gameCanvas;
        public Button back;
        public Text difficultyText;
        
        public AudioSource musicAudioSource;
        public AudioSource soundAudioSource;
        public Image musicImage;
        public Image soundImage;

        public Sprite musicOn;
        public Sprite musicOff;
        public Sprite soundOn;
        public Sprite soundOff;
        
        public void Play(string difficulty)
        {
            Global.Difficulty = difficulty;
            SceneManager.LoadScene(1);
        }

        public void ChooseDifficulty()
        {
            mainCanvas.enabled = false;
            gameCanvas.enabled = true;
        }

        public void Scoreboard()
        {
            back.interactable = false;
            mainCanvas.enabled = false;
            scoreCanvas.enabled = true;
            foreach (Transform child in easyScoresContent.transform) {
                Destroy(child.gameObject);
            }
            foreach (Transform child in mediumScoresContent.transform) {
                Destroy(child.gameObject);
            }
            foreach (Transform child in hardScoresContent.transform) {
                Destroy(child.gameObject);
            }
            foreach (Transform child in progressiveScoresContent.transform) {
                Destroy(child.gameObject);
            }
            
            DatabaseHandler.GetScores(scores =>
            {
                ShowScores(scores.Easy, easyScoresContent);
                ShowScores(scores.Medium, mediumScoresContent);
                ShowScores(scores.Hard, hardScoresContent);
                ShowScores(scores.Progressive, progressiveScoresContent);
                back.interactable = true;
            }, () =>
            {
                mainCanvas.enabled = false;
                scoreCanvas.enabled = false;
                fallbackCanvas.enabled = true;
            });
        }

        private void ShowScores(Dictionary<string, string> scores, Transform scoresContent)
        {
            var scoresInt = new Dictionary<string, int>();
            foreach (var score in scores)
            {
                scoresInt.Add(score.Key, int.Parse(score.Value.Trim('"')));
            }
            var sortedScores = from entry in scoresInt orderby entry.Value descending select entry;
            foreach (var score in sortedScores)
            {
                var newScore = Instantiate(scoreGameObject, scoresContent, false);
                newScore.transform.GetChild(0).GetComponent<Text>().text = score.Key;
                newScore.transform.GetChild(1).GetComponent<Text>().text = score.Value.ToString();
            }
        }

        public void Back()
        {
            mainCanvas.enabled = true;
            scoreCanvas.enabled = false;
            fallbackCanvas.enabled = false;
            gameCanvas.enabled = false;
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Difficulty()
        {
            switch (difficultyText.text)
            {
                case "EASY":
                    easyScores.SetAsFirstSibling();
                    hardScores.SetAsFirstSibling();
                    progressiveScores.SetAsFirstSibling();
                    difficultyText.text = "MEDIUM";
                    break;
                case "MEDIUM":
                    easyScores.SetAsFirstSibling();
                    mediumScores.SetAsFirstSibling();
                    progressiveScores.SetAsFirstSibling();
                    difficultyText.text = "HARD";
                    break;
                case "HARD":
                    easyScores.SetAsFirstSibling();
                    mediumScores.SetAsFirstSibling();
                    hardScores.SetAsFirstSibling();
                    difficultyText.text = "PROGRESSIVE";
                    break;
                default:
                    mediumScores.SetAsFirstSibling();
                    hardScores.SetAsFirstSibling();
                    progressiveScores.SetAsFirstSibling();
                    difficultyText.text = "EASY";
                    break;
            }
        }

        public void Music()
        {
            if (musicAudioSource.mute)
            {
                musicImage.sprite = musicOn;
                musicAudioSource.mute = false;
            }
            else
            {
                musicImage.sprite = musicOff;
                musicAudioSource.mute = true;
            }
        }

        public void Sound()
        {
            if (soundAudioSource.mute)
            {
                soundImage.sprite = soundOn;
                soundAudioSource.mute = false;
            }
            else
            {
                soundImage.sprite = soundOff;
                soundAudioSource.mute = true;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities
{
    public class ScoreButtonHandler : MonoBehaviour
    {
        public InputField name;
        
        public void Submit()
        {
            DatabaseHandler.PostScore(name.text, Global.Score, Global.Difficulty, BackToMainMenu);
        }
        
        public static void BackToMainMenu()
        {
            Destroy(GameObject.Find("Sound"));
            Destroy(GameObject.Find("Music"));
            Global.InGame = false;
            Global.Score = 0;
            SceneManager.LoadScene(0);
        }
    }
}

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
            DatabaseHandler.PostScore(name.text, Global.Score, () =>
            {
                Destroy(GameObject.Find("Sound"));
                Destroy(GameObject.Find("Music"));
                Global.Score = 0;
                SceneManager.LoadScene(0);
            });
        }
    }
}

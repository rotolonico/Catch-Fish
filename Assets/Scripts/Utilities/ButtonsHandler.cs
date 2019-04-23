using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class ButtonsHandler : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void Scoreboard()
        {
            
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

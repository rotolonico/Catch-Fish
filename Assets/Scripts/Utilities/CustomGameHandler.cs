using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class CustomGameHandler : MonoBehaviour
    {
        public InputField customPlayerSpeed;
        public InputField customFishSpeed;
        public InputField customBubblesSpeed;
        public InputField customMaxOxygen;
        public InputField customOxygenFactor;
        public InputField customProbabilityGoodFish;

        private ButtonsHandler buttonsHandler;

        private void Start()
        {
            buttonsHandler = GetComponent<ButtonsHandler>();
        }

        public void CreateCustomGame()
        {
            
            float playerSpeed = float.TryParse(customPlayerSpeed.text, out playerSpeed) ? playerSpeed : (float) Global.rnd.NextDouble() * 20;
            if (playerSpeed < 0) playerSpeed = 0;
            if (playerSpeed > 20) playerSpeed = 20;
            Global.CustomPlayerSpeed = playerSpeed;

            float fishSpeed = float.TryParse(customFishSpeed.text, out fishSpeed) ? fishSpeed : (float) Global.rnd.NextDouble() * 20;
            if (fishSpeed < 0) fishSpeed = 0;
            if (fishSpeed > 20) fishSpeed = 20;
            Global.CustomFishSpeed = fishSpeed;

            float bubblesSpeed = float.TryParse(customBubblesSpeed.text, out bubblesSpeed) ? bubblesSpeed : (float) Global.rnd.NextDouble() * 20;
            if (bubblesSpeed < 0) bubblesSpeed = 0;
            if (bubblesSpeed > 20) bubblesSpeed = 20;
            Global.CustomBubblesSpeed = bubblesSpeed;

            float maxOxygen = float.TryParse(customMaxOxygen.text, out maxOxygen) ? maxOxygen : (float) Global.rnd.NextDouble() * 95 + 5;
            if (maxOxygen < 5) maxOxygen = 5;
            if (maxOxygen > 100) maxOxygen = 100;
            Global.CustomMaxOxygen = maxOxygen;

            float oxygenFactor = float.TryParse(customOxygenFactor.text, out oxygenFactor) ? 1 / oxygenFactor : 1 / ((float) Global.rnd.NextDouble() / 100);
            if (oxygenFactor < 0) oxygenFactor = 0;
            if (oxygenFactor > 100) oxygenFactor = 100;
            Global.CustomOxygenFactor = oxygenFactor;

            float probabilityGoodFish = float.TryParse(customProbabilityGoodFish.text, out probabilityGoodFish) ? probabilityGoodFish : (float) Global.rnd.NextDouble() * 100;
            if (probabilityGoodFish < 0) probabilityGoodFish = 0;
            if (probabilityGoodFish > 100) probabilityGoodFish = 100;
            Global.CustomProbabilityGoodFish = probabilityGoodFish;
            
            buttonsHandler.Play("Custom");
        }
    }
}

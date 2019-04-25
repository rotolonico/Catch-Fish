using System.Collections;
using Fish;
using UnityEngine;

namespace Generators
{
    public class FishGenerator : MonoBehaviour
    {
        public GameObject[] fishes;

        public float probability;

        private int menuSpawnsCount;

        private void Start()
        {
            switch (Global.Difficulty)
            {
                case "Easy":
                    probability = 50;
                    break;
                case "Medium":
                    probability = 15;
                    break;
                case "Hard":
                    probability = 0;
                    break;
                case "Progressive":
                    probability = 75;
                    break;
                default:
                    probability = Global.CustomProbabilityGoodFish;
                    break;
            }

            if (!Global.InGame) probability = 0;
            
            StartCoroutine(GenerateFish());
        }

        private void Update()
        {
            if (Global.Difficulty == "Progressive" && probability > 0) probability -= Time.deltaTime * Global.EscalationFactor * 10;
            if (probability < 0) probability = 0;
        }

        private IEnumerator GenerateFish()
        {
            if (!Global.InGame) menuSpawnsCount++;
            if (!Global.InGame && menuSpawnsCount > 50) Destroy(GetComponent<FishGenerator>());
            yield return new WaitForSeconds((float) Global.rnd.NextDouble() * 3);
            var fishPosition = new Vector3(transform.position.x + Global.rnd.Next(-10,11), Global.rnd.Next(-5,6) ,0);
            var fishType = Global.rnd.Next(fishes.Length);
            if (Global.rnd.NextDouble() < probability / 100) fishType = Global.FishType;
            if (Vector3.Distance(fishPosition, transform.position) > 2) Instantiate(fishes[fishType], fishPosition, Quaternion.identity);
            StartCoroutine(GenerateFish());
        }
    }
}

using System.Collections;
using Fish;
using UnityEngine;

namespace Generators
{
    public class FishGenerator : MonoBehaviour
    {
        public GameObject[] fishes;

        private void Start()
        {
            StartCoroutine(GenerateFish());
        }

        private IEnumerator GenerateFish()
        {
            if (!Global.InGame) yield break;
            yield return new WaitForSeconds((float) Global.rnd.NextDouble() * 3);
            var fishPosition = new Vector3(transform.position.x + Global.rnd.Next(-10,11), Global.rnd.Next(-5,6) ,0);
            var fishType = Global.rnd.Next(fishes.Length);
            if (Vector3.Distance(fishPosition, transform.position) > 2) Instantiate(fishes[fishType], fishPosition, Quaternion.identity);
            StartCoroutine(GenerateFish());
        }
    }
}

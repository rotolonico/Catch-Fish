using System.Collections;
using UnityEngine;

namespace Generators
{
    public class BubbleGenerator : MonoBehaviour
    {
        public GameObject bubble;

        private void Start()
        {
            StartCoroutine(GenerateBubble());
        }

        private IEnumerator GenerateBubble()
        {
            if (!Global.InGame) yield break;
            yield return new WaitForSeconds((float) Global.rnd.NextDouble() * 3);
            var bubblePosition = new Vector3(Global.rnd.Next(-10,11), -5 ,0);
            Instantiate(bubble, bubblePosition, Quaternion.identity);
            StartCoroutine(GenerateBubble());
        }
    }
}

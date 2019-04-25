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
            yield return new WaitForSeconds((float) Global.rnd.NextDouble() * 1.5f);
            var bubblePosition = new Vector3(transform.position.x + Global.rnd.Next(-10,11), -5 ,0);
            Instantiate(bubble, bubblePosition, Quaternion.identity);
            StartCoroutine(GenerateBubble());
        }
    }
}

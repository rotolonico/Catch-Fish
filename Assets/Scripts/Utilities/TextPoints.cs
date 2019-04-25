using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class TextPoints : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(AnimationCoroutine());
        }

        private IEnumerator AnimationCoroutine()
        {
            GetComponent<Animator>().Play("TextPoint");
            yield return new WaitForSeconds(3);
            Destroy(transform.parent.gameObject);
        }
    }
}

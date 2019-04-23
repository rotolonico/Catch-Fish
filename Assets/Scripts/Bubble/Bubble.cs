using System.Collections;
using UnityEngine;

namespace Bubble
{
    public class Bubble : MonoBehaviour
    {
        public float speed;
    
        private Rigidbody2D rb;

        private int direction;
    
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            StartCoroutine(ChangeDirection());
        }

        private IEnumerator ChangeDirection()
        {
            if (!Global.InGame) yield break;
            direction = Global.rnd.Next(0, 2);
            yield return new WaitForSeconds((float)Global.rnd.NextDouble());
            StartCoroutine(ChangeDirection());
        }

        private void Update()
        {
            var position = transform.position;
            rb.MovePosition(direction == 0 ? position + (Vector3.up + Vector3.left) * Time.deltaTime * speed : position + (Vector3.up + Vector3.right) * Time.deltaTime * speed);
        }
    }
}

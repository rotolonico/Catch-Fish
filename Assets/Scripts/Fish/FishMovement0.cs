using System.Collections;
using UnityEngine;

namespace Fish
{
    public class FishMovement0 : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer sr;

        public int speed;

        private int direction;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            StartCoroutine(ChangeDirection());
        }
        
        private IEnumerator ChangeDirection()
        {
            if (!Global.InGame) yield break;
            direction = Global.rnd.Next(0, 2);
            sr.flipX = direction == 1;
            yield return new WaitForSeconds((float)Global.rnd.NextDouble() * 2);
            StartCoroutine(ChangeDirection());
        }
        

        private void Update()
        {
            var position = transform.position;
            rb.MovePosition(direction == 0 ? position + Vector3.left * Time.deltaTime * speed : position + Vector3.right * Time.deltaTime * speed);
        }
    }
}

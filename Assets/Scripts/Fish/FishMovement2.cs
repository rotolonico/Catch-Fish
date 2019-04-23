using System.Collections;
using UnityEngine;

namespace Fish
{
    public class FishMovement2 : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer sr;

        public int speed;

        private Vector2 direction;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            StartCoroutine(ChangeDirection());
        }
        
        private IEnumerator ChangeDirection()
        {
            if (!Global.InGame) yield break;
            direction = new Vector2((float) Global.rnd.NextDouble() * 2, (float) Global.rnd.NextDouble() * 2);
            sr.flipX = direction.x < 1;
            yield return new WaitForSeconds((float) Global.rnd.NextDouble() * 2);
            StartCoroutine(ChangeDirection());
        }
        

        private void Update()
        {
            var position = transform.position;
            if (direction.x > 1) direction.x -= 2;
            if (direction.y > 1) direction.y -= 2;
            rb.MovePosition(position + (Vector3) direction * Time.deltaTime * speed);
        }
    }
}

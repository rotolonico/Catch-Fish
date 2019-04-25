using System.Collections;
using UnityEngine;

namespace Fish
{
    public class FishMovement2 : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer sr;

        public float speed;

        private Vector2 direction;

        private void Start()
        {
            switch (Global.Difficulty)
            {
                case "Easy":
                    speed = 1;
                    break;
                case "Medium":
                    speed = 2;
                    break;
                case "Hard":
                    speed = 4;
                    break;
                case "Progressive":
                    speed = Global.Speed;
                    break;
                default:
                    speed = Global.CustomFishSpeed;
                    break;
            }
            
            if (!Global.InGame) speed = 2;
            
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            StartCoroutine(ChangeDirection());
        }
        
        private IEnumerator ChangeDirection()
        {
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
            
            if (Global.Difficulty == "Progressive") speed = Global.Speed;
        }
    }
}

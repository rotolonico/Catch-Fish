using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Fish
{
    public class FishMovement1 : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer sr;

        public int speed;

        private Vector2 direction = Vector2.zero;

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
                default:
                    speed = 4;
                    break;
            }
            
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            direction = new Vector2(Global.rnd.Next(0, 2), Global.rnd.Next(0, 2));
            sr.flipX = direction.x > 0;
        }

        private void OnCollisionEnter2D()
        {
            direction.y = -direction.y;
        }

        private void Update()
        {
            var position = transform.position;
            if (Math.Abs(direction.x) < 0.1f) direction.x = -1;
            if (Math.Abs(direction.y) < 0.1f) direction.y = -1;
            rb.MovePosition(position + (Vector3) direction * Time.deltaTime * speed);
        }
    }
}

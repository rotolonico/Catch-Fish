﻿using System.Collections;
using UnityEngine;

namespace Fish
{
    public class FishMovement0 : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer sr;

        public float speed;

        private int direction;

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
            direction = Global.rnd.Next(0, 2);
            sr.flipX = direction == 1;
            yield return new WaitForSeconds((float)Global.rnd.NextDouble() * 2);
            StartCoroutine(ChangeDirection());
        }
        

        private void Update()
        {
            var position = transform.position;
            rb.MovePosition(direction == 0 ? position + Vector3.left * Time.deltaTime * speed : position + Vector3.right * Time.deltaTime * speed);
            
            if (Global.Difficulty == "Progressive") speed = Global.Speed;
        }
    }
}

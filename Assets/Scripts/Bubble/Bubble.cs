using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bubble
{
    public class Bubble : MonoBehaviour
    {
        public float speed;
        public float upwardsSpeed;
    
        private Rigidbody2D rb;

        private int direction;
    
        private void Start()
        {
            switch (Global.Difficulty)
            {
                case "Easy":
                    upwardsSpeed = 2;
                    break;
                case "Medium":
                    upwardsSpeed = 3;
                    break;
                case "Hard":
                    upwardsSpeed = 4;
                    break;
                case "Progressive":
                    upwardsSpeed = Global.UpwardsSpeed;
                    break;
                default:
                    upwardsSpeed = Global.CustomBubblesSpeed;
                    break;
            }

            if (!Global.InGame) upwardsSpeed = 3;
            
            rb = GetComponent<Rigidbody2D>();
            StartCoroutine(ChangeDirection());
        }

        private IEnumerator ChangeDirection()
        {
            direction = Global.rnd.Next(0, 2);
            yield return new WaitForSeconds((float)Global.rnd.NextDouble());
            StartCoroutine(ChangeDirection());
        }

        private void Update()
        {
            var position = transform.position;
            rb.MovePosition(direction == 0 ? position + (Vector3.up * upwardsSpeed + Vector3.left * speed) * Time.deltaTime : position + (Vector3.up * upwardsSpeed + Vector3.right * speed) * Time.deltaTime * speed);

            if (Global.Difficulty == "Progressive") upwardsSpeed = Global.UpwardsSpeed;
        }
    }
}

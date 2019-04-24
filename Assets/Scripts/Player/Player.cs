using System.Collections;
using Bars;
using Fish;
using UnityEngine;
using Utilities;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public float speed;
        public float maxOxygen;
        public RectTransform oxygenBar;

        private int range = 3;
        private float oxygen;
        private Rigidbody2D rb;
        private Camera mainCamera;

        private ScoreBar scoreBar;
        private FishBar fishBar;
        private SoundHandler soundHandler;
        

        private void Start()
        {
            switch (Global.Difficulty)
            {
                case "Easy":
                    maxOxygen = 25;
                    break;
                case "Medium":
                    maxOxygen = 20;
                    break;
                default:
                    maxOxygen = 15;
                    break;
            }
            
            mainCamera = Camera.main;
            rb = GetComponent<Rigidbody2D>();
            oxygen = maxOxygen;
            StartCoroutine(UpdateScore());
            scoreBar = GameObject.Find("ScoreText").GetComponent<ScoreBar>();
            fishBar = GameObject.Find("FishImage").GetComponent<FishBar>();
            NewFishType();
            soundHandler = GameObject.Find("Sound").GetComponent<SoundHandler>();
        }

        private void NewFishType()
        {
            Global.FishType = Global.rnd.Next(range);
            fishBar.UpdateScore();
        }

        private void Update()
        {
            UpdateOxygen();
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            var newPosition = transform.position;
            rb.velocity = Vector2.zero;
            newPosition.x += Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
            newPosition.y += Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            transform.position = newPosition;
            var cameraTransform = mainCamera.transform;
            cameraTransform.position = new Vector3(newPosition.x, cameraTransform.position.y, -10);
        }

        private IEnumerator UpdateScore()
        {
            if (!Global.InGame) yield break;
            yield return new WaitForSeconds(3);
            Global.Score++;
            scoreBar.UpdateScore();
            StartCoroutine(UpdateScore());
        }

        private void UpdateOxygen()
        {
            if (oxygen < 0) EndGame();
            if (oxygen > maxOxygen) oxygen = maxOxygen;
            oxygen -= Time.deltaTime;
            var oxygenBarPosition = oxygenBar.anchoredPosition;
            oxygenBarPosition = new Vector2(oxygen / maxOxygen * 300 - 300, oxygenBarPosition.y);
            oxygenBar.anchoredPosition = oxygenBarPosition;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Bubble")) return;
            oxygen += maxOxygen / 10;
            soundHandler.PlayEatBubble();
            Destroy(other.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Fish")) return;
            if (other.gameObject.GetComponent<FishType>().fishType == Global.FishType)
            {
                soundHandler.PlayEatGood();
                Global.Score += 10;
                NewFishType();
            }
            else
            {
                soundHandler.PlayEatBad();
                Global.Score -= Global.Score > 5 ? 5 : Global.Score;
            }
            
            scoreBar.UpdateScore();
            Destroy(other.gameObject);
        }

        private void EndGame()
        {
            GameObject.Find("ScoreCanvas").GetComponent<Canvas>().enabled = true;
            Destroy(gameObject);
        }
    }
}

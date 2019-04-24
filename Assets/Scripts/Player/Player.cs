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
        public float oxygenFactor;
        public float maxOxygen;
        public RectTransform oxygenBar;

        public Sprite player;
        public Sprite playerSwimming;

        private int range = 3;
        private float oxygen;
        private Rigidbody2D rb;
        private Camera mainCamera;

        private ScoreBar scoreBar;
        private FishBar fishBar;
        private SoundHandler soundHandler;
        private SpriteRenderer sr;

        private float lastMovement;
        

        private void Start()
        {
            switch (Global.Difficulty)
            {
                case "Easy":
                    maxOxygen = 25;
                    oxygenFactor = 15;
                    break;
                case "Medium":
                    maxOxygen = 20;
                    oxygenFactor = 17;
                    break;
                case "Hard":
                    maxOxygen = 15;
                    oxygenFactor = 17;
                    break;
                default:
                    maxOxygen = 30;
                    oxygenFactor = 10;
                    break;
            }
            
            mainCamera = Camera.main;
            sr = GetComponent<SpriteRenderer>();
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

            if (Global.Difficulty != "Progressive") return;
            Global.upwardsSpeed += Time.deltaTime * Global.EscalationFactor;
            Global.speed += Time.deltaTime * Global.EscalationFactor;
            speed = Global.speed;
            maxOxygen -= Time.deltaTime * Global.EscalationFactor;
            if (oxygenFactor < 17) oxygenFactor += Time.deltaTime * Global.EscalationFactor;
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

            if (lastMovement <= 0.5f && Input.GetAxisRaw("Horizontal") > 0.5f)
            {
                sr.sprite = playerSwimming;
                sr.flipX = false;
                transform.eulerAngles = new Vector3(0, 0, -90);
            } else if (lastMovement >= -0.5f && Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                sr.sprite = playerSwimming;
                sr.flipX = true;
                transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (Input.GetAxisRaw("Horizontal") > -0.5f && Input.GetAxisRaw("Horizontal") < 0.5f && (lastMovement >= 0.5f || lastMovement <= -0.5f))
            {
                sr.sprite = player;
                sr.flipX = false;
                transform.eulerAngles = Vector3.zero;
            }
            
            lastMovement = Input.GetAxisRaw("Horizontal");
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
            oxygen += maxOxygen / oxygenFactor;
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

using UnityEngine;

namespace Utilities
{
    public class SoundHandler : MonoBehaviour
    {
        public AudioClip eatGood;
        public AudioClip eatBad;
        public AudioClip eatBubble;

        public AudioSource audioSource;

        public void PlayEatGood()
        {
            audioSource.clip = eatGood;
            audioSource.Play();
        }
        
        public void PlayEatBad()
        {
            audioSource.clip = eatBad;
            audioSource.Play();
        }
        
        public void PlayEatBubble()
        {
            audioSource.clip = eatBubble;
            audioSource.Play();
        }
    }
}


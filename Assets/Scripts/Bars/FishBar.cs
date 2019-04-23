using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bars
{
    public class FishBar : MonoBehaviour
    {
        public Sprite[] fishes;
        public Vector2[] sizes;

        private RectTransform fishRectTransform;
        private Image fishImage;
        
        private void Start()
        {
            fishRectTransform = GetComponent<RectTransform>();
            fishImage = GetComponent<Image>();
        }

        public void UpdateScore()
        {
            try
            {
                fishImage.sprite = fishes[Global.FishType];
                fishRectTransform.sizeDelta = sizes[Global.FishType];
            }
            catch (Exception)
            {
                fishImage = GetComponent<Image>();
                fishRectTransform = GetComponent<RectTransform>();
                fishImage.sprite = fishes[Global.FishType];
                fishRectTransform.sizeDelta = sizes[Global.FishType];
            }
            
        }
    }
}

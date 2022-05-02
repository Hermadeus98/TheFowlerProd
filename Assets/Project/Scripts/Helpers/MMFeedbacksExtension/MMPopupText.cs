using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("TheFowler/Popup Text")]
    public class MMPopupText : MMFeedback
    {
        [TextArea(3,5)] public string message = "null message";
        public float duration = 1.2f;
        
        public Vector3 offsetSpawningPos = new Vector3(0f,0f,0f);
        public Vector3 offsetMovement = new Vector3(0f,1f,0f);

        public Color color = Color.red;

        public Sprite popupSprite = null;
        public Sprite extraImage;

        public bool setSize = false;
        [SerializeField] public float sizePercent = 0;

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            var popup = Spawnables.Instance.PopupText;
            var instance = Instantiate(popup);

            if(extraImage == null)
                instance.extraImage.gameObject.SetActive(false);
            else
            {
                instance.extraImage.gameObject.SetActive(true);
                instance.extraImage.sprite = extraImage;
            }

            if (setSize)
            {
                instance.SetSizePercent(sizePercent);
            }

            instance.popupSprite = this.popupSprite;
            
            instance.Color = color;
            instance.transform.SetParent(transform);
            instance.transform.position = transform.position + offsetSpawningPos;
            instance.duration = duration;
            instance.offset = offsetMovement;
            instance.Play(message);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace TheFowler
{
    public class AnimatedText : SerializedMonoBehaviour
    {
        [SerializeField, GetComponent] private TextMeshProUGUI textComponent;
        [SerializeField] private float displayDuration = 0.01f;

        [SerializeField]
        private Dictionary<char, AnimatedTextException> exceptions = new Dictionary<char, AnimatedTextException>();

        public TextMeshProUGUI TextComponent => textComponent;
        public bool isComplete;
        
        private string textToDisplay;

        private Coroutine displayCor;
        
        public void SetText(string text)
        {
            textToDisplay = text;
            if(displayCor != null) StopCoroutine(displayCor);
            displayCor = StartCoroutine(Display());
        }

        public void Append(string text)
        {
            textToDisplay += text;
            if(displayCor != null) StopCoroutine(displayCor);
            displayCor = StartCoroutine(Display(false));
        }

        private IEnumerator Display(bool clear = true)
        {
            isComplete = false;
            if(clear) textComponent.SetText(string.Empty);

            if (!string.IsNullOrEmpty(textToDisplay))
            {
                for (int i = 0; i < textToDisplay.Length; i++)
                {
                    var character = textToDisplay[i];
                    AnimatedTextException exception = null;
                    exceptions?.TryGetValue(character, out exception);
                    if (exception != null)
                    {
                        textComponent.text += textToDisplay[i];
                        yield return new WaitForSeconds(exception.overrideDuration);
                    }
                    else
                    {
                        textComponent.text += textToDisplay[i];
                        yield return new WaitForSeconds(displayDuration * Time.deltaTime);
                    }
                }
            }

            isComplete = true;
            yield break;
        }

        public void Complete()
        {
            isComplete = true;
            textComponent.SetText(textToDisplay);
        }

        public void SetAndFinishText(string text)
        {
            textToDisplay = text;
            Complete();
        }
    }

    [Serializable]
    public class AnimatedTextException
    {
        public float overrideDuration;
    }
}

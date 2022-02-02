using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class DialogueMovementView : UIView
    {
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is DialogueArg cast)
            {
                Add(cast);
            }
        }
        
        [SerializeField] private RectTransform container;
        [SerializeField] private RectTransform[] plugs;

        [SerializeField] private Queue<DialogueUIElement> queue = new Queue<DialogueUIElement>();

        public DialogueUIElement currentDialogueElement;

        private int a;

        [Button]
        public void Add(DialogueArg arg)
        {
            a++;
            var uiElement = Instantiate(Spawnables.Instance.DialogueUIElement, container);
            currentDialogueElement = uiElement;
            uiElement.transform.SetParent(plugs[0]);
            uiElement.RectTransform.anchoredPosition = new Vector3(-uiElement.RectTransform.sizeDelta.x, 0f, 0f);
            uiElement.Refresh(arg);
            uiElement.Show();

            uiElement.name = a.ToString();
            queue.Enqueue(uiElement);
            ReAgenceUI();
        }

        private void ReAgenceUI()
        {
            if (queue.Count > 4)
            {
                var element = queue.Dequeue();
                Destroy(element.gameObject);
            }

            for (int i = 0; i < queue.Count; i++)
            {
                var element = queue.ElementAt(i);
                element.transform.SetParent(plugs[queue.Count - 1 - i]);
                element.RectTransform.DOAnchorPosY(0, .2f);
            }
        }

        public override void Hide()
        {
            //base.Hide();
            StartCoroutine(HideCoroutine());
        }

        private IEnumerator HideCoroutine()
        {
            for (int i = 0; i < 3; i++)
            {
                var element = queue.Peek();
                yield return element.DestroyElement(1f);
                
                Add(new DialogueArg()
                {
                    Dialogue = null,
                });
            }

            queue.ForEach(w => w.DestroyElement(0.1f));
            queue.Clear();
            
            openTween = CanvasGroup.DOFade(0f, .1f);
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = true;
            
            /*queue.ForEach(w => Destroy(w.gameObject));
            queue.Clear();*/
        }
    }
}

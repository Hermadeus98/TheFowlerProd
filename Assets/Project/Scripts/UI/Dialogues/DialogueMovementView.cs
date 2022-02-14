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
        [TabGroup("References")]
        [SerializeField] private RectTransform container;
        
        [TabGroup("References")]
        [SerializeField] private RectTransform[] plugs;

        [TabGroup("References")]
        [SerializeField] private DialogueChoiceSelector choiceSelector;
        
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private Queue<DialogueUIElement> queue = new Queue<DialogueUIElement>();

        [TabGroup("Debug")]
        [SerializeField, ReadOnly] public DialogueUIElement currentDialogueElement;

        private List<DialogueUIElement> bin = new List<DialogueUIElement>();
        
        public DialogueChoiceSelector ChoiceSelector => choiceSelector;

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            ChapterManager.onChapterChange += delegate(Chapter chapter) { Hide(); };
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            ChapterManager.onChapterChange -= delegate(Chapter chapter) { Hide(); };
        }

        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is DialogueArg cast)
            {
                Add(cast);
            }
        }
        
        [Button]
        public void Add(DialogueArg arg)
        {
            var uiElement = Instantiate(Spawnables.Instance.DialogueUIElement, container);
            currentDialogueElement = uiElement;
            uiElement.transform.SetParent(plugs[0]);
            uiElement.RectTransform.anchoredPosition = new Vector3(-uiElement.RectTransform.sizeDelta.x, 0f, 0f);
            uiElement.Refresh(arg);
            uiElement.Show();

            bin.Add(uiElement);
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

            for (var i = 0; i < queue.Count; i++)
            {
                var element = queue.ElementAt(i);
                element.transform.SetParent(plugs[queue.Count - 1 - i]);
                element.RectTransform.DOAnchorPosY(0, .2f);
            }
        }

        public override void Hide()
        {
            if(isActive)
                StartCoroutine(HideCoroutine());
            
            base.Hide();
        }

        private IEnumerator HideCoroutine()
        {
            for (int i = 0; i < 3; i++)
            {
                var element = queue.Peek();
                yield return element.HideElement(1f);
                
                Add(new DialogueArg()
                {
                    Dialogue = null,
                });
            }

            queue.ForEach(w => w?.HideElement(0.1f));
            queue.Clear();
            
            openTween = CanvasGroup.DOFade(0f, .1f);
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = true;

            yield return new WaitForSeconds(1f);
            
            for (int i = 0; i < plugs.Length; i++)
            {
                for (int j = 0; j < plugs[i].childCount; j++)
                {
                    Destroy(plugs[i].transform.GetChild(j).GetComponent<DialogueUIElement>().gameObject, 1f);
                }
            }
        }
        
        public void SetChoices(DialogueNode dialogueNode)
        {
            if (dialogueNode.hasMultipleChoices)
            {
                choiceSelector.Refresh(dialogueNode.children.Cast<DialogueNode>().ToArray());
                return;
            }
        }
    }
}

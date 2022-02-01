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

        private int a;

        [Button]
        public void Add(DialogueArg arg)
        {
            a++;
            var uiElement = Instantiate(Spawnables.Instance.DialogueUIElement, container);
            uiElement.transform.SetParent(plugs[0]);
            uiElement.RectTransform.anchoredPosition = new Vector3(-uiElement.RectTransform.sizeDelta.x, 0f, 0f);
            Debug.Log(arg);
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
            base.Hide();
            queue.ForEach(w => Destroy(w.gameObject));
            queue.Clear();
        }
    }
}

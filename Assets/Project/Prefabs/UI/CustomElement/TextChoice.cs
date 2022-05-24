using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextChoice : MonoBehaviour
{
    [Serializable]
    public class TextOption
    {
        public string text;
        [TextArea(2,4)] public string description;
        public UnityEvent OnSelected;
    }

    public TextOption[] options;
    public TextOption CurrentOption => options[current];

    private int current;
    
    public Image rightArrow, leftArrow;
    private Tween rTween, lTween;
    
    public void Initialize()
    {
        current = 0;
        GetComponent<TextMeshProUGUI>().SetText(options[current].text);

        options.ForEach(w => w.OnSelected.AddListener(delegate
        {
            FindObjectsOfType<DescriptionText>().ForEach(x => x.UpdateText(w.description));
        }));
    }

    public void Next()
    {
        current++;
        if (current >= options.Length)
            current = 0;
        GetComponent<TextMeshProUGUI>().SetText(options[current].text);
        options[current].OnSelected?.Invoke();
        
        rTween?.Kill();
        rTween = rightArrow.DOFade(.5f, .2f).OnComplete(delegate { rightArrow.DOFade(1f, .1f); });
    }

    public void Previous()
    {
        current--;
        if (current < 0)
            current = options.Length - 1;
        GetComponent<TextMeshProUGUI>().SetText(options[current].text);
        options[current].OnSelected?.Invoke();
        
        lTween?.Kill();
        lTween = leftArrow.DOFade(.5f, .2f).OnComplete(delegate { leftArrow.DOFade(1f, .1f); });
    }
}

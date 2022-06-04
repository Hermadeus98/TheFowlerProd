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
        public string textFrench;
        [TextArea(2,4)] public string description;
        [TextArea(2,4)] public string descriptionFrench;
        public UnityEvent OnSelected;
    }

    public TextOption[] options;
    public TextOption CurrentOption => options[current];

    private int current;
    
    public Image rightArrow, leftArrow;
    private Tween rTween, lTween;

    private void OnEnable()
    {
        LocalisationManager.Register(() => SetDescriptionText());
        LocalisationManager.Register(() => SetTitleText());
    }

    private void OnDisable()
    {
        LocalisationManager.UnRegister(() => SetDescriptionText());
        LocalisationManager.UnRegister(() => SetTitleText());
    }
    public void Initialize()
    {
        current = 0;
        SetTitleText();

        SetDescriptionText();
    }

    public void Next()
    {
        current++;
        if (current >= options.Length)
            current = 0;

        SetTitleText();

        options[current].OnSelected?.Invoke();
        
        rTween?.Kill();
        rTween = rightArrow.DOFade(.5f, .2f).OnComplete(delegate { rightArrow.DOFade(1f, .1f); });
    }

    public void Previous()
    {
        current--;
        if (current < 0)
            current = options.Length - 1;

        SetTitleText();

        options[current].OnSelected?.Invoke();
        
        lTween?.Kill();
        lTween = leftArrow.DOFade(.5f, .2f).OnComplete(delegate { leftArrow.DOFade(1f, .1f); });
    }

    private void SetDescriptionText()
    {
        if(LocalisationManager.language == Language.ENGLISH)
        {

            options.ForEach(w => w.OnSelected.AddListener(delegate
            {
                FindObjectsOfType<DescriptionText>().ForEach(x => x.UpdateText(w.description));
            }));
        }
        else
        {
            options.ForEach(w => w.OnSelected.AddListener(delegate
            {
                FindObjectsOfType<DescriptionText>().ForEach(x => x.UpdateText(w.descriptionFrench));
            }));
        }

    }

    private void SetTitleText()
    {
        if (LocalisationManager.language == Language.ENGLISH)
        {
            GetComponent<TextMeshProUGUI>().SetText(options[current].text);
        }
        else
        {
            GetComponent<TextMeshProUGUI>().SetText(options[current].textFrench);
        }

    }
}

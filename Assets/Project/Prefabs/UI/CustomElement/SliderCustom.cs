using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderCustom : MonoBehaviour
{
    public List<SliderElement> SliderElements = new List<SliderElement>();

    private int currentValue, maxValue;

    public void Generate()
    {
        maxValue = transform.childCount;
        currentValue = maxValue;
        
        for (int i = 0; i < maxValue; i++)
        {
            SliderElements.Add(new SliderElement()
            {
                fill = transform.GetChild(i).GetChild(0).GetComponent<Image>(),
            });
        }
        
        SetFill(maxValue);
    }

    public void SetFill(int value)
    {
        currentValue = value;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
        
        SliderElements.ForEach(w => w.Hide());
        for (int i = 0; i < value; i++)
        {
            SliderElements[i].Show();
        }
    }

    public void Add()
    {
        currentValue++;
        SetFill(currentValue);
    }

    public void Remove()
    {
        currentValue--;
        SetFill(currentValue);
    }
}

public class SliderElement
{
    public Image fill;
    private Tween anim;
    
    public void Show()
    {
        anim?.Kill();
        anim = fill.DOFade(1f, .2f);
    }

    public void Hide()
    {
        anim?.Kill();
        anim = fill.DOFade(0f, .2f);
    }
}

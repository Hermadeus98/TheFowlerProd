using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionText : MonoBehaviour
{
    private TextMeshProUGUI text => GetComponent<TextMeshProUGUI>();

    public void UpdateText(string desc)
    {
        text.SetText(desc);
    }
}

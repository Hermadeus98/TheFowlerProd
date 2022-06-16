using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using TheFowler;
using UnityEngine;

public class ColorBlindUpdater : MonoBehaviour, IUpdater
{
    public TextChoice tc;
        
    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        switch (ColorBlindHandler.Instance.currentMode)
        {
            case ColorBlindMode.NORMAL:
                tc.SetTo(0);
                break;
            case ColorBlindMode.PROTANOPIA:
                tc.SetTo(1);
                break;
            case ColorBlindMode.PROTANOMALY:
                tc.SetTo(2);
                break;
            case ColorBlindMode.DEUTERANOPIA:
                tc.SetTo(3);
                break;
            case ColorBlindMode.TRITANOPIA:
                tc.SetTo(4);
                break;
            case ColorBlindMode.TRITANOMALY:
                tc.SetTo(5);
                break;
            case ColorBlindMode.ACHROMATOPSIA:
                tc.SetTo(6);
                break;
            case ColorBlindMode.ACHROMATOMALY:
                tc.SetTo(7);
                break;

        }


        //int index = tc.current;

        //var difficultyTexts = FindObjectsOfType<ColorBlindUpdater>();
        //difficultyTexts.ForEach(w => w.Apply(index));
    }

    public void Apply(int current)
    {
        tc.SetTo(current);
    }
}

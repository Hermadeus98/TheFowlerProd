using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using TheFowler;
using UnityEngine;

public class PreviewUpdater : MonoBehaviour, IUpdater
{
    public TextChoice tc;
        
    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (Player.showPreview)
        {
            tc.SetTo(1);
        }
        else
        {
            tc.SetTo(0);
        }


        //int index = tc.current;

        //var difficultyTexts = FindObjectsOfType<PreviewUpdater>();
        //difficultyTexts.ForEach(w => w.Apply(index));
    }

    private void Update()
    {
        Refresh();
    }

    public void Apply(int current)
    {
        tc.SetTo(current);
    }
}

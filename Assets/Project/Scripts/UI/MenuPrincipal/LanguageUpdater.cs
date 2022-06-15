using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using TheFowler;
using UnityEngine;

public class LanguageUpdater : MonoBehaviour, IUpdater
{
    public TextChoice tc;
        
    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        int index = tc.current;

        var difficultyTexts = FindObjectsOfType<LanguageUpdater>();
        difficultyTexts.ForEach(w => w.Apply(index));
    }

    public void Apply(int current)
    {
        tc.SetTo(current);
    }
}

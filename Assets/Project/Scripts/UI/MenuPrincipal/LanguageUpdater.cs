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

        switch (LocalisationManager.language)
        {
            case Language.ENGLISH:
                tc.SetTo(0);
                break;
            case Language.FRENCH:
                tc.SetTo(1);
                break;

        }

        //int index = tc.current;

        //var difficultyTexts = FindObjectsOfType<LanguageUpdater>();
        //difficultyTexts.ForEach(w => w.Apply(index));
    }

    public void Apply(int current)
    {
        tc.SetTo(current);
    }
}

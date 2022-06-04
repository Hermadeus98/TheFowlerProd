using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class LocalisationManager
{

    public static List<LocalisationElement> elements = new List<LocalisationElement>();
    public static List<Action> actions = new List<Action>();
    public static Language language;

    

    public static void Register(LocalisationElement element)
    {
        if (elements.Contains(element)) return;

        elements.Add(element);

        element.Refresh();
    }


    public static void UnRegister(LocalisationElement element)
    {
        if (!elements.Contains(element)) return;

        elements.Remove(element);
    }


    public static void Register(Action action)
    {
        if (actions.Contains(action)) return;

        actions.Add(action);

        action.Invoke();
    }


    public static void UnRegister(Action action)
    {
        if (!actions.Contains(action)) return;

        actions.Remove(action);
    }

    public static void Notify()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].Refresh();
        }


        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Invoke();
        }
    }

    public static void ChangeLanguage(int value)
    {
        language = (Language)value;

        Notify();
    }

}

public enum Language
{
    ENGLISH = 0,
    FRENCH = 1
}

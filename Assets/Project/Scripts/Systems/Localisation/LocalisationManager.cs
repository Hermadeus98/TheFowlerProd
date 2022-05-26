using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class LocalisationManager
{

    public static List<LocalisationElement> elements = new List<LocalisationElement>();
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

    public static void Notify()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].Refresh();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VFXCycleThrough : MonoBehaviour
{
    public GameObject[] objs;

    int index;

    private void Update()
    {
        if(Keyboard.current.numpadPlusKey.wasPressedThisFrame)
        {
            index++;
            UpdateCurrent();
        }

        if (Keyboard.current.numpadMinusKey.wasPressedThisFrame)
        {
            index--;
            if (index < 0) index = objs.Length - 1;
            UpdateCurrent();
        }
    }

    private void UpdateCurrent()
    {
        foreach (var obj in objs)
        {
            obj.SetActive(false);
        }

        int ind = index % objs.Length;

        objs[ind].SetActive(true);
    }
}

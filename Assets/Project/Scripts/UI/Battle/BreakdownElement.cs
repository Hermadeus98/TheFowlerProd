using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;
using UnityEngine.UI;

public class BreakdownElement : MonoBehaviourSingleton<BreakdownElement>
{
    private void Start()
    {
        
        GetComponent<Image>().enabled = false;
    }

    public void Play()
    {
        GetComponent<Animator>().SetTrigger("Play");
    }
}

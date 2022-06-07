using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TheFowler;
using UnityEngine;

public class LoseGraphics : SerializedMonoBehaviour
{
    public static LoseGraphics Instance;
    public Animator[] Animators;
    public CinemachineVirtualCamera cam;

    private void Awake()
    {
        Instance = this;

        gameObject.SetActive(false);
    }

    [Button]
    public void OpenTest()
    {
        StartCoroutine(Open());
    }

    public IEnumerator Open()
    {
        Animators.ForEach(w => w.SetTrigger("Death"));
        
        BlackPanel.Instance.Show();
        yield return new WaitForSeconds(BlackPanel.Instance.duration);
        gameObject.SetActive(true);
        cam.m_Priority = 1000;
        BlackPanel.Instance.Hide();
    }

    [Button]
    public void CloseTest()
    {
        StartCoroutine(Close());
    }

    public IEnumerator Close()
    {
        Animators.ForEach(w => w.SetTrigger("Resurect"));
            
        BlackPanel.Instance.Show();
        yield return new WaitForSeconds(3f);
        StartCoroutine(SetFalse());
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }
}

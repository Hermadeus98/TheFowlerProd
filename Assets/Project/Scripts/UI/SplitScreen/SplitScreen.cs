using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using QRCode;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class SplitScreen : MonoBehaviourSingleton<SplitScreen>
    {
        public RectTransform splitScreenRect;
        public Image frame;

        private Tween move, fill, fade;

        [SerializeField, ReadOnly] private CinemachineVirtualCameraBase currentSplitCam, bigSplitCam;

        [SerializeField] private CanvasGroup CanvasGroup, textBox;

        [SerializeField] private TextMeshProUGUI text;

        private Camera splitCam;
        
        private void Start()    
        {
            splitCam = GameObject.FindGameObjectWithTag("CameraSplit").GetComponent<Camera>();
            splitCam.gameObject.SetActive(false);
        }

        [Button]
        public void Show(CinemachineVirtualCameraBase splitCam, CinemachineVirtualCameraBase bigCam)
        {
            splitCam.gameObject.SetActive(true);
            
            SetLittleCamera(splitCam);
            SetBigCamera(bigCam);
            
            fade?.Kill();
            fade = CanvasGroup.DOFade(1f, .2f);

            /*splitScreenRect.transform.position = new Vector3(-splitScreenRect.sizeDelta.x, splitScreenRect.position.y, splitScreenRect.position.z);
            move?.Kill();
            move = splitScreenRect.DOAnchorPosX(0, .2f).SetEase(Ease.OutBack);
            frame.fillAmount = 0f;
            fill?.Kill();
            fill = frame.DOFillAmount(1f, .2f).SetEase(Ease.OutBack);*/
        }

        [Button]
        public void SetLittleCamera(CinemachineVirtualCameraBase splitCam)
        {
            if (currentSplitCam != null) currentSplitCam.m_Priority = 0;
            currentSplitCam = splitCam;
            currentSplitCam.m_Priority = 500;
        }

        public void SetBigCamera(CinemachineVirtualCameraBase splitCam)
        {
            if (bigSplitCam != null) bigSplitCam.m_Priority = 0;
            bigSplitCam = splitCam;
            bigSplitCam.m_Priority = 400;
        }

        public void SetPunchLine(string punchline)
        {
            textBox.DOFade(1f, .2f);
            text.SetText(punchline);
        }

        [Button]
        public void Hide()
        {
            fade?.Kill();
            fade = CanvasGroup.DOFade(0f, .2f);
            
            if (bigSplitCam != null) bigSplitCam.m_Priority = 0;

            textBox.DOFade(0f, .2f).OnComplete(
                delegate
                {
                    splitCam.gameObject.SetActive(false);
                });

            /*move?.Kill();
            move = splitScreenRect.DOAnchorPosX(-splitScreenRect.sizeDelta.x, .2f).SetEase(Ease.OutBack);
            fill?.Kill();
            fill = frame.DOFillAmount(0f, .2f).SetEase(Ease.OutBack);*/
        }
    }
}

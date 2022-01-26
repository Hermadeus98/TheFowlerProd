using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace TheFowler
{
    public class FPSCounter : SerializedMonoBehaviour
    {
        [SerializeField] private Color red, white, green;

        [SerializeField] private TextMeshProUGUI text;
        public int AverageFPS { get; private set; }
            
        int[] fpsBuffer;
        int fpsBufferIndex;
        public int frameRange = 60;
        
        public void Update ()
        {
            if (fpsBuffer == null || fpsBuffer.Length != frameRange) {
                InitializeBuffer();
            }
            UpdateBuffer();
            CalculateFPS();

            if (AverageFPS < 15)
            {
                text.color = red;
            }
            else if(AverageFPS >=15 && AverageFPS <= 30)
            {
                text.color = white;
            }
            else
            {
                text.color = green;
            }
            
            text.SetText($"{AverageFPS} FPS");
        }
        
        void UpdateBuffer () {
            fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
            if (fpsBufferIndex >= frameRange) {
                fpsBufferIndex = 0;
            }
        }
        
        void CalculateFPS () {
            int sum = 0;
            for (int i = 0; i < frameRange; i++) {
                sum += fpsBuffer[i];
            }
            AverageFPS = sum / frameRange;
        }
        
        void InitializeBuffer () {
            if (frameRange <= 0) {
                frameRange = 1;
            }
            fpsBuffer = new int[frameRange];
            fpsBufferIndex = 0;
        }
    }
}

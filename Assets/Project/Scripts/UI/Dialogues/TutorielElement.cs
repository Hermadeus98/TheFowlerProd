using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

namespace TheFowler
{
    public class TutorielElement : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        public TutorielElement nextElement;

        public VideoPlayer player;


        public void Initialize()
        {
            player.Play();
        }

        public void End()
        {

            player.Stop();
        }
    }
}


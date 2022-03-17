using System;
using System.Linq;
using QRCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;
using DG.Tweening;
using UnityEngine.Video;

namespace TheFowler
{
    public class VideoView : UIView
    {
        [SerializeField] private VideoPlayer player;
        [SerializeField] private RenderTexture texture;
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);



        }


        public override void Show()
        {
            base.Show();

        }

        public void Show(VideoClip video)
        {
            Show();

            player.clip = video;
            player.Play();
            
        }






        public override void Hide()
        {
            base.Hide();
            player.Stop();
            texture.Release();
            player.clip = null;
            
        }

    }

}

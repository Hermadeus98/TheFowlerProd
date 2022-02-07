using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public class ChapterGymRoom : Chapter
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            Game.LoadSceneAdditive("Scenes GymRoom", () =>
            {
                Coroutiner.Play(OnChapterLoaded(arg));
            });
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            
            Game.UnloadScene("Scenes GymRoom");
        }

        protected override IEnumerator OnChapterLoaded(EventArgs arg)
        {
            yield return base.OnChapterLoaded(arg);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public class ChapterOutro : Chapter
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            Game.LoadSceneAdditive("Scenes Outro", () =>
            {
                Coroutiner.Play(OnChapterLoaded(arg));
            });
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            Game.UnloadScene("Scenes Outro");
        }
    }
}

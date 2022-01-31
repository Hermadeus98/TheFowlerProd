using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public class ChapterTwoPartTwo : Chapter
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            Game.LoadSceneAdditive("Scenes Chapter Two Part 2", () =>
            {
                Coroutiner.Play(OnChapterLoaded(arg));
            });
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            Game.UnloadScene("Scenes Chapter Two Part 2");
        }
    }
}

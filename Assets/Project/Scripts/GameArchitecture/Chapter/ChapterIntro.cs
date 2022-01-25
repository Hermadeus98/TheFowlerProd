using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class ChapterIntro : Chapter
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            GameState.ChangeState(GameStateEnum.CINEMATIC);
        }
    }
}

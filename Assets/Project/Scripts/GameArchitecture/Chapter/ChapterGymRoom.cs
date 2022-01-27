using System;
using System.Collections;
using System.Collections.Generic;
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
                OnChapterLoaded(arg);
            });
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            
            Game.UnloadScene("Scenes GymRoom");
        }
    }
}

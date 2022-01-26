using System;

namespace TheFowler
{
    public class ChapterThree : Chapter
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            Game.LoadSceneAdditive("Scenes Chapter Three", () =>
            {
                Player.Initialize();
            });
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            Game.UnloadScene("Scenes Chapter Three");

        }
    }
}

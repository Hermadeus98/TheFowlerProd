using System;

namespace TheFowler
{
    public class ChapterTwo : Chapter
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            Game.LoadSceneAdditive("Scenes Chapter Two Part 1", null);
            Game.LoadSceneAdditive("Scenes Chapter Two Part 2", () =>
            {
                OnChapterLoaded(arg);
            });
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            Game.UnloadScene("Scenes Chapter Two Part 1");
            Game.UnloadScene("Scenes Chapter Two Part 2");
        }
    }
}

using System;

namespace TheFowler
{
    public class ChapterIntro : Chapter
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            GameState.ChangeState(GameStateEnum.CINEMATIC);

            Game.LoadSceneAdditive("Scenes Intro", () =>
            {
                Player.Initialize();
            });
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            Game.UnloadScene("Scenes Intro");
        }
    }
}

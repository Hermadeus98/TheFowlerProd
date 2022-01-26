using System;
using TheFowler;

public class ChapterOne : Chapter
{
    public override void OnStateEnter(EventArgs arg)
    {
        base.OnStateEnter(arg);
        
        Game.LoadSceneAdditive("Scenes Chapter One", () =>
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
        
        Game.UnloadScene("Scenes Chapter One");
    }
}

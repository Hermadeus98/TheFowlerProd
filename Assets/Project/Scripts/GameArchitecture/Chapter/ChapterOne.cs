using System;
using System.Collections;
using QRCode.Utils;
using TheFowler;

public class ChapterOne : Chapter
{
    public override void OnStateEnter(EventArgs arg)
    {
        base.OnStateEnter(arg);

        
        Game.LoadSceneAdditive("Scenes Chapter One", () =>
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
        
        Game.UnloadScene("Scenes Chapter One");
    }

    protected override IEnumerator OnChapterLoaded(EventArgs arg)
    {

        return base.OnChapterLoaded(arg);

    }
}

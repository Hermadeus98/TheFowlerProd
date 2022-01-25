using System;
using TheFowler;

public class ChapterOne : Chapter
{
    public override void OnStateEnter(EventArgs arg)
    {
        base.OnStateEnter(arg);
        Game.LoadSceneAdditive(
            SceneEnum.Scene_Arene_Tutoriel, 
            SceneEnum.Scene_Harmonisation_Tutoriel,
            SceneEnum.Scene_Couloir_InstrumentBrise,
            SceneEnum.Scene_Arene_Tutoriel_Cinematique);

        Player.Initialize();
    }

    public override void OnStateExecute()
    {
        base.OnStateExecute();
    }

    public override void OnStateExit(EventArgs arg)
    {
        base.OnStateExit(arg);
        Game.UnloadScene(
            SceneEnum.Scene_Arene_Tutoriel, 
            SceneEnum.Scene_Harmonisation_Tutoriel,
            SceneEnum.Scene_Couloir_InstrumentBrise,
            SceneEnum.Scene_Arene_Tutoriel_Cinematique);
    }
}

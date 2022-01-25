using System;

namespace TheFowler
{
    public static class SceneLoader
    {
        public static string GetSceneKey(SceneEnum scene)
        {
            switch (scene)
            {
                case SceneEnum.Scene_Main:
                    return "Scene_Main";
                case SceneEnum.Scene_UI:
                    break;
                case SceneEnum.Scene_Cinematique:
                    break;
                case SceneEnum.Scene_Intro:
                    break;
                case SceneEnum.Scene_Arene_Tutoriel:
                    return "Scene_Arene_Tutoriel";
                case SceneEnum.Scene_Arene_Tutoriel_Cinematique:
                    return "Scene_Arene_Tutoriel_Cinematique";
                case SceneEnum.Scene_Harmonisation_Tutoriel:
                    return "Scene_Harmonisation_Tutoriel";
                case SceneEnum.Scene_Couloir_InstrumentBrise:
                    return "Scene_Couloir_InstrumentBrise";
                case SceneEnum.Scene_Arene_Voliere:
                    break;
                case SceneEnum.Scene_Arene_Voliere_Cinematique:
                    break;
                case SceneEnum.Scene_Couloir_Transition_SalleDeBal:
                    break;
                case SceneEnum.Scene_Arene_SalleDeBal:
                    break;
                case SceneEnum.Scene_Arene_SalleDeBal_Cinematique:
                    break;
                case SceneEnum.Scene_Harmonisation_SalleDeBal:
                    break;
                case SceneEnum.Scene_Harmonisation_Tribunal:
                    break;
                case SceneEnum.Scene_Arene_Tribunal:
                    break;
                case SceneEnum.Scene_Arene_Tribubal_Cinematique_Intro:
                    break;
                case SceneEnum.Scene_Arene_Tribubal_Cinematique_Outro:
                    break;
                case SceneEnum.Scene_Outro:
                    break;
                case SceneEnum.Scene_Credit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scene), scene, null);
            }
            
            return String.Empty;
        }   
    }
}

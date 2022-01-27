using System;

namespace TheFowler
{
    public static class SceneLoader
    {
        public static string GetSceneKey(SceneEnum scene)
        {
            switch (scene)
            {
                //--<SCENES TECH>
                case SceneEnum.Scene_Main:
                    return "Scene_Main";
                case SceneEnum.Scene_UI:
                    return "Scene_UI";
                //--<SCENES INTRO>
                case SceneEnum.Scene_Cinematique_Intro:
                    return "Scene_Cinematique_Intro";
                case SceneEnum.Scene_Intro:
                    return "Scene_Intro";
                //--<SCENES CHAPTER ONE>
                case SceneEnum.Scene_Arene_Tutoriel:
                    return "Scene_Arene_Tutoriel";
                case SceneEnum.Scene_Arene_Tutoriel_Cinematique:
                    return "Scene_Arene_Tutoriel_Cinematique";
                case SceneEnum.Scene_Harmonisation_Tutoriel:
                    return "Scene_Harmonisation_Tutoriel";
                case SceneEnum.Scene_Couloir_InstrumentBrise:
                    return "Scene_Couloir_InstrumentBrise";
                //--<SCENES CHAPTER TWO - PART 1>
                case SceneEnum.Scene_Arene_Voliere:
                    return "Scene_Arene_Voliere";
                case SceneEnum.Scene_Arene_Voliere_Cinematique:
                    return "Scene_Arene_Voliere_Cinematique";
                case SceneEnum.Scene_Couloir_Transition_SalleDeBal:
                    return "Scene_Couloir_Transition_SalleDeBalle";
                //--<SCENES CHAPTER TWO - PART 2>
                case SceneEnum.Scene_Arene_SalleDeBal:
                    return "Scene_Arene_SalleDeBalle";
                case SceneEnum.Scene_Arene_SalleDeBal_Cinematique:
                    return "Scene_Arene_SalleDeBal_Cinematique";
                case SceneEnum.Scene_Harmonisation_SalleDeBal:
                    return "Scene_Harmonisation_SalleDeBal";
                //--<SCENES CHAPTER THREE>
                case SceneEnum.Scene_Harmonisation_Hub_Tribunal:
                    return "Scene_Harmonisation_Hub_Tribunal";
                case SceneEnum.Scene_Arene_Tribunal:
                    return "Scene_Arene_Tribunal";
                case SceneEnum.Scene_Arene_Tribubal_Cinematique_Intro:
                    return "Scene_Arene_Tribunal_Cinematique_Intro";
                case SceneEnum.Scene_Arene_Tribubal_Cinematique_Outro:
                    return "Scene_Arene_Tribunal_Cinematique_Intro";
                //--<OUTRO>
                case SceneEnum.Scene_Outro:
                    return "Scene_Outro";
                case SceneEnum.Scene_Credit:
                    return "Scene_Credit";
                default:
                    throw new ArgumentOutOfRangeException(nameof(scene), scene, null);
            }
            
            return String.Empty;
        }   
    }
}

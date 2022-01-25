using QRCode;
using QRCode.Extensions;
using UnityEngine.SceneManagement;

namespace TheFowler
{
    /// <summary>
    /// Callback Gestion
    /// Scene Management
    /// GameMode & GameState References
    /// </summary>
    public static class Game
    {
        public static void Initialize()
        {
            GameEvent.AddListener(GameEventAddressCore.OnGameStart, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Start Game"));
            GameEvent.AddListener(GameEventAddressCore.OnGamePause, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Pause Game"));
            GameEvent.AddListener(GameEventAddressCore.OnGameUnpause, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Unpause Game"));
            GameEvent.AddListener(GameEventAddressCore.SetDependancies, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Set Dependancies"));
        }

        public static void SetDependancies()
        {
            GameEvent.Broadcast(GameEventAddressCore.SetDependancies);
        }

        public static void LoadSceneAdditive(params SceneEnum[] scenes)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                SceneManager.LoadSceneAsync(SceneLoader.GetSceneKey(scenes[i]), LoadSceneMode.Additive);
            }
        }
        
        public static void UnloadScene(params SceneEnum[] scenes)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                SceneManager.UnloadSceneAsync(SceneLoader.GetSceneKey(scenes[i]));
            }
        }
    }

    public enum SceneEnum
    {
        //Scenes Techniques
        Scene_Main,
        Scene_UI,
        
        //INTRO
        Scene_Cinematique,
        Scene_Intro,
        
        //Chapitre 1 - Rencontre avec Pheobe
        Scene_Arene_Tutoriel,
        Scene_Arene_Tutoriel_Cinematique,
        Scene_Harmonisation_Tutoriel,
        Scene_Couloir_InstrumentBrise,
        
        //Chapitre 2 P1 - La volière
        Scene_Arene_Voliere,
        Scene_Arene_Voliere_Cinematique,
        Scene_Couloir_Transition_SalleDeBal,
        
        //Chapitre 2 P2 - La salle de bal
        Scene_Arene_SalleDeBal,
        Scene_Arene_SalleDeBal_Cinematique,
        Scene_Harmonisation_SalleDeBal,
        
        //Chapitre 3 - Le tribunal
        Scene_Harmonisation_Tribunal,
        Scene_Arene_Tribunal,
        Scene_Arene_Tribubal_Cinematique_Intro,
        Scene_Arene_Tribubal_Cinematique_Outro,
        
        //OUTRO
        Scene_Outro,
        Scene_Credit
    }
}

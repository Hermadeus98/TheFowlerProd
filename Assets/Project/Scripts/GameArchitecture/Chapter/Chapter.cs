using System;
using System.Collections;
using QRCode;
using QRCode.Extensions;
using UnityEngine;

namespace TheFowler
{
    public class Chapter : State
    {
        public ChapterData ChapterData;

        public string ChapterName
        {
            get => ChapterData.ChapterName;
        }
        
        public override void OnStateEnter(EventArgs arg)
        {

            Player.canOpenPauseMenu = true;
            SoundManager.PlaySound(ChapterData.audioEventOnChapterStart, null);
            
            GameState.gameArguments.currentChapter = this;

            if (ChapterData.IsNull())
            {
                QRDebug.LogError("CHAPTER LOADING ERROR ", FrenchPallet.EMERALD, "ChapterData is no loaded.");
                Debug.Break();
            }

            QRDebug.Log("Chapter Enter", FrenchPallet.EMERALD, ChapterName);
        }

        public override void OnStateExecute()
        {

        }

        public override void OnStateExit(EventArgs arg)
        {
            QRDebug.Log("Chapter Exit", FrenchPallet.EMERALD, ChapterName);
            SoundManager.PlaySound(ChapterData.audioEventOnChapterEnded, null);
        }

        protected virtual IEnumerator OnChapterLoaded(EventArgs arg)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            //Player.Initialize();
            yield return new WaitForEndOfFrame();
            ChapterManager.onChapterLoaded?.Invoke(this);
            GameState.ChangeState(GameState.gameArguments.currentChapterData.InitialGameState);
            GameState.gameArguments.currentChapterData.InitialGameInstructions.Call();
            yield return new WaitForEndOfFrame();
            GameplayPhaseManager.PlayGameplayPhase(ChapterData.OnStartGamephase_Id);
        }
    }
}

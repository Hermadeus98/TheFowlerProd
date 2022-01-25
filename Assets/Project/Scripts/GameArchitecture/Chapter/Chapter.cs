using System;

using QRCode;
using QRCode.Extensions;

namespace TheFowler
{
    public class Chapter : State
    {
        public string ChapterName;

        public override void OnStateEnter(EventArgs arg)
        {
            GameState.gameArguments.currentChapter = this;

            QRDebug.Log("Chapter Enter", FrenchPallet.EMERALD, ChapterName);
        }

        public override void OnStateExecute()
        {

        }

        public override void OnStateExit(EventArgs arg)
        {
            QRDebug.Log("Chapter Exit", FrenchPallet.EMERALD, ChapterName);
        }
    }
}

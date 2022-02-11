using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public static class CreateAssetMenuPath
    {
        public const string root = "TheFowler/";

        public const string data = root + "Datas/";
        public const string ui = data + "UI/";
        public const string battle = data + "Battle/";

        public const string Spawnables = data + "Spawnables";
        public const string controller = data + "Controllers/";

        public const string presets = data + "Presets/";
        public const string presets_AI = presets + "AI/";

        public const string chapterData = data + "ChapterData";

        public const string dialogueData = data + "DialogueDatabase";

        public const string actorDB = data + "ActorDatabase";

        public const string camera = data + "/Cameras";
        public const string cameraTransitionData = camera + "TransitionDB";
        
        public const string rappelInputDB = ui + "RappelInputDatabase";
        public const string spellTypeDB = ui + "SpellTypeDatabase";

        public const string battleActorData = battle + "BattleActor";
        public const string spell = data + "Spell";
    }
}

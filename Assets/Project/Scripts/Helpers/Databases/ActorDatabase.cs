using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.actorDB)]
    public class ActorDatabase : Database<ActorEnum, ActorInfosDB>
    {
        
    }

    public class ActorInfosDB
    {
        public string actorName;
        public Sprite portraitBuste;
        public Sprite portrait;
        public Sprite thumbnail;
    }
}

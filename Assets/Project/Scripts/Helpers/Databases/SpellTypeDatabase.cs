using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.spellTypeDB)]
    public class SpellTypeDatabase : Database<Spell.SpellTypeEnum, Sprite>
    {
        
    }
}

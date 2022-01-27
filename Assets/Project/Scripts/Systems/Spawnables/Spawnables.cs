using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.Spawnables)]
    public class Spawnables : ScriptableObjectSingleton<Spawnables>
    {
        [Required] public Robyn Robyn;
        [Required] public Abigael Abigael;
        [Required] public Pheobe Pheobe;
    }
}

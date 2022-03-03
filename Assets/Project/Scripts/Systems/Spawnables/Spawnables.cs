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

        [TitleGroup("UI")] 
        [Required] public DialogueUIElement DialogueUIElement;

        [TitleGroup("UI")] [Required] public PopupText PopupText;
    }
}

using UnityEngine;

namespace QRCode.Editor
{
    [CreateAssetMenu(menuName = "QRCode/Editor/SceneDatabase")]
    public class SceneDatabase : ScriptableObjectSingleton<SceneDatabase>
    {
        public SceneReference[] sceneReferences;
    }
}

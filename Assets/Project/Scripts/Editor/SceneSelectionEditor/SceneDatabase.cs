using System;
using UnityEngine;

namespace QRCode.Editor
{
    [CreateAssetMenu(menuName = "QRCode/Editor/SceneDatabase")]
    public class SceneDatabase : ScriptableObjectSingleton<SceneDatabase>
    {
        public ScenesBatch[] ScenesBatches;
    }

    [Serializable]
    public class ScenesBatch
    {
        public string batchName;
        public SceneReference[] sceneReferences;
    }
}

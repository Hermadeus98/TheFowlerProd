using System;
using System.Linq;
using UnityEngine;

namespace QRCode
{
    [CreateAssetMenu(menuName = "QRCode/Editor/SceneDatabase")]
    public class SceneDatabase : ScriptableObjectSingleton<SceneDatabase>
    {
        public ScenesBatch[] ScenesBatches;

        public ScenesBatch GetBatch(string key)
        {
            return ScenesBatches.First(w => w.batchName == key);
        }
    }

    [Serializable]
    public class ScenesBatch
    {
        public string batchName;
        public SceneReference[] sceneReferences;
    }
}

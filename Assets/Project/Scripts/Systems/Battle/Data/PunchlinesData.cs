using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using AK.Wwise;
using Sirenix.Utilities;
using Random = UnityEngine.Random;

namespace TheFowler
{
    [CreateAssetMenu(menuName = "TheFowler/Datas/Punchlines Data")]
    public class PunchlinesData : SerializedScriptableObject
    {
        public Dictionary<PunchlineCallback, PunchlineData[]> database =
            new Dictionary<PunchlineCallback, PunchlineData[]>();

        public PunchlineData[] Get(PunchlineCallback callback) => database[callback];

        public PunchlineData GetRandom(PunchlineCallback callback)
        {
            if (!database.ContainsKey(callback))
            {
                Debug.LogError($"Key {callback} is missing in the database", this);
                return null;
            }
            
            var pool = Get(callback);

            if (pool.IsNullOrEmpty())
            {
                Debug.LogError($"Key {callback} don't have punchline register", this);
                return null;
            }
            
            if (pool.All(w => w.isPlayed))
            {
                pool.ForEach(w => w.isPlayed = false);
            }

            var selected = pool.Where(w => !w.isPlayed).ToArray();
            
            return selected[Random.Range(0, pool.Length - 1)];
        }
    }
    
    [System.Serializable]
    public class PunchlineData
    {
        public AK.Wwise.Event audio;
        [TextArea(3, 5)] public string text;

        public float soundDuration = 2f;
        [ReadOnly] public bool isPlayed = false;
    }
}


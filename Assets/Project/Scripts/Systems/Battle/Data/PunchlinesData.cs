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
        [SerializeField] private Dictionary<PunchlineCallback, PunchlineData[]> database =
            new Dictionary<PunchlineCallback, PunchlineData[]>();

        public List<PunchlineData> Get(PunchlineCallback callback) => Structures.First(w => w.Callback == callback).PunchlineDatas;

        public List<PunchlineDataStructure> Structures;

        [Button]
        public void Generate()
        {
            Structures = new List<PunchlineDataStructure>();
            
            for (int i = 0; i < database.Count; i++)
            {
                Structures.Add(new PunchlineDataStructure()
                {
                    Callback = database.ElementAt(i).Key,
                    PunchlineDatas = new List<PunchlineData>()
                });

                var s =Structures.Find(w => w.Callback == database.ElementAt(i).Key);
                
                for (int j = 0; j < database.ElementAt(i).Value.Length; j++)
                {
                    s.PunchlineDatas.Add(database.ElementAt(i).Value[j]);
                }
            }
        }
        
        public PunchlineData GetRandom(PunchlineCallback callback)
        {
            if (Structures == null)
                return null;
            
            if (!Structures.Exists(w => w.Callback == callback))
            {
                Debug.LogError($"Key {callback} is missing in the database in {name}", this);
                return null;
            }
            
            var pool = Get(callback);
            
            var s = Structures.First(w => w.Callback == callback);
            var chance = Random.Range(0, 100);
            if (chance > s.chance)
                return null;

            if (pool.IsNullOrEmpty())
            {
                Debug.LogError($"Key {callback} don't have punchline register in {name}", this);
                return null;
            }
            
            if (pool.All(w => w.isPlayed))
            {
                pool.ForEach(w => w.isPlayed = false);
            }

            
            var selected = pool.Where(w => !w.isPlayed).ToArray();
            return selected[Random.Range(0, pool.Count - 1)];
        }
    }

    [System.Serializable]
    public class PunchlineDataStructure
    {
        [Button] public int chance = 100;
        public PunchlineCallback Callback;
        public List<PunchlineData> PunchlineDatas;
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


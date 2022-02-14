using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DataInitializer<T> where T : struct
    {
        public T datas;
        
        public DataInitializer(string json)
        {
            datas = JsonUtility.FromJson<T>(json);
        }
    }
}

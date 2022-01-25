using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode
{
    public class Database<T,U> : SerializedScriptableObject, IDatabase
    {
        [SerializeField] private Dictionary<T, U> database = new Dictionary<T, U>();

        public U GetElement(T key)
        {
            return database[key];
        }
    }

    public interface IDatabase
    {
        
    }
}

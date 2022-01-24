using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QRCode
{
    /// <summary>
    /// Each adress have some key to open, when all keys are open, a callback is call.
    /// </summary>
    public static class SetupEvent
    {
        //---<DATA>----------------------------------------------------------------------------------------------------<
        private static Dictionary<string, Dictionary<string, bool>> addressTable =
            new Dictionary<string, Dictionary<string, bool>>();

        private static Dictionary<string, Action> eventTable = new Dictionary<string, Action>();

        private static EventTranslator Translator;
        
        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            Translator = new EventTranslator();
            
            var attributes = typeof(SetupEventKey).GetCustomAttributes(true);
            foreach (var attribute in attributes)
            {
                if (attribute is AddressAttribute address)
                {
                    Translator.Add(address.TypeReference, address.Pin);
                }
            }
            
            var a = typeof(SetupEventAddress).GetCustomAttributes(true);
            foreach (var attribute in a)
            {
                if (attribute is AddressAttribute address)
                {
                    Translator.Add(address.TypeReference, address.Pin);
                }
            }
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        
        /// <summary>
        /// Add a key at a specified adress.
        /// </summary>
        public static void AddKey<A,K>(A address, K key, Action onComplete = null) 
            where A : Enum 
            where K: Enum
        {
            AddKey(address.ToString(), key.ToString(), onComplete);
        }
        
        /// <summary>
        /// Add a key at a specified adress.
        /// </summary>
        public static void AddKey(string address, string key, Action onComplete = null)
        {
            if (!addressTable.ContainsKey(address))
            {
                addressTable.Add(address, new Dictionary<string, bool>()
                    {{key, false}}
                );
            }
            
            if (!addressTable[address].ContainsKey(key))
            {
                addressTable[address].Add(key, false);
            }

            if (!eventTable.ContainsKey(address))
            {
                eventTable.Add(address, null);
                if(onComplete != null)
                    eventTable[address] += onComplete;
            }
            else
            {
                if(onComplete != null)
                    eventTable[address] += onComplete;
            }
        }

        public static void RemoveKey(Enum address, Enum key)
        {
            RemoveKey(address.ToString(), key.ToString());
        }
        
        public static void RemoveKey(string address, string key)
        {
            if (address.Contains(address))
            {
                var dic = addressTable[address];
                if (dic.ContainsKey(key))
                {
                    addressTable[address].Remove(key);
                    if (addressTable[address].Count == 0)
                        eventTable.Remove(address);
                }
            }
        }
        
        /// <summary>
        /// Add a callback at a specified adress.
        /// </summary>
        public static void CallBackOnComplete<A>(A address, Action onComplete) where A : Enum
        {
            CallBackOnComplete(address.ToString(), onComplete);
        }
        
        /// <summary>
        /// Add a callback at a specified adress.
        /// </summary>
        public static void CallBackOnComplete(string address, Action onComplete)
        {
            if(!addressTable.ContainsKey(address))
                throw new Exception($"The is no door with this name : {address}.");

            eventTable[address] += onComplete;
        }

        public static void RemoveCallBackOnComplete(Enum address, Action onComplete)
        {
            RemoveCallBackOnComplete(address.ToString(), onComplete);
        }
        
        /// <summary>
        /// Remove a callback at the target address.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="onComplete"></param>
        public static void RemoveCallBackOnComplete(string address, Action onComplete)
        {
            if (eventTable.ContainsKey(address))
            {
                eventTable[address] -= onComplete;
            }
        }

        /// <summary>
        /// Check if all key at the specified adress.
        /// </summary>
        public static bool TryUnlock<A>(A address, bool remove = false) where A : Enum
        {
            return TryUnlock(address.ToString(), remove);
        }
        
        /// <summary>
        /// Check if all key at the specified adress.
        /// </summary>
        public static bool TryUnlock(string address, bool remove = false)
        {
            if (!eventTable.ContainsKey(address))
                throw new Exception($"The is no door with this name : {address}.");
            
            var subKeys = addressTable[address];
            if (!subKeys.Values.All(w => w)) return false;
            
            eventTable[address].Invoke();
            if(remove) eventTable.Remove(address);
            return true;

        }

        /// <summary>
        /// Unlock all keys at the specified adress.
        /// </summary>
        public static void UnlockAll<A>(A address, bool invoke = true, bool remove = false) where A : Enum
        {
            UnlockAll(address, invoke, remove);
        }
        
        /// <summary>
        /// Unlock all keys at the specified adress.
        /// </summary>
        public static void UnlockAll(string address, bool invoke = true, bool remove = false)
        {
            var table = addressTable[address];

            for (int i = 0; i < table.Keys.Count; i++)
            {
                addressTable[address][table.Keys.ElementAt(i)] = true;
            }
            
            if(invoke)
                TryUnlock(address, remove);
        }

        /// <summary>
        /// Unlock a key at a specified adress and check if the callback can be invoke.
        /// </summary>
        public static void UnlockKey<A, K>(A address, K key, bool remove = false)
            where A : Enum
            where K : Enum
        {
            UnlockKey(address.ToString(), key.ToString(), remove);
        }

        /// <summary>
        /// Unlock a key at a specified adress and check if the callback can be invoke.
        /// </summary>
        public static void UnlockKey(string address, string key, bool remove = false)
        {
            addressTable[address][key] = true;
            TryUnlock(address, remove);
        }

        /// <summary>
        /// Lock a key at a specified adress.
        /// </summary>
        public static void LockKey<A, K>(A address, K key)
            where A : Enum
            where K : Enum
        {
            LockKey(address.ToString(), key.ToString());
        }
        
        /// <summary>
        /// Lock a key at a specified adress.
        /// </summary>
        public static void LockKey(string address, string key)
        {
            addressTable[address][key] = false;
        }
    }
}

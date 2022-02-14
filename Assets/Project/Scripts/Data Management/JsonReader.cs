using System.Collections;
using System.Collections.Generic;
using Unity.RemoteConfig;
using UnityEngine;

namespace TheFowler
{
    public static class JsonReader
    {
        public static void Initialize()
        {
            RemoteSettingsManager.AddFetchCompletedCallback(UpdateDatas);
        }

        public static void UpdateDatas(ConfigResponse response)
        {
            
        }
    }
}

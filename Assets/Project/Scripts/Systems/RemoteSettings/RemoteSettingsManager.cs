using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using Unity.RemoteConfig;
using UnityEngine;

namespace TheFowler
{
    public class RemoteSettingsManager : MonoBehaviourSingleton<RemoteSettingsManager>
    {
        [SerializeField, EnumPaging] private RemoteEnvironment remoteEnvironment;
        public static RemoteEnvironment RemoteEnvironment => Instance.remoteEnvironment;
        
        public struct userAttributes { }

        public struct appAttributes { }

        public static void SetRemoteEnvironment()
        {
            ConfigManager.SetEnvironmentID(GetEnvironment(RemoteEnvironment));
        }

        [Button]
        public static void Fetch()
        {
            SetRemoteEnvironment();
            ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        }

        public static void AddFetchCompletedCallback(Action<ConfigResponse> action)
        {
            ConfigManager.FetchCompleted += action;
        }

        private static string GetEnvironment(RemoteEnvironment remoteEnvironment)
        {
            switch (remoteEnvironment)
            {
                case RemoteEnvironment.PRODUCTION:
                    return "61112c49-e51d-4d98-abf8-1789fffe57d5";
                case RemoteEnvironment.RELEASE:
                    return "a18a2725-613e-477c-8bf7-f0906278ace4";
                default:
                    throw new ArgumentOutOfRangeException(nameof(remoteEnvironment), remoteEnvironment, null);
            }
        }
    }

    public enum RemoteEnvironment
    {
        PRODUCTION,
        RELEASE,
    }
}

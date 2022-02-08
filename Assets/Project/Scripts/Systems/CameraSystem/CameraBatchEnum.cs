using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public enum CameraBatchEnum
    {
        DEFAULT,
        PLAYER_CONTROLLER,
        BATTLE,
        CINEMATIC,
        CUT_SCENE,
        DIALOGUE_MOUVEMENT,
    }

    public enum CameraGenericKeyEnum
    {
        NULL,
        FACIAL_ROBYN,
        FACIAL_PHOEBE,
        FACIAL_ABI,
        TPS_CAMERA,
    }

    public static class CameraGenericKey
    {
        public static string GetCameraGenericKey(CameraGenericKeyEnum key)
        {
            switch (key)
            {
                case CameraGenericKeyEnum.NULL:
                    break;
                case CameraGenericKeyEnum.FACIAL_ROBYN:
                    return "Robyn_Facial";
                case CameraGenericKeyEnum.FACIAL_PHOEBE:
                    return "Phoebe_Facial";
                case CameraGenericKeyEnum.FACIAL_ABI:
                    return "Abigael_Facial";
                case CameraGenericKeyEnum.TPS_CAMERA:
                    return "Robyn_TPS";
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            
            return String.Empty;
        }
    }
}

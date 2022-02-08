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
        
        //Dialogue
        FACIAL_ROBYN,
        FACIAL_PHOEBE,
        FACIAL_ABI,
        
        //Explo
        TPS_CAMERA,
        
        //Combats
    }

    public static class CameraGenericKey
    {
        public static string GetCameraGenericKey(CameraGenericKeyEnum key)
        {
            switch (key)
            {
                case CameraGenericKeyEnum.NULL:
                    break;
                
                //Dialogues
                case CameraGenericKeyEnum.FACIAL_ROBYN:
                    return "Robyn_Facial";
                case CameraGenericKeyEnum.FACIAL_PHOEBE:
                    return "Phoebe_Facial";
                case CameraGenericKeyEnum.FACIAL_ABI:
                    return "Abigael_Facial";
                
                //Explo
                case CameraGenericKeyEnum.TPS_CAMERA:
                    return "Robyn_TPS";
                
                //Combats
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            
            return String.Empty;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public enum CameraGenericKeyEnum
    {
        NULL,
        
        //Generic Dialogues
        FACIAL_ROBYN,
        FACIAL_PHOEBE,
        FACIAL_ABIGAIL,
        CONTEXTUAL_ROBYN,
        CONTEXTUAL_PHOEBE,
        CONTEXTUAL_ABIGAIL,
        
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
                case CameraGenericKeyEnum.FACIAL_ABIGAIL:
                    return "Abigael_Facial";
                case CameraGenericKeyEnum.CONTEXTUAL_ROBYN:
                    return "Robyn_Contextual";
                case CameraGenericKeyEnum.CONTEXTUAL_PHOEBE:
                    return "Phoebe_Contextual";
                case CameraGenericKeyEnum.CONTEXTUAL_ABIGAIL:
                    return "Abigail_Contextual";
                
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

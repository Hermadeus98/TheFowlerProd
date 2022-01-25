using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.presets + "Controller Data Preset")]
    public class ControllerPresets : Database<ControllerMovement, ControllerData>
    {
        
    }
}
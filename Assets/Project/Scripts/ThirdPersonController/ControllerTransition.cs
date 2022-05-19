using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class ControllerTransition : SerializedMonoBehaviour
    {
        [MinMaxSlider(0f, 1f)] public Vector2 speedModifier = new Vector2(.2f, 1f);

        public float minRadius = 2f, maxRadius = 5f;
        public AnimationCurve normalizedCurve;
        public CinemachineMixingCamera camOverride;
        
        private float normalizedValue;
        private float dist;

        private bool haveReplace = false;

        private void Update()
        {
            if(Player.Robyn == null)
                return;
            
            if(Player.Robyn.Controller.GetController(ControllerEnum.PLAYER_CONTROLLER) == null)
                return;
            
            var tps = Player.Robyn.Controller.SetController<ThirdPersonController>(ControllerEnum.PLAYER_CONTROLLER);

            dist = Vector3.Distance(Player.Robyn.pawnTransform.position, transform.position);

            if (dist < maxRadius)
            {
                normalizedValue = normalizedCurve.Evaluate((dist - minRadius) / (maxRadius - minRadius));
                normalizedValue = Mathf.Clamp(normalizedValue, speedModifier.x, speedModifier.y);

                if (camOverride != null & !haveReplace)
                {
                    haveReplace = true;
                    camOverride.ChildCameras[0].transform.position = Player.Robyn.tpsCam.transform.position;
                    camOverride.ChildCameras[0].transform.rotation = Player.Robyn.tpsCam.transform.rotation;
                }
            }
            else
            {
                normalizedValue = 1f;
                haveReplace = false;
            }

            tps.SpeedModifier = normalizedValue;
        }

        private void LateUpdate()
        {
            if(Player.Robyn == null)
                return;
            
            if (dist < maxRadius)
            {
                if (camOverride != null)
                {
                    camOverride.m_Priority = 500;

                    camOverride.m_Weight0 = normalizedValue;
                    camOverride.m_Weight1 = 1 - normalizedValue;
                }
            }
            else
            {
                if (camOverride != null)
                {
                    camOverride.m_Priority = 0;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minRadius);
            Gizmos.DrawWireSphere(transform.position, maxRadius);
        }
    }
}

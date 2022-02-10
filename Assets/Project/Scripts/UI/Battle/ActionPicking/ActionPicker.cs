using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TheFowler
{
    public class ActionPicker : UIElement
    {
        [SerializeField] private Transform front, back;
        
        [Button]
        public void PlugToActor(BattleActor actor)
        {
            front.position = actor.sockets.body_Middle.position;
            back.position = actor.sockets.body_Middle.position;

#if UNITY_EDITOR
            /*var rotation = SceneView.currentDrawingSceneView.camera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward,
                rotation * Vector3.up
            );*/
#endif
        }
    }
}

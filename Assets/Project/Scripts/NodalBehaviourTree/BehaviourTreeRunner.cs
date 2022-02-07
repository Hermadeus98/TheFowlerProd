using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        private BehaviourTree tree;

        private void Start()
        {
            tree = ScriptableObject.CreateInstance<BehaviourTree>();
            
            /*var log1 = ScriptableObject.CreateInstance<DebugLogNode>();
            log1.message = "log1";
            
            var pause = ScriptableObject.CreateInstance<WaitNode>();
            
            
            var log2 = ScriptableObject.CreateInstance<DebugLogNode>();
            log2.message = "log2";
            
            var log3 = ScriptableObject.CreateInstance<DebugLogNode>();
            log3.message = "log3";*/

            /*var sequencer = ScriptableObject.CreateInstance<SequencerNode>();
            sequencer.children.Add(log1);
            sequencer.children.Add(pause);
            sequencer.children.Add(log2);
            sequencer.children.Add(pause);
            sequencer.children.Add(log3);*/
            
            var loop = ScriptableObject.CreateInstance<RepeatNode>();
            //loop.child = sequencer;

            tree.rootNode = loop;
        }

        private void Update()
        {
            tree.Update();
        }
    }
}

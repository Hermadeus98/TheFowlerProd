using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu]
    public class BehaviourTree : ScriptableObject
    {
        [TitleGroup("Main Settings"), Required]
        public Node rootNode;
        [TitleGroup("Main Settings"), Required]
        public ActorEnum[] actors;
        [TitleGroup("Infos")]
        public Node.State treeState = Node.State.Running;
        public List<Node> nodes = new List<Node>();

        public DialogueType dialogueType;

        public Node.State Update()
        {
            if (rootNode.state == Node.State.Running)
            {
                return rootNode.Update();
            }

            return treeState;
        }

        public Node CreateNode(Type type)
        {
            var node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
#if UNITY_EDITOR
            node.guid = GUID.Generate().ToString();
            #endif
            nodes.Add(node);
            
#if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
#endif
            
            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            
#if UNITY_EDITOR
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
#endif
        }

        public void AddChild(Node parent, Node child)
        {
            DecoratorNode decoratorNode = parent as DecoratorNode;
            if (decoratorNode)
            {
                decoratorNode.child = child;
            }
            
            CompositeNode compositeNode = parent as CompositeNode;
            if (compositeNode)
            {
                compositeNode.children.Add(child);
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            DecoratorNode decoratorNode = parent as DecoratorNode;
            if (decoratorNode)
            {
                decoratorNode.child = null;
            }
            
            CompositeNode compositeNode = parent as CompositeNode;
            if (compositeNode)
            {
                compositeNode.children.Remove(child);
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();
            DecoratorNode decoratorNode = parent as DecoratorNode;
            if (decoratorNode && decoratorNode.child != null)
            {
                children.Add(decoratorNode.child);
            }
            
            CompositeNode compositeNode = parent as CompositeNode;
            if (compositeNode)
            {
                return compositeNode.children;
            }

            return children;
        }

        public void SearchRootNode()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].name == "Main")
                {
                    rootNode = nodes[i];
                    return;
                }
            }
        }
    }
}

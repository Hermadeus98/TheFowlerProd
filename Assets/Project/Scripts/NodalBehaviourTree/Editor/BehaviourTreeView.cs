using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode.Extensions;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace TheFowler
{
    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }

        public Action<NodeView> OnNodeSelected;
        private BehaviourTree tree;
        
        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Project/Scripts/NodalBehaviourTree/Editor/BehaviourTreeEditorWindow.uss");
            styleSheets.Add(styleSheet);
        }

        NodeView FindNodeView(Node node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }
        
        public void PopulateView(BehaviourTree tree)
        {
            this.tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            graphElements.ForEach(w => RemoveElement(w));
            graphViewChanged += OnGraphViewChanged;

            //Creates node view
            tree.nodes.ForEach(n => CreateNodeView(n));
            
            //Create edges
            tree.nodes.ForEach(n =>
            {
                var children = tree.GetChildren(n);
                children.ForEach(c =>
                {
                    NodeView parentView = FindNodeView(n);
                    NodeView childView = FindNodeView(c);

                    var edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                });
            });

            SetRootNode();
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()
                .Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(elem =>
                {
                    NodeView nodeView = elem as NodeView;
                    if (nodeView != null)
                    {
                        tree.DeleteNode(nodeView.node);
                    }
                    
                    Edge edge = elem as Edge;
                    if (edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        tree.RemoveChild(parentView.node, childView.node); 
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.AddChild(parentView.node, childView.node);
                });
            }
            
            SetRootNode();

            return graphViewChange;
        }

        private ContextualMenuPopulateEvent evt;
        
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            this.evt = evt;
            
            //base.BuildContextualMenu(evt);
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }
            
            {
                var types = TypeCache.GetTypesDerivedFrom<DialogueNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }
        }

        void CreateNode(Type type)
        {
            var node = tree.CreateNode(type);
            CreateNodeView(node);
        }

        void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.OnNodeSelected += OnNodeSelected;
            AddElement(nodeView);
            
            PersonalizeStyle(nodeView);
        }

        void PersonalizeStyle(NodeView nv)
        {
            if (nv.node is ChoiceNode choiceNode)
            {
                var color = new Color().ToColor("#284B63");
                nv.titleContainer.Q("title").style.backgroundColor = color;
            }
            
            if (nv.node is TargetNode targetNode)
            {
                var color = new Color().ToColor("#BA274A");
                nv.titleContainer.Q("title").style.backgroundColor = color;
            }
            
            if (nv.node is CastNode castNode)
            {
                var color = new Color().ToColor("#DC493A");
                nv.titleContainer.Q("title").style.backgroundColor = color;
            }
            
            if (nv.node is AleatoryChoiceNode ran)
            {
                var color = new Color().ToColor("#284B63");
                nv.titleContainer.Q("title").style.backgroundColor = color;
            }
            
            if (nv.node is DialogueNode dialogueNode)
            {
                
                switch (dialogueNode.dialogue.ActorEnum)
                {
                    case ActorEnum.ROBYN:
                        var color1 = new Color().ToColor("#DC493A");
                        nv.titleContainer.Q("title").style.backgroundColor = color1;
                        break;
                    case ActorEnum.ABIGAEL:
                        var color2 = new Color().ToColor("#9F8219");
                        nv.titleContainer.Q("title").style.backgroundColor = color2;
                        break;
                    case ActorEnum.PHEOBE:
                        var color3 = new Color().ToColor("#284B63");
                        nv.titleContainer.Q("title").style.backgroundColor = color3;
                        break;
                    case ActorEnum.GUARD:
                        var color4 = new Color(0.19f, 0.19f, 0.19f);
                        nv.titleContainer.Q("title").style.backgroundColor = color4;
                        break;
                    case ActorEnum.LIEUTENANT:
                        var color5 = new Color(0.19f, 0.19f, 0.19f);
                        nv.titleContainer.Q("title").style.backgroundColor = color5;
                        break;
                    case ActorEnum.EMPTY:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
        }

        void SetRootNode()
        {
            if (tree.rootNode.IsNull() && !tree.nodes.IsNullOrEmpty())
            {
                for (int i = 0; i < tree.nodes.Count; i++)
                {
                    if (tree.nodes[i].name == "Main")
                    {
                        tree.rootNode = tree.nodes[i];
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace TheFowler
{
    public class InspectorView : VisualElement
    {
        private UnityEditor.Editor editor;
        
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits>
        {
        }
        
        public InspectorView()
        {
            
        }

        public void UpdateSelection(NodeView nodeView)
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(nodeView.node);
            IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
            Add(container);
        }
    }
}

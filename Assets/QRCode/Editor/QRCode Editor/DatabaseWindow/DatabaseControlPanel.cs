/*
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using Sirenix.Utilities.Editor;

namespace QRCode.Editor
{
    public class DatabaseControlPanel : OdinMenuEditorWindow
    {
        [MenuItem("QRCode/Database Control Panel")]
        private static void OpenWindow()
        {
            GetWindow<DatabaseControlPanel>().Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true, OdinMenuStyle.TreeViewStyle)
            {
                DefaultMenuStyle = {IconSize = 25f},
                Config = {DrawSearchToolbar = true, DrawScrollView = true}
            };

            tree.AddAllAssetsAtPath("String - Sprite DB", "Assets/QRCode V2/Resources/Databases/String Sprite DB", typeof(StringSpriteDatabase), true).ForEach(AddDragHandles);
            tree.AddAllAssetsAtPath("String - Color DB", "Assets/QRCode V2/Resources/Databases/String Color DB", typeof(StringColorDatabase), true).ForEach(AddDragHandles);
            
            return tree;
        }
        
        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(
                menuItem.Rect, 
                menuItem.Value, 
                false, 
                false);
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolBarHeight = MenuTree.Config.SearchToolbarHeight;
            SirenixEditorGUI.BeginHorizontalToolbar(toolBarHeight);
            {
                if (selected != null)
                    GUILayout.Label(selected.Name);
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}
*/

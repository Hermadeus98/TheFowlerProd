using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public static class UI
    {
        public class Views
        {
            public const string
                StaticDialogs = "StaticDialogueView",
                LoaderFader = "LoaderFaderView",
                ChapterTitle = "ChapterLoaderView",
                MovementDialogs = "MovementDialogueView",
                SkillPicking = "SkillPickingView",
                TargetPicking = "TargetPickingView",
                ActionPicking = "ActionPickingView",
                TurnTransition = "TurnTransitionView",
                Harmo = "HarmonisationView",
                InfoBox = "InfoBoxView",
                TransitionView = "TransitionView",
                CutsceneView = "TwoDCutsceneView",
                AlliesDataView = "AlliesDataView";
        }
        
        private static Dictionary<string, UIView> views = new Dictionary<string, UIView>();

        public static void RegisterView(UIView view)
        {
            if(!views.ContainsKey(view.ViewName))
            {
                views.Add(view.ViewName, view);
            }
        }
        
        public static void UnregisterView(UIView view)
        {
            if(views.ContainsKey(view.ViewName))
            {
                views.Remove(view.ViewName);
            }
        }

        public static void OpenView(string key)
        {
            views[key]?.Show();
        }
        
        public static T OpenView<T>(string key) where T : UIView
        {
            views[key]?.Show();
            return views[key] as T;
        }
        
        public static void CloseView(string key)
        {
            views[key]?.Hide();
        }

        public static void RefreshView(string key, EventArgs args)
        {
            views[key]?.Refresh(args);
        }

        public static T GetView<T>(string key) where T : UIView
        {
            return views[key] as T;
        }
    }
}

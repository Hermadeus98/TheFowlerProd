using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public static class UI
    {
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
        
        public static void CloseView(string key)
        {
            views[key]?.Hide();
        }

        public static void RefreshView(string key, EventArgs args)
        {
            views[key]?.Refresh(args);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class TutorielHandler : MonoBehaviour
    {
        public bool isTutoriel;
        public bool basicAttack;
        private void Awake()
        {
            Tutoriel.isTutoriel = isTutoriel;
            Tutoriel.BasicAttack = basicAttack;
        }


        public void SetIsTutoriel(bool value)
        {
            Tutoriel.isTutoriel = value;
            
        }

        public void SetBasicAttack(bool value)
        {
            Tutoriel.BasicAttack = value;

        }

        public void ShowPanel(string panel)
        {
            var view = UI.GetView<TutorielView>(UI.Views.Tuto);
            view.Show((PanelTutoriel)System.Enum.Parse(typeof(PanelTutoriel), panel));
        }


    }
    [System.Serializable]
    public enum PanelTutoriel
    {
        BASICATTACK,
    }

}


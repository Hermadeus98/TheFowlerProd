using System;
using System.Linq;
using QRCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;


namespace TheFowler
{
    public class HarmonisationView : UIView
    {
        [TabGroup("References")]
        [SerializeField] private GameObject choice, abigail, phoebe, abigailSolo;


        [ReadOnly]
        public bool isChosing, onAbi, onPhoebe;
        [ReadOnly]
        public bool isAbigailSolo;
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
        }

        public override void Show()
        {
            base.Show();
            SetupShow();
        }

        public void SetupShow()
        {
            if (!isAbigailSolo)
            {
                ShowChoice();
            }
            else
            {
                ShowAbiSolo();
            }
        }

        public override void Hide()
        {
            base.Hide();

            choice.SetActive(false);
            abigail.SetActive(false);
            phoebe.SetActive(false);
            abigailSolo.SetActive(false);
            isChosing = false;
            onAbi = false;
            onPhoebe = false;
        }

        public void ShowAbi()
        {
            choice.SetActive(false);
            abigail.SetActive(true);
            phoebe.SetActive(false);
            isChosing = false;
            onAbi = true;
            onPhoebe = false;
        }

        public void ShowAbiSolo()
        {
            choice.SetActive(false);
            abigail.SetActive(false);
            abigailSolo.SetActive(true);
            phoebe.SetActive(false);
            isChosing = false;
            onAbi = true;
            onPhoebe = false;
        }

        public void ShowPhoebe()
        {
            choice.SetActive(false);
            abigail.SetActive(false);
            phoebe.SetActive(true);
            isChosing = false;
            onAbi = false;
            onPhoebe = true;

        }

        public void ShowChoice()
        {
            choice.SetActive(true);
            abigail.SetActive(false);
            phoebe.SetActive(false);
            isChosing = true;
            onAbi = false;
            onPhoebe = false;
        }



    }

}

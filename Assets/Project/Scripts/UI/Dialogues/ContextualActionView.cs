using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace TheFowler
{
    public class ContextualActionView : UIView
    {
        [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;
        private string inputName;
        private float value;
        private  System.Action action;
        private ContextualActionType actionType;
        private int numberOfRepetition;

        [TabGroup("INTRO")]
        [SerializeField] private GameObject Intro;
        [TabGroup("INTRO")]
        [SerializeField] private Image introInput;
        Tween tween;
        [TabGroup("INTRO")]
        [SerializeField] private AK.Wwise.Event exploEvent; 
        [TabGroup("INTRO")]
        [SerializeField] private AK.Wwise.Event titleCardAmbiant;
        [TabGroup("INTRO")]
        [SerializeField] private AK.Wwise.Event titleCardAmbiantStop;
        [TabGroup("INTRO")]
        [SerializeField] private AK.Wwise.Event xPressed;

        public override void Show()
        {
            base.Show();
            titleCardAmbiant.Post(gameObject);
        }

        public override void Hide()
        {
            base.Hide();
            numberOfRepetition = 0;
            exploEvent.Post(gameObject);
            titleCardAmbiantStop.Post(gameObject);
        }

        public void Show(ContextualActionLocation type, float _value, System.Action _action)
        {
            Show();

            Intro.SetActive(false);

            switch (type)
            {
                case ContextualActionLocation.INTRO:

                    actionType = ContextualActionType.REPEATEDPRESS;
                    inputName = "X";
                    value = _value;
                    action = _action;
                    Intro.SetActive(true);
                    tween = introInput.rectTransform.DOShakeScale(5, .1f, 10, 0, false).OnComplete(() => tween.Restart());
                    break;
            }
        }

        private void Update()
        {
            if (isActive)
            {
                switch (actionType)
                {
                    case ContextualActionType.REPEATEDPRESS:
                        if (Inputs.actions[inputName].WasPressedThisFrame())
                        {
                            tween.Kill();
                            introInput.rectTransform.localScale += new Vector3(.5f, .5f, .5f);
                            tween = introInput.rectTransform.DOShakeScale(5, .1f, 10, 0, false).OnComplete(() => tween.Restart());
                            numberOfRepetition++;

                            xPressed.Post(gameObject);

                            if (value == numberOfRepetition)
                            {
                                action.Invoke();
                                numberOfRepetition = 0;
                                tween.Kill();
                                tween = null;
                                introInput.rectTransform.localScale = Vector3.one;
                            }
                        }
                        break;
                }

            }
        }
    }

    public enum ContextualActionLocation
    {
        INTRO
    }

    public enum ContextualActionType
    {
        REPEATEDPRESS,
        MAINTAIN
    }
}

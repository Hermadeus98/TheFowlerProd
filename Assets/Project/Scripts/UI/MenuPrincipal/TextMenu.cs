using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TheFowler
{
    public class TextMenu : UISelectorElement
    {
        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;

        [SerializeField] private UnityEvent OnSelect, OnHighLigh;

        private Sequence pulse;

        public SliderCustom Slider;
        public Slider SliderSimple;
        public TextMeshProUGUI textSlider;
        public TextChoice TextChoice;
        
        private bool isSelected = false;

        [TextArea(2, 4)] public string descriptionText, descriptionTextFrench;

        public TextNavigation TextNavigation;
        
        protected override void OnStart()
        {
            base.OnStart();

            transform.parent.TryGetComponent<TextNavigation>(out TextNavigation);

            if (Slider != null)
            {
                Slider.gameObject.SetActive(true);
                Slider.Generate();
            }
            
            if(SliderSimple != null)
            {
                SliderSimple.gameObject.SetActive(true);
                textSlider.gameObject.SetActive(true);
                
                SliderSimple.onValueChanged.AddListener(delegate(float value)
                {
                    textSlider.SetText(value + "/" + SliderSimple.maxValue);
                });
            }

            if (TextChoice != null)
            {
                TextChoice.gameObject.SetActive(true);
                TextChoice.Initialize();
            }
        }

        public override void Select()
        {
            isSelected = true;
            
            text.color = selectedColor;

            pulse?.Kill();
            pulse = DOTween.Sequence();
            pulse.SetUpdate(true);
            pulse.Append(
                text.rectTransform.DOScale(1.1f, .6f).SetUpdate(true)
                );
            pulse.Append(
                text.rectTransform.DOScale(1f, .6f).SetUpdate(true)
                );
            pulse.SetLoops(-1);
            pulse.Play();

            OnHighLigh.AddListener(() => ChangeLanguage());

            OnHighLigh?.Invoke();
        }

        private void ChangeLanguage()
        {
            if (TextChoice != null)
            {

                if (LocalisationManager.language == Language.ENGLISH)
                {
                    FindObjectsOfType<DescriptionText>().ForEach(w => w.UpdateText(TextChoice.CurrentOption.description));
                }
                else
                {
                    FindObjectsOfType<DescriptionText>().ForEach(w => w.UpdateText(TextChoice.CurrentOption.descriptionFrench));
                }
            }
            else
            {
                if (LocalisationManager.language == Language.ENGLISH)
                {
                    FindObjectsOfType<DescriptionText>().ForEach(w => w.UpdateText(descriptionText));
                }
                else
                {
                    FindObjectsOfType<DescriptionText>().ForEach(w => w.UpdateText(descriptionTextFrench));
                }
            }
        }

        public override void DeSelect()
        {
            isSelected = false;
            
            text.color = normalColor;
            pulse?.Kill();
            text.rectTransform.DOScale(1f, .2f).SetUpdate(true);

            OnHighLigh.RemoveListener(() => ChangeLanguage());
        }

        public override void OnClick()
        {
            base.OnClick();
            OnSelect?.Invoke();
            if(isActive)
                SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Confirm, null);
        }

        private const float wait = .7f;
        private float c_wait = 0f;
        
        private void Update()
        {
            if (!isActive && !Player.isInPauseMenu)
                return;

            if (TextNavigation != null)
            {
                if(!TextNavigation.isActive)
                    return;
            }
            
            if (isSelected)
            {
                if (Gamepad.current != null)
                {
                    if (Gamepad.current.dpad.right.wasReleasedThisFrame ||
                        Gamepad.current.leftStick.right.wasReleasedThisFrame)
                        c_wait = 0f;
                    if (Gamepad.current.dpad.left.wasReleasedThisFrame ||
                        Gamepad.current.leftStick.left.wasReleasedThisFrame)
                        c_wait = 0f;
                    
                    if (Gamepad.current.dpad.right.wasPressedThisFrame || Gamepad.current.leftStick.right.wasPressedThisFrame)
                    {
                        c_wait = 0f;
                        
                        Slider?.Add();
                        TextChoice?.Next();
                        
                        if (SliderSimple != null)
                        {
                            SliderSimple.value++;
                            SliderSimple.value = Mathf.Clamp(SliderSimple.value, 0, SliderSimple.maxValue);
                            return;
                        }
                    }

                    if (Gamepad.current.dpad.left.wasPressedThisFrame || Gamepad.current.leftStick.left.wasPressedThisFrame)
                    {
                        c_wait = 0f;
                        
                        Slider?.Remove();
                        TextChoice?.Previous();
                        
                        if (SliderSimple != null)
                        {
                            SliderSimple.value--;
                            SliderSimple.value = Mathf.Clamp(SliderSimple.value, 0, SliderSimple.maxValue);
                            return;
                        }
                    }

                    if (Gamepad.current.dpad.right.isPressed || Gamepad.current.leftStick.right.IsPressed())
                    {
                        c_wait += Time.deltaTime;
                    }

                    if (Gamepad.current.dpad.left.isPressed || Gamepad.current.leftStick.left.isPressed)
                    {
                        c_wait += Time.deltaTime;
                    }
                        
                    if(c_wait < wait)
                        return;
                    
                    if (Gamepad.current.dpad.right.isPressed || Gamepad.current.leftStick.right.IsPressed())
                    {
                        if (SliderSimple != null)
                        {
                            SliderSimple.value++;
                            SliderSimple.value = Mathf.Clamp(SliderSimple.value, 0, SliderSimple.maxValue);
                        }
                    }

                    if (Gamepad.current.dpad.left.isPressed || Gamepad.current.leftStick.left.isPressed)
                    {
                        if (SliderSimple != null)
                        {
                            SliderSimple.value--;
                            SliderSimple.value = Mathf.Clamp(SliderSimple.value, 0, SliderSimple.maxValue);
                        }
                    }
                }
            }
        }
    }
}

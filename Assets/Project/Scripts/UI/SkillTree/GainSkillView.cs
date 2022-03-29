using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;

namespace TheFowler
{
    public class GainSkillView : UIView
    {
        [TabGroup("References")]
        [SerializeField] private Image character;
        [TabGroup("References")]
        [SerializeField] private Sprite[] charactersSprite;
        [TabGroup("References")]
        [SerializeField] private MMFeedbacks feedbacks;
        [TabGroup("References")]
        [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;

        private int number;
        private int currentIteration;

        public override void Show()
        {
            base.Show();
            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = true;

            GeneralInputsHandler.Instance.RobynInputs.enabled = false;
            GeneralInputsHandler.Instance.enabled = false;

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
            infoButtons[0] = InfoBoxButtons.CONFIRM;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);

        }

        public override void Hide()
        {
            base.Hide();


            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = false;

            GeneralInputsHandler.Instance.enabled = true;
            GeneralInputsHandler.Instance.RobynInputs.enabled = true;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).Hide();

        }

         [Button]
        public void Show(int value)
        {
            Show();



            number = value;

            RepeatFeedback();


        }

        public void RepeatFeedback()
        {
            if(currentIteration < number)
            {
                character.sprite = charactersSprite[currentIteration];


                StartCoroutine(WaitForFeedbacks());
                currentIteration++;
            }
            else
            {
                Hide();
                currentIteration = 0;
            }

        }

        private IEnumerator WaitForFeedbacks()
        {
            feedbacks.PlayFeedbacks();
            yield return WaitForKeyPress("Next");
            feedbacks.ResumeFeedbacks();
            yield return null;
        }

        private IEnumerator WaitForKeyPress(string input)
        {
            

            bool done = false;
            while (!done) // essentially a "while true", but with a bool to break out naturally
            {
                if (Inputs.actions[input].IsPressed())
                {
                    done = true; // breaks the loop
                }
                yield return null; // wait until next frame, then continue execution from here (loop continues)
            }

            // now this function returns
        }
    }
}


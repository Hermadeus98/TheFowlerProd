using System;
using QRCode.Extensions;
using QRCode.Utils;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace QRCode.Demos
{
    public class GameEventTests : MonoBehaviour
    {
        //---<NOTICE>------------------------------------------------------------------------------------------------------<
        // 1. The event system register each event with an uniq id.
        // 2. The id is defined by a pin of the Enum address and the selected element in the address.
        // 3. An address with a pin = 12 and the 13th selected have an address equals at 1213.

        //--<Private>
        [Title("Simple Event")] [SerializeField]
        private MeshRenderer meshRenderer = default;

        [Title("One Parameter Event")] [SerializeField]
        private TextMeshProUGUI OneParamText = default;

        [Title("Return Value Event")] [SerializeField]
        private TMP_InputField InputField = default;

        [SerializeField] private TextMeshProUGUI NameText = default;

        //---<INITIALIZATION>----------------------------------------------------------------------------------------------<
        private void OnEnable()
        {
            //Add an event listener
            //At first you have to register the event in OnEnable.

            //--<Simple Event>
            GameEvent.AddListener(AddressTest.ChangeColorRandomly, ChangeRandomColor);

            //--<One Parameter Event>
            GameEvent<float>.AddListener(AddressTest.FloatChange, OnFloatChanged);

            //--<Return Value Event>
            GameEvent.AddListener<string>(AddressTest.ReturnValue, GetName);

            //--<With Event Args>
            GameEvent<EventArgs>.AddListener(AddressTest.WithEventArgs, DebugEvent);

            //--<Return Event Args>
            GameEvent.AddListener<EventArgs>(AddressTest.ReturnEventArgs, GetEventArgs);
        }

        private void OnDisable()
        {
            //Remove an event Listener
            //When you register an event, remove it if the game object is disable.

            //--<Simple Event>
            GameEvent.RemoveListener(AddressTest.ChangeColorRandomly, ChangeRandomColor);

            //--<One Parameter Event>
            GameEvent<float>.RemoveListener(AddressTest.FloatChange, OnFloatChanged);

            //--<Return Value Event>
            GameEvent.RemoveListener<string>(AddressTest.ReturnValue, GetName);

            //--<With Event Args>
            GameEvent<EventArgs>.RemoveListener(AddressTest.WithEventArgs, DebugEvent);

            //--<Return Event Args>
            GameEvent.RemoveListener<EventArgs>(AddressTest.ReturnEventArgs, GetEventArgs);
        }

        //---<UPDATE>------------------------------------------------------------------------------------------------------<
        private void Update()
        {
            //Broadcast you're event.

            //--<Simple Event>
            if (Input.GetKeyDown(KeyCode.A))
            {
                GameEvent.Broadcast(AddressTest.ChangeColorRandomly);
            }

            //--<One Parameter Event>
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GameEvent<float>.Broadcast(AddressTest.FloatChange, Random.Range(0f, 100f));
            }

            //--<Return Value Event>
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameEvent.Broadcast<string>(AddressTest.ReturnValue, SetName);
            }

            //--<With Event Args>
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameEvent<EventArgs>.Broadcast(AddressTest.WithEventArgs, new FloatEventArgs() {Value = 1.236f});
            }

            //--<Return Event Args>
            if (Input.GetKeyDown(KeyCode.T))
            {
                GameEvent.Broadcast<EventArgs>(AddressTest.WithEventArgs, DebugFloatArg);
            }
        }

        //---<TESTS>-------------------------------------------------------------------------------------------------------<

        //Writing an Event Listener

        //--<Simple Event>
        void ChangeRandomColor()
        {
            meshRenderer.material.color = new Color().GetRandomColor();
        }

        //--<One Parameter Event>
        void OnFloatChanged(float newValue)
        {
            OneParamText.SetText($"{newValue}");
        }

        //--<Return Value Parameter>
        string GetName()
        {
            return InputField.text;
        }

        void SetName(string name)
        {
            NameText.SetText(name);
        }

        //--<With Event Args>
        void DebugEvent(EventArgs args)
        {
            if (args is FloatEventArgs)
            {
                var cast = args as FloatEventArgs;
                Debug.Log(cast.Value);
            }
        }

        //--<Return Event Args>
        EventArgs GetEventArgs()
        {
            return new FloatEventArgs() {Value = 1658.6f};
        }

        void DebugFloatArg(EventArgs args)
        {
            if (args is FloatEventArgs)
            {
                var cast = args as FloatEventArgs;
                Debug.Log(cast.Value);
            }
        }
    }

    /// <summary>
    /// To define a new adress, create an enum with an identification number.
    /// Each addresses must have a different pin number.
    /// </summary>
    [Address(1687, typeof(AddressTest))]
    public enum AddressTest : byte
    {
        ChangeColorRandomly,
        FloatChange,
        ReturnValue,
        WithEventArgs,
        ReturnEventArgs
    }
}

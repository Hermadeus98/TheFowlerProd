using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SetupTest : MonoBehaviour
{
    //---<NOTICE>------------------------------------------------------------------------------------------------------<
    // 1. Create a new enum for the address and another one for the keys with the [Address] attribute.
    // 2. Add a new event with an address, and AddKey() to add new conditions.
    // 3. Add an OnComplete callback when every keys are unlocked.
    // 4. Each time you will try to UnlockKey, the SetupEvent system will try to deliver the onComplete callback of
    //    the chosen address.
    
    //--<Private>
    [Title("Simple Test")] [SerializeField]
    private TextMeshProUGUI ResultText = default;

    //---<INITIALISATION>----------------------------------------------------------------------------------------------<
    private void OnEnable()
    {
        //When you add a new key, a new adress is setup.
        SetupEvent.AddKey(DoorTest.Door_01, KeyTest.Key_01, () => Debug.Log("Key One Allow to open door !"));
        //When you add a new key when an adress was declared, whe add just a new condition.
        SetupEvent.AddKey(DoorTest.Door_01, KeyTest.Key_02, () => Debug.Log("Key Two Allow to open door !"));
        SetupEvent.AddKey(DoorTest.Door_01, KeyTest.Key_03, () => Debug.Log("Key Three Allow to open door !"));
        SetupEvent.AddKey(DoorTest.Door_01, KeyTest.Key_04, () => Debug.Log("Key Four Allow to open door !"));
        
        //When all key of this address will be opened, the address will invoke a callback.
        //Note that you can add quickly callback by the AddKey() method!
        SetupEvent.CallBackOnComplete(DoorTest.Door_01, OpenComplete);
    }

    //---<TESTS>-------------------------------------------------------------------------------------------------------<
    public void OpenKey01(bool value)
    {
        if (value)
        {
            SetupEvent.UnlockKey(DoorTest.Door_01, KeyTest.Key_01);
        }
        else
        {
            SetupEvent.LockKey(DoorTest.Door_01, KeyTest.Key_01);
        }
    }
    
    public void OpenKey02(bool value)
    {
        if (value)
        {
            SetupEvent.UnlockKey(DoorTest.Door_01, KeyTest.Key_02);
        }
        else
        {
            SetupEvent.LockKey(DoorTest.Door_01, KeyTest.Key_02);
        }
    }
    
    public void OpenKey03(bool value)
    {
        if (value)
        {
            SetupEvent.UnlockKey(DoorTest.Door_01, KeyTest.Key_03);
        }
        else
        {
            SetupEvent.LockKey(DoorTest.Door_01, KeyTest.Key_03);
        }
    }
    
    public void OpenKey04(bool value)
    {
        if (value)
        {
            SetupEvent.UnlockKey(DoorTest.Door_01, KeyTest.Key_04);
        }
        else
        {
            SetupEvent.LockKey(DoorTest.Door_01, KeyTest.Key_04);
        }
    }

    //---<CALLBACKS>---------------------------------------------------------------------------------------------------<
    public void OpenComplete()
    {
        ResultText.SetText("OPEN !");
        ResultText.color = new Color().ToColor("#27ae60");
    }
}

//---<ENUMS>-----------------------------------------------------------------------------------------------------------<
[Address(0, typeof(DoorTest))]
public enum DoorTest : byte
{
    Door_01
}

[Address(1, typeof(KeyTest))]
public enum KeyTest : byte
{
    Key_01,
    Key_02,
    Key_03,
    Key_04,
}

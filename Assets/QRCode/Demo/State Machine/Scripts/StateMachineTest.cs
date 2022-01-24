using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using MoreMountains;

public class StateMachineTest : SerializedMonoBehaviour
{
    
    
    
    
    
    
    
    
    
    
    private StateMachine stateMachine; 
    WrapperArgs<MyArgDeTest> wrapper = new WrapperArgs<MyArgDeTest>(new MyArgDeTest() {a = 5f});

    public UnityEvent Event;

    public Camera Camera;
    
    [Button]
    public void Init()
    {
        var states = new State[]
        {
            new StateTest() {StateName = "A"},
            new StateTest() {StateName = "B"},
            new StateTest() {StateName = "C"}
        };
        
        stateMachine = new StateMachine(states, "A", UpdateMode.Update, wrapper);
    }

    [Button]
    public void ChangeState(string key)
    {
        stateMachine.SetState(key, wrapper);
    }
}

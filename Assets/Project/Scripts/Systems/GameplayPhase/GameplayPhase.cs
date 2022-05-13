using System;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using TheFowler;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameplayPhase : GameplayMonoBehaviour
{
    [TitleGroup("General Settings"), PropertyOrder]
    [SerializeField] private GameplayPhaseEnum gameplayPhase_id = GameplayPhaseEnum.NULL;
    
    [TitleGroup("On Start")]
    [SerializeField] protected GameInstructions OnStart;

    [TitleGroup("On Start")] [SerializeField]
    private UnityEvent onStartEvent;
    
    [TitleGroup("On End")]
    [SerializeField] private GameplayPhaseEnum onEndGameplayPhase_id = GameplayPhaseEnum.NULL;
    [TitleGroup("On End")]
    [SerializeField] protected GameInstructions OnEnd;
    [TitleGroup("On End")] [SerializeField]
    private UnityEvent onEndEvent;

    [TabGroup("Debug")]
    [SerializeField, ReadOnly] protected bool isActive = false;

    public bool IsActive { get => isActive; }
    public GameplayPhaseEnum GameplayPhaseID { get => gameplayPhase_id; set => gameplayPhase_id = value; }

    protected bool callOnEndEvent = true;

    [Button]
    public virtual void PlayPhase()
    {
        isActive = true;
        GeneralInputsHandler.Instance.enabled = false;

        UI.GetView<InfoBoxView>(UI.Views.InfoBox).Hide();

        QRDebug.Log("GAMEPLAY PHASE", FrenchPallet.PETER_RIVER, $"{gameplayPhase_id} STARTED");

        OnStart.Call();
        onStartEvent?.Invoke();


    }

    [Button]
    public virtual void EndPhase()
    {
        QRDebug.Log("GAMEPLAY PHASE", FrenchPallet.PETER_RIVER, $"{gameplayPhase_id} FINISH");
        
        isActive = false;

        GeneralInputsHandler.Instance.enabled = true;

        if (callOnEndEvent)
        {
            OnEnd.Call();
            onEndEvent?.Invoke();
            if (onEndGameplayPhase_id != GameplayPhaseEnum.NULL)
                GameplayPhaseManager.PlayGameplayPhase(onEndGameplayPhase_id);
        }
    }


    protected virtual void Update()
    {
        /*if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EndPhase();
        }*/
        
        if(!isActive)
            return;
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
        if(gameplayPhase_id != GameplayPhaseEnum.NULL)
            GameplayPhaseManager.RegisterGameplayPhase(this);
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
        if(gameplayPhase_id != GameplayPhaseEnum.NULL)
            GameplayPhaseManager.UnregisterGameplayPhase(this);
    }
}

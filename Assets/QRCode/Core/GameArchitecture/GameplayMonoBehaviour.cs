using System;
using QRCode.Extensions;
using Sirenix.OdinInspector;

namespace QRCode
{
    public class GameplayMonoBehaviour : SerializedMonoBehaviour, ISavable
    {
        protected virtual void OnEnable()
        {
            RegisterEvent();
        }

        protected virtual void OnDisable()
        {
            UnregisterEvent();
        }

        protected virtual void OnDestroy()
        {
            UnregisterEvent();
        }

        private void Start()
        {
            OnStart();
        }

        //---<GAME>----------------------------------------------------------------------------------------------------<
        protected virtual void OnStart(){ }
        
        protected virtual void Initialize(EventArgs args){ }

        protected virtual void SetDependancies()
        {
            QRDebug.Log("Game", FrenchPallet.CARROT, "Set Dependancies", gameObject);
        }
        
        protected virtual void OnGameStart()
        {
            QRDebug.Log("Game", FrenchPallet.CARROT, "Start", gameObject);
        }
        
        protected virtual void OnPause()
        {
            QRDebug.Log("Game", FrenchPallet.CARROT, "Pause", gameObject);
        }
        
        protected virtual void OnUnpause()
        {
            QRDebug.Log("Game", FrenchPallet.CARROT, "UnPause", gameObject);
        }
        
        //---<EVENTS>--------------------------------------------------------------------------------------------------<
        protected virtual void RegisterEvent()
        {
            GameEvent.AddListener(GameEventAddressCore.SetDependancies, SetDependancies);
            GameEvent.AddListener(GameEventAddressCore.OnGameStart, OnGameStart);
            GameEvent.AddListener(GameEventAddressCore.OnGamePause, OnPause);
            GameEvent.AddListener(GameEventAddressCore.OnGameUnpause, OnUnpause);
        }

        protected virtual void UnregisterEvent()
        {
            GameEvent.RemoveListener(GameEventAddressCore.SetDependancies, SetDependancies);
            GameEvent.RemoveListener(GameEventAddressCore.OnGameStart, OnGameStart);
            GameEvent.RemoveListener(GameEventAddressCore.OnGamePause, OnPause);
            GameEvent.RemoveListener(GameEventAddressCore.OnGameUnpause, OnUnpause);
        }

        //---<SAVE>----------------------------------------------------------------------------------------------------<
        public virtual void Save()
        {
            
        }

        public virtual void Load()
        {
            
        }
    }
}

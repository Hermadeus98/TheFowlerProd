using System;

namespace QRCode.Utils
{
    public class UpdateRunner : MonoBehaviourSingleton<UpdateRunner>
    {
        public static event Action UpdateEvent, FixedUpdateEvent, LateUpdateEvent;

        public void Register(UpdateMode updateMode, Action method)
        {
            switch (updateMode)
            {
                case UpdateMode.Update:
                    UpdateEvent += method;
                    break;
                case UpdateMode.FixedUpdate:
                    FixedUpdateEvent += method;
                    break;
                case UpdateMode.LateUpdate:
                    LateUpdateEvent += method;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(updateMode), updateMode, null);
            }
        }
        
        public void UnRegister(UpdateMode updateMode, Action method)
        {
            switch (updateMode)
            {
                case UpdateMode.Update:
                    UpdateEvent -= method;
                    break;
                case UpdateMode.FixedUpdate:
                    FixedUpdateEvent -= method;
                    break;
                case UpdateMode.LateUpdate:
                    LateUpdateEvent -= method;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(updateMode), updateMode, null);
            }
        }
        
        private void Update()
        {
            UpdateEvent?.Invoke();
        }
        
        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }
        
        private void LateUpdate()
        {
            LateUpdateEvent?.Invoke();
        }
    }
}

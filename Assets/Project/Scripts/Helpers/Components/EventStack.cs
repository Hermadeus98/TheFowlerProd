using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class EventStack : SerializedMonoBehaviour
    {
        public EventStackElement[] EventStackElements;

        private bool isActive = false;

        private float delay;
        private float d_t;
        [SerializeField, ReadOnly] private int currentIndex;
        
        [Button]
        public void Play()
        {
            if(EventStackElements.IsNullOrEmpty())
                return;

            currentIndex = 0;
            delay = EventStackElements[currentIndex].duration;
            EventStackElements[currentIndex].UnityEvent.Invoke();
            d_t = delay;
        }

        private void Update()
        {   
            if(!isActive)
                return;

            if (d_t > 0)
            {
                d_t -= Time.deltaTime;
            }
            else
            {
                Next();
            }
        }

        [ButtonGroup()]
        public void Next()
        {
            currentIndex++;
            
            if(currentIndex >= EventStackElements.Length)
                return;
            
            EventStackElements[currentIndex].UnityEvent.Invoke();
            delay = EventStackElements[currentIndex].duration;
            d_t = delay;
        }

        [ButtonGroup()]
        public void Previous()
        {
            currentIndex--;

            if (currentIndex < 0)
            {
                currentIndex = -1;
                return;
            }

            EventStackElements[currentIndex].UnityEvent.Invoke();
            delay = EventStackElements[currentIndex].duration;
            d_t = delay;
        }

        public void Stop()
        {
            
        }

        public void Restart()
        {
            
        }
    }
    
    public class EventStackElement
    {
        public UnityEvent UnityEvent = new UnityEvent();
        public float duration;
    }
}

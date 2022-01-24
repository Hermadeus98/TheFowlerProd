using System.Collections.Generic;
using UnityEngine;

namespace QRCode
{
    public class Observer<T> : MonoBehaviourSingleton<Observer<T>> where T : IObservable
    {
        private List<T> observables = new List<T>();

        public void Register(T observable)
        {
            observables.Add(observable);

            Debug.Log("Register " + observables.Count);
        }

        public void Unregister(T observable)
        {
            observables.Remove(observable);

            Debug.Log("Unregister " + observables.Count);
        }

        public void Notify()
        {
            for (int i = 0; i < observables.Count; i++)
                observables[i].OnNotify();
        }
    }
}
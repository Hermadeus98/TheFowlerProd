using System.Collections;
using System.Collections.Generic;

namespace QRCode.Utils
{
    public class Coroutiner : MonoBehaviourSingleton<Coroutiner>
    {
        //--<Privates>
        private List<IEnumerator> Coroutines = new List<IEnumerator>();

        //---<CORE>----------------------------------------------------------------------------------------------------<
        public void Play(IEnumerator enumerator)
        {
            StartCoroutine(GetIENumerator(enumerator));
        }

        public void Stop(IEnumerator enumerator)
        {
            StopCoroutine(GetIENumerator(enumerator));
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        private IEnumerator GetIENumerator(IEnumerator enumerator)
        {
            IEnumerator n = null;
            
            for (int i = 0; i < Coroutines.Count; i++)
            {
                if (Coroutines[i] == enumerator)
                    n = Coroutines[i];
            }

            if (n == null)
            {
                Coroutines.Add(enumerator);
                n = enumerator;
            }
            
            return n;
        }
    }
}

using System.Collections;
using System.Collections.Generic;

namespace QRCode.Utils
{
    public class Coroutiner : MonoBehaviourSingleton<Coroutiner>
    {
        //--<Privates>
        private static List<IEnumerator> Coroutines = new List<IEnumerator>();

        //---<CORE>----------------------------------------------------------------------------------------------------<
        public static void Play(IEnumerator enumerator)
        {
            Instance.StartCoroutine(GetIENumerator(enumerator));
        }

        public static void Stop(IEnumerator enumerator)
        {
            Instance.StopCoroutine(GetIENumerator(enumerator));
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        private static IEnumerator GetIENumerator(IEnumerator enumerator)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.numberOfAllies)]
    public class NumberOfAllies : ScriptableObject
    {

        public int numberOfAllies;


        public void Reset()
        {
            numberOfAllies = 2;
        }
    }

}

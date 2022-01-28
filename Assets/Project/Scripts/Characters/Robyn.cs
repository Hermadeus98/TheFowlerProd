using UnityEngine;

namespace TheFowler
{
    public class Robyn : Character
    {
        protected override void OnStart()
        {
            base.OnStart();
            Player.Robyn = this;
            Debug.Log(2);
        }
    }
}

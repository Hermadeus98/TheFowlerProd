using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCode
{
    public abstract class State : Istate
    {
        public string StateName { get; set; }
        public abstract void OnStateEnter(EventArgs arg);

        public abstract void OnStateExecute();

        public abstract void OnStateExit(EventArgs arg);
    }
}

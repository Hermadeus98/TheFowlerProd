using System;

namespace QRCode
{
    public interface Istate
    {
        public string StateName { get; set; }
        public abstract void OnStateEnter(EventArgs arg);

        public abstract void OnStateExecute();

        public abstract void OnStateExit(EventArgs arg);
    }
}

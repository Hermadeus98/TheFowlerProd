using System;

namespace QRCode
{
    public class WrapperArgs<T> : EventArgs
    {
        //--<Argument>
        public T Arg { get; private set; }
        
        //--<Constructors>
        public WrapperArgs(T arg) => this.Arg = arg;
    }

    public class WrapperArgs<T,U> : EventArgs
    {
        //--<Arguments>
        public T Arg1 { get; private set; }
        public U Arg2 { get; private set; }
        
        //--<Constructors>
        public WrapperArgs(T arg1, U arg2)
        {
            this.Arg1 = arg1;
            this.Arg2 = arg2;
        }
    }
    
    public class WrapperArgs<T,U,V> : EventArgs
    {
        //--<Argument>
        public T Arg1 { get; private set; }
        public U Arg2 { get; private set; }
        public V Arg3 { get; private set; }

        //--<Constructors>
        public WrapperArgs(T arg1, U arg2, V arg3)
        {
            this.Arg1 = arg1;
            this.Arg2 = arg2;
            this.Arg3 = arg3;
        }
    }
}


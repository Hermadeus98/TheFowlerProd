using System;

namespace QRCode.Utils
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T Value { get; set; }
    }
    
    public class FloatEventArgs : GenericEventArgs<float> { }
    
    public class IntEventArgs : GenericEventArgs<int> { }
    
    public class StringEventArgs : GenericEventArgs<string> { }
    
    public class BoolEventArgs : GenericEventArgs<bool> { }
}

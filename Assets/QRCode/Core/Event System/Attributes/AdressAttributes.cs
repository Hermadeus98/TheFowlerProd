using System;

namespace QRCode
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class AddressAttribute : Attribute
    {
        public AddressAttribute(ushort pin, Type typeReference)
        {
            this.Pin = pin;
            TypeReference = typeReference;
        }
        
        public ushort Pin { get; private set; }
        
        public Type TypeReference { get; private set; }
    }
}

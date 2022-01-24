using System;
using System.Collections.Generic;

namespace QRCode
{
    /// <summary>
    /// Used to translate an enum to an ushort address.
    /// </summary>
    public class EventTranslator
    {
        //---<DATA>----------------------------------------------------------------------------------------------------<
        private readonly Dictionary<Type, ushort> baseCodesTable;

        //---<CONSTRUCTOR>---------------------------------------------------------------------------------------------<
        public EventTranslator()
        {
            baseCodesTable = new Dictionary<Type, ushort>();
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public ushort GetBaseCode(Type type) => baseCodesTable[type];

        public ushort Translate(Enum adress) =>
            ushort.Parse(GetBaseCode(adress.GetType()).ToString() + Convert.ToByte(adress));

        public void Add(Type type, ushort code)
        {
            if (!baseCodesTable.ContainsKey(type))
            {
                baseCodesTable.Add(type, code);
            }
        }
    }
}

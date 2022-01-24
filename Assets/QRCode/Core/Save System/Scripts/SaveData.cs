using System;

namespace QRCode
{
    [Serializable]
    public class SaveData
    {
        private static SaveData _current;

        public static SaveData current
        {
            get
            {
                if (_current == null)
                {
                    _current = new SaveData();
                }

                return _current;
            }
        }

        public int intValue;
    }
}
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace QRCode
{
    public static class SerializationManager
    {
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public static bool Save(string saveName, object saveData)
        {
            var formatter = GetBinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            var path = Application.persistentDataPath + "/saves/" + saveName + ".save";

            var file = File.Create(path);
            
            formatter.Serialize(file, saveData);
            
            file.Close();

            return true;
        }

        public static object Load(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            var formatter = GetBinaryFormatter();

            var file = File.Open(path, FileMode.Open);

            try
            {
                var save = formatter.Deserialize(file);
                file.Close();
                return save;
            }
            catch
            {
                throw new Exception($"Failed to load file at {path}");
            }
        }

        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        private static BinaryFormatter GetBinaryFormatter()
        {
            var formatter = new BinaryFormatter();

            var selector = new SurrogateSelector();

            var vector3Surrogate = new Vector3SerializationSurrogate();
            selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);

            var quaternionSurrogate = new QuaternionSerializationSurrogate();
            selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);
            
            formatter.SurrogateSelector = selector;
            
            return formatter;
        }
    }
}

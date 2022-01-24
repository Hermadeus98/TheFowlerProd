using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QRCode
{
    /// <summary>
    /// Use with Game Event for register all Delegates.
    /// </summary>
    public static class GameEventInternal
    {
        //---<DATA>---------------------------------------------------------------------------------------------------<
        readonly public static Dictionary<ushort, Delegate> eventsTable = new Dictionary<ushort, Delegate>();
        public static MessengerMode DEFAULT_MODE = MessengerMode.REQUIRE_LISTENER;

        public static EventTranslator Translator;
        
        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            Translator = new EventTranslator();
            
            var attributes = typeof(GameEventAddressGameplay).GetCustomAttributes(true);
            var attributes1 = typeof(GameEventAddressCore).GetCustomAttributes(true);
            //var attributes2 = typeof(AddressTest).GetCustomAttributes(true);
        
            foreach (var attribute in attributes)
            {
                if (attribute is AddressAttribute address)
                {
                    Translator.Add(address.TypeReference, address.Pin);
                }
            }
            
            foreach (var attribute in attributes1)
            {
                if (attribute is AddressAttribute address)
                {
                    Translator.Add(address.TypeReference, address.Pin);
                }
            }
            
            //-FOR DEMO
            /*foreach (var attribute in attributes2)
            {
                if (attribute is AddressAttribute address)
                {
                    Translator.Add(address.TypeReference, address.Pin);
                }
            }*/
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public static void AddListener(ushort address, Delegate callback)
        {
            OnListenerAdding(address, callback);
            eventsTable[address] = Delegate.Combine(eventsTable[address], callback);
        }

        public static void RemoveListener(ushort address, Delegate handler)
        {
            OnListenerRemoving(address, handler);
            eventsTable[address] = Delegate.Remove(eventsTable[address], handler);
            OnListenerRemoved(address);   
        }

        public static T[] GetInvocationList<T>(ushort address)
        {
            if (eventsTable.TryGetValue(address, out var d))
            {
                try
                {
                    return d.GetInvocationList().Cast<T>().ToArray();
                }
                catch
                {
                    throw CreateBroadcastSignatureException(address);
                }
            }

            return new T[0];
        }

        static void OnListenerAdding(ushort address, Delegate listenerBeingAdded)
        {
            if (!eventsTable.ContainsKey(address))
                eventsTable.Add(address, null);

            var d = eventsTable[address];
            if(d != null && d.GetType() != listenerBeingAdded.GetType())
                throw new ListenerException(
                    string.Format(
                        "Attempting to add listener with inconsistent signature for event type {0}." +
                        " Current listeners have type {1} and listener being added has type {2}",
                        address,
                        d.GetType().Name,
                        listenerBeingAdded.GetType().Name));
        }

        static void OnListenerRemoving(ushort address, Delegate listenerBeingRemoved)
        {
            if (eventsTable.ContainsKey(address))
            {
                var d = eventsTable[address];
                if(d == null)
                {
                    throw new ListenerException(
                        string.Format(
                            "Attempting to remove listener with for event type {0} but current listener is null.",
                            address));
                }
                else if(d.GetType() != listenerBeingRemoved.GetType())
                {
                    throw new ListenerException(
                        string.Format(
                            "Attempting to remove listener with inconsistent signature for event type {0}." +
                            " Current listeners have type {1} and listener being removed has type {2}",
                            address,
                            d.GetType().Name,
                            listenerBeingRemoved.GetType().Name));
                }
            }
            else
            {
                throw new ListenerException(
                    string.Format(
                        "Attempting to remove listener for type {0} but Messenger doesn't know about this event type."
                        , address));
            }
        }

        static void OnListenerRemoved(ushort address)
        {
            if(eventsTable[address] == null)
            {
                eventsTable.Remove(address);
            }
        }

        static public void OnBroadcasting(ushort address, MessengerMode mode)
        {
            if (mode == MessengerMode.REQUIRE_LISTENER && !eventsTable.ContainsKey(address))
            {
                throw new BroadcastException(
                    string.Format(
                        "Broadcasting message {0} but no listener found.",
                        address));
            }
        }

        //---<EXCEPTIONS>----------------------------------------------------------------------------------------------<
        static public BroadcastException CreateBroadcastSignatureException(ushort adress)
        {
            return new BroadcastException(
                string.Format(
                    "Broadcasting message {0} but listeners have a different signature than the broadcaster."
                    , adress));
        }

        public class BroadcastException : Exception
        {
            public BroadcastException(string msg)
                : base(msg)
            {
            }
        }

        public class ListenerException : Exception
        {
            public ListenerException(string msg)
                : base(msg)
            {
            }
        }
    }

    //---<GAME EVENT>--------------------------------------------------------------------------------------------------<
    
    /// <summary>
    /// Use this class for register Game Event.
    /// http://wiki.unity3d.com/index.php/CSharpMessenger_Extended
    /// </summary>
    static public class GameEvent
    {
        public static void AddListener(Enum address, Action handler)
        {
            AddListener(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void AddListener(ushort address, Action handler)
        {
            GameEventInternal.AddListener(address, handler);
        }

        public static void AddListener<TReturn>(Enum address, Func<TReturn> handler)
        {
            AddListener<TReturn>(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void AddListener<TReturn>(ushort address, Func<TReturn> handler)
        {
            GameEventInternal.AddListener(address, handler);
        }

        static public void RemoveListener<A>(A address, Action handler)
            where A : Enum
        {
            RemoveListener(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void RemoveListener(ushort address, Action handler)
        {
            GameEventInternal.RemoveListener(address, handler);
        }

        public static void RemoveListener<TReturn>(Enum address, Func<TReturn> handler)
        {
            RemoveListener<TReturn>(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void RemoveListener<TReturn>(ushort address, Func<TReturn> handler)
        {
            GameEventInternal.RemoveListener(address, handler);
        }

        public static void Broadcast<A>(A address) where A : Enum
        {
            Broadcast(GameEventInternal.Translator.Translate(address));
        }
        
        static public void Broadcast(ushort address)
        {
            Broadcast(address, GameEventInternal.DEFAULT_MODE);
        }

        public static void Broadcast<TReturn>(Enum address, Action<TReturn> returnCall)
        {
            Broadcast<TReturn>(GameEventInternal.Translator.Translate(address), returnCall);
        }
        
        static public void Broadcast<TReturn>(ushort address, Action<TReturn> returnCall)
        {
            Broadcast(address, returnCall, GameEventInternal.DEFAULT_MODE);
        }

        public static void Broadcast(Enum address, MessengerMode mode)
        {
            Broadcast(GameEventInternal.Translator.Translate(address), mode);
        }
        
        /// <summary>
        /// Invoke callback register at <see cref="adress"/>
        /// </summary>
        /// <param name="address></param>
        /// <param name="mode"></param>
        static public void Broadcast(ushort address, MessengerMode mode)
        {
            GameEventInternal.OnBroadcasting(address, mode);
            var invocationList = GameEventInternal.GetInvocationList<Action>(address);

            foreach (var callback in invocationList)
                callback.Invoke();
        }

        public static void Broadcast<TReturn>(Enum address, Action<TReturn> returnCall, MessengerMode mode)
        {
            Broadcast<TReturn>(GameEventInternal.Translator.Translate(address), returnCall, mode);
        }
        
        static public void Broadcast<TReturn>(ushort address, Action<TReturn> returnCall, MessengerMode mode)
        {
            GameEventInternal.OnBroadcasting(address, mode);
            var invocationList = GameEventInternal.GetInvocationList<Func<TReturn>>(address);

            foreach (var result in invocationList.Select(del => del.Invoke()).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
    }

    //---<GAME EVENT - ONE PARAMETER>----------------------------------------------------------------------------------<
    static public class GameEvent<T>
    {
        public static void AddListener<A>(A address, Action<T> handler)
            where A : Enum
        {
            AddListener(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void AddListener(ushort address, Action<T> handler)
        {
            GameEventInternal.AddListener(address, handler);
        }
        
        public static void AddListener<TReturn>(Enum address, Func<T, TReturn> handler)
        {
            AddListener<TReturn>(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void AddListener<TReturn>(ushort address, Func<T, TReturn> handler)
        {
            GameEventInternal.AddListener(address, handler);
        }

        public static void RemoveListener(Enum address, Action<T> handler)
        {
            RemoveListener(GameEventInternal.Translator.Translate(address), handler);
        }

        static public void RemoveListener(ushort address, Action<T> handler)
        {
            GameEventInternal.RemoveListener(address, handler);
        }

        public static void RemoveListener<TReturn>(Enum address, Func<T, TReturn> handler)
        {
            RemoveListener(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void RemoveListener<TReturn>(ushort address, Func<T, TReturn> handler)
        {
            GameEventInternal.RemoveListener(address, handler);
        }

        public static void Broadcast(Enum address, T arg1)
        {
            Broadcast(GameEventInternal.Translator.Translate(address), arg1);
        }
        
        static public void Broadcast(ushort address, T arg1)
        {
            Broadcast(address, arg1, GameEventInternal.DEFAULT_MODE);
        }
        
        public static void Broadcast<TReturn>(Enum address, T arg1, Action<TReturn> returnCall)
        {
            Broadcast(GameEventInternal.Translator.Translate(address), arg1, returnCall);
        }

        static public void Broadcast<TReturn>(ushort address, T arg1, Action<TReturn> returnCall)
        {
            Broadcast(address, arg1, returnCall, GameEventInternal.DEFAULT_MODE);
        }

        public static void Broadcast(Enum address, T arg1, MessengerMode mode)
        {
            Broadcast(GameEventInternal.Translator.Translate(address), arg1, mode);
        }

        static public void Broadcast(ushort address, T arg1, MessengerMode mode)
        {
            GameEventInternal.OnBroadcasting(address, mode);
            var invocationList = GameEventInternal.GetInvocationList<Action<T>>(address);

            foreach (var callback in invocationList)
                callback.Invoke(arg1);
        }

        public static void Broadcast<TReturn>(Enum address, T arg1, Action<TReturn> returnCall, MessengerMode mode)
        {
            Broadcast(GameEventInternal.Translator.Translate(address), arg1, returnCall, mode);
        }

        static public void Broadcast<TReturn>(ushort address, T arg1, Action<TReturn> returnCall, MessengerMode mode)
        {
            GameEventInternal.OnBroadcasting(address, mode);
            var invocationList = GameEventInternal.GetInvocationList<Func<T, TReturn>>(address);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
    }

    //---<GAME EVENT - TWO PARAMETERS>---------------------------------------------------------------------------------<
    // Two parameters
    static public class GameEvent<T, U>
    {
        public static void AddListener(Enum address, Action<T, U> handler)
        {
            AddListener(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void AddListener(ushort address, Action<T, U> handler)
        {
            GameEventInternal.AddListener(address, handler);
        }

        public static void AddListener<TReturn>(Enum address, Func<T, U, TReturn> handler)
        {
            AddListener(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void AddListener<TReturn>(ushort address, Func<T, U, TReturn> handler)
        {
            GameEventInternal.AddListener(address, handler);
        }

        static public void RemoveListener(Enum address, Action<T, U> handler)
        {
            RemoveListener(GameEventInternal.Translator.Translate(address), handler);
        }

        static public void RemoveListener(ushort address, Action<T, U> handler)
        {
            GameEventInternal.RemoveListener(address, handler);
        }

        static public void RemoveListner<TReturn>(Enum adress, Func<T, U, TReturn> handler)
        {
            GameEventInternal.RemoveListener(GameEventInternal.Translator.Translate(adress), handler);
        }
        
        static public void RemoveListener<TReturn>(ushort address, Func<T, U, TReturn> handler)
        {
            GameEventInternal.RemoveListener(address, handler);
        }

        static void Broadcast(Enum address, T arg1, U arg2)
        {
            Broadcast(GameEventInternal.Translator.Translate(address), arg1, arg2);
        }
        
        static public void Broadcast(ushort address, T arg1, U arg2)
        {
            Broadcast(address, arg1, arg2, GameEventInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(Enum address, T arg1, U arg2, Action<TReturn> returnCall)
        {
            Broadcast(GameEventInternal.Translator.Translate(address), arg1, arg2, returnCall);
        }

        static public void Broadcast<TReturn>(ushort address, T arg1, U arg2, Action<TReturn> returnCall)
        {
            Broadcast(address, arg1, arg2, returnCall, GameEventInternal.DEFAULT_MODE);
        }

        static public void Broadcast(ushort address, T arg1, U arg2, MessengerMode mode)
        {
            GameEventInternal.OnBroadcasting(address, mode);
            var invocationList = GameEventInternal.GetInvocationList<Action<T, U>>(address);

            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2);
        }

        static public void Broadcast<TReturn>(ushort address, T arg1, U arg2, Action<TReturn> returnCall, MessengerMode mode)
        {
            GameEventInternal.OnBroadcasting(address, mode);
            var invocationList = GameEventInternal.GetInvocationList<Func<T, U, TReturn>>(address);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
    }

    //---<GAME EVENT - THREE PARAMETERS>-------------------------------------------------------------------------------<
    // Three parameters
    static public class GameEvent<T, U, V>
    {
        public static void AddListener(Enum address, Action<T, U, V> handler)
        {
            AddListener(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void AddListener(ushort address, Action<T, U, V> handler)
        {
            GameEventInternal.AddListener(address, handler);
        }

        static public void AddListner<TReturn>(Enum address, Func<T, U, V, TReturn> handler)
        {
            AddListener(GameEventInternal.Translator.Translate(address), handler);
        }

        static public void AddListener<TReturn>(ushort address, Func<T, U, V, TReturn> handler)
        {
            GameEventInternal.AddListener(address, handler);
        }

        static public void RemoveListener(Enum address, Action<T, U, V> handler)
        {
            RemoveListener(GameEventInternal.Translator.Translate(address), handler);
        }

        static public void RemoveListener(ushort address, Action<T, U, V> handler)
        {
            GameEventInternal.RemoveListener(address, handler);
        }

        static public void RemoveListener<TReturn>(Enum address, Func<T, U, V, TReturn> handler)
        {
            RemoveListener(GameEventInternal.Translator.Translate(address), handler);
        }
        
        static public void RemoveListener<TReturn>(ushort address, Func<T, U, V, TReturn> handler)
        {
            GameEventInternal.RemoveListener(address, handler);
        }

        static public void Broadcast(Enum address, T arg1, U arg2, V arg3)
        {
            Broadcast(GameEventInternal.Translator.Translate(address), arg1, arg2, arg3);
        }
        
        static public void Broadcast(ushort address, T arg1, U arg2, V arg3)
        {
            Broadcast(address, arg1, arg2, arg3, GameEventInternal.DEFAULT_MODE);
        }

        static public void Broadcast<TReturn>(Enum address, T arg1, U arg2, V arg3, Action<TReturn> returnCall)
        {
            Broadcast(GameEventInternal.Translator.Translate(address), arg1, arg2, arg3, returnCall);
        }
        
        static public void Broadcast<TReturn>(ushort address, T arg1, U arg2, V arg3, Action<TReturn> returnCall)
        {
            Broadcast(address, arg1, arg2, arg3, returnCall, GameEventInternal.DEFAULT_MODE);
        }

        static public void Broadcast(ushort address, T arg1, U arg2, V arg3, MessengerMode mode)
        {
            GameEventInternal.OnBroadcasting(address, mode);
            var invocationList = GameEventInternal.GetInvocationList<Action<T, U, V>>(address);

            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2, arg3);
        }

        static public void Broadcast<TReturn>(ushort address, T arg1, U arg2, V arg3, Action<TReturn> returnCall, MessengerMode mode)
        {
            GameEventInternal.OnBroadcasting(address, mode);
            var invocationList = GameEventInternal.GetInvocationList<Func<T, U, V, TReturn>>(address);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2, arg3)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
    }

    //---<ENUMS>-------------------------------------------------------------------------------------------------------<
    public enum MessengerMode
    {
        DONT_REQUIRE_LISTENER,
        REQUIRE_LISTENER
    }
}
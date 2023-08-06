
/*
 * Added by mahdi fada
 * Add Time: 5 / 13 / 2020
 * Description: Message
 * Advanced C# messenger by Ilya Suzdalnitski. V1.0
 * 
 * Based on Rod Hyde's "CSharpMessenger" and Magnus Wolffelt's "CSharpMessenger Extended".
 * 
 * Features:
 	* Prevents a MissingReferenceException because of a reference to a destroyed message handler.
 	* Option to log all messages
 	* Extensive error detection, preventing silent bugs
 * 
 * Usage examples:
 	1. Messenger.AddListener<GameObject>("prop collected", PropCollected);
 	   Messenger.Broadcast<GameObject>("prop collected", prop);
 	2. Messenger.AddListener<float>("speed changed", SpeedChanged);
 	   Messenger.Broadcast<float>("speed changed", 0.5f);
 * 
 * Messenger cleans up its evenTable automatically upon loading of a new level.
 * 
 * Don't forget that the messages that should survive the cleanup, should be marked with Messenger.MarkAsPermanent(string)
 * 
 */

//#define LOG_ALL_MESSAGES
//#define LOG_ADD_LISTENER
//#define LOG_BROADCAST_MESSAGE
//#define REQUIRE_LISTENER

using System;
using System.Collections.Generic;

namespace GameWarriors.EventDomain.Core
{
    /// <summary>
    /// The core implementation of event container
    /// </summary>
    /// <typeparam name="EventType">The key type of the message container dictionary </typeparam>
    public class Messenger<EventType>
    {
        #region Internal variables


        //Disable the unused variable warning
#pragma warning disable 0414
        //Ensures that the MessengerHelper will be created automatically upon start of the game.
        //private MessengerHelper messengerHelper = (new GameObject("MessengerHelper")).AddComponent<MessengerHelper>();
#pragma warning restore 0414

        public Dictionary<EventType, Delegate> eventTable = new Dictionary<EventType, Delegate>();

        //Message handlers that should never be removed, regardless of calling Cleanup
        //public List<EventType> permanentMessages = new List<EventType>();
        #endregion
        #region Helper methods
        //Marks a certain message as permanent.
//        public void MarkAsPermanent(EventType eventType)
//        {
//#if LOG_ALL_MESSAGES
//		Debug.Log("Messenger MarkAsPermanent \t\"" + eventType + "\"");
//#endif
//            permanentMessages.Add(eventType);
//        }


        public void Cleanup()
        {
#if LOG_ALL_MESSAGES
		Debug.Log("MESSENGER Cleanup. Make sure that none of necessary listeners are removed.");
#endif

            List<EventType> messagesToRemove = new List<EventType>();

            foreach (KeyValuePair<EventType, Delegate> pair in eventTable)
            {
                bool wasFound = false;

                //foreach (EventType message in permanentMessages)
                //{
                //    //if (pair.Key == message)
                //    if (pair.Key.Equals(message))
                //    {
                //        wasFound = true;
                //        break;
                //    }
                //}

                if (!wasFound)
                    messagesToRemove.Add(pair.Key);
            }

            foreach (EventType message in messagesToRemove)
            {
                eventTable.Remove(message);
            }
        }

        public void ClearListener<T>(T message) where T : EventType
        {
            eventTable.Remove(message);
        }

        //public void PrintEventTable()
        //{
        //    Debug.Log("\t\t\t=== MESSENGER PrintEventTable ===");

        //    foreach (KeyValuePair<EventType, Delegate> pair in eventTable)
        //    {
        //        Debug.Log("\t\t\t" + pair.Key + "\t\t" + pair.Value);
        //    }

        //    Debug.Log("\n");
        //}
        #endregion

        #region Message logging and exception throwing
        public void OnListenerAdding(EventType eventType, Delegate listenerBeingAdded)
        {
#if LOG_ALL_MESSAGES || LOG_ADD_LISTENER
		Debug.Log("MESSENGER OnListenerAdding \t\"" + eventType + "\"\t{" + listenerBeingAdded.Target + " -> " + listenerBeingAdded.Method + "}");
#endif

            if (!eventTable.ContainsKey(eventType))
            {
                eventTable.Add(eventType, null);
            }

            Delegate d = eventTable[eventType];
            if (d != null && d.GetType() != listenerBeingAdded.GetType())
            {
                throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
            }
        }

        public bool OnListenerRemoving(EventType eventType, Delegate listenerBeingRemoved)
        {
#if LOG_ALL_MESSAGES
		Debug.Log("MESSENGER OnListenerRemoving \t\"" + eventType + "\"\t{" + listenerBeingRemoved.Target + " -> " + listenerBeingRemoved.Method + "}");
#endif

            if (eventTable.ContainsKey(eventType))
            {
#if REMOVE_EXCEPTION
                Delegate d = eventTable[eventType];

                if (d == null)
                {
                    throw new ListenerException(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
                }
                else if (d.GetType() != listenerBeingRemoved.GetType())
                {
                    throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
                }
#endif
                return true;
            }
            else
            {
#if REMOVE_EXCEPTION
                throw new ListenerException(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
#endif
            }
            return false;
        }

        public void OnListenerRemoved(EventType eventType)
        {
            if (eventTable[eventType] == null)
            {
                eventTable.Remove(eventType);
            }
        }

        public void OnBroadcasting(EventType eventType)
        {
#if REQUIRE_LISTENER
            if (!eventTable.ContainsKey(eventType))
            {
                throw new BroadcastException(string.Format("Broadcasting message \"{0}\" but no listener found. Try marking the message with Messenger.MarkAsPermanent.", eventType));
            }
#endif
        }

        public BroadcastException CreateBroadcastSignatureException(EventType eventType)
        {
            return new BroadcastException(string.Format("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", eventType));
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
        #endregion

        #region AddListener
        //No parameters
        public void AddListener(EventType eventType, Action handler)
        {
            OnListenerAdding(eventType, handler);
            eventTable[eventType] = (Action)eventTable[eventType] + handler;
        }

        //Single parameter
        public void AddListener<T>(EventType eventType, Action<T> handler)
        {
            OnListenerAdding(eventType, handler);
            eventTable[eventType] = (Action<T>)eventTable[eventType] + handler;
        }

        //Two parameters
        public void AddListener<T, U>(EventType eventType, Action<T, U> handler)
        {
            OnListenerAdding(eventType, handler);
            eventTable[eventType] = (Action<T, U>)eventTable[eventType] + handler;
        }

        //Three parameters
        public void AddListener<T, U, V>(EventType eventType, Action<T, U, V> handler)
        {
            OnListenerAdding(eventType, handler);
            eventTable[eventType] = (Action<T, U, V>)eventTable[eventType] + handler;
        }

        //Four parameters
        public void AddListener<T, U, V, W>(EventType eventType, Action<T, U, V, W> handler)
        {
            OnListenerAdding(eventType, handler);
            eventTable[eventType] = (Action<T, U, V, W>)eventTable[eventType] + handler;
        }

        #endregion

        #region RemoveListener
        //No parameters
        public bool RemoveListener(EventType eventType, Action handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                eventTable[eventType] = (Action)eventTable[eventType] - handler;
                OnListenerRemoved(eventType);
                return true;
            }
            return false;
        }

        //Single parameter
        public bool RemoveListener<T>(EventType eventType, Action<T> handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                eventTable[eventType] = (Action<T>)eventTable[eventType] - handler;
                OnListenerRemoved(eventType);
                return true;
            }
            return false;
        }

        //Two parameters
        public bool RemoveListener<T, U>(EventType eventType, Action<T, U> handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                eventTable[eventType] = (Action<T, U>)eventTable[eventType] - handler;
                OnListenerRemoved(eventType);
                return true;
            }
            return false;
        }

        //Three parameters
        public bool RemoveListener<T, U, V>(EventType eventType, Action<T, U, V> handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                eventTable[eventType] = (Action<T, U, V>)eventTable[eventType] - handler;
                OnListenerRemoved(eventType);
                return true;
            }
            return false;
        }

        //Four parameters
        public bool RemoveListener<T, U, V, W>(EventType eventType, Action<T, U, V, W> handler)
        {
            if (OnListenerRemoving(eventType, handler))
            {
                eventTable[eventType] = (Action<T, U, V, W>)eventTable[eventType] - handler;
                OnListenerRemoved(eventType);
                return true;
            }
            return false;
        }

        #endregion

        #region Broadcast
        //No parameters
        public void Broadcast(EventType eventType)
        {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
		Debug.Log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
#endif
            OnBroadcasting(eventType);

            Delegate d;
            if (eventTable.TryGetValue(eventType, out d))
            {
                Action Action = d as Action;

                if (Action != null)
                {
                    Action();
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }

        //Single parameter
        public void Broadcast<T>(EventType eventType, T arg1)
        {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
		Debug.Log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
#endif
            OnBroadcasting(eventType);

            Delegate d;
            if (eventTable.TryGetValue(eventType, out d))
            {
                Action<T> Action = d as Action<T>;

                if (Action != null)
                {
                    Action(arg1);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }

        //Two parameters
        public void Broadcast<T, U>(EventType eventType, T arg1, U arg2)
        {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
		Debug.Log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
#endif
            OnBroadcasting(eventType);

            Delegate d;
            if (eventTable.TryGetValue(eventType, out d))
            {
                Action<T, U> Action = d as Action<T, U>;

                if (Action != null)
                {
                    Action(arg1, arg2);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }

        //Three parameters
        public void Broadcast<T, U, V>(EventType eventType, T arg1, U arg2, V arg3)
        {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
		Debug.Log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
#endif
            OnBroadcasting(eventType);

            Delegate d;
            if (eventTable.TryGetValue(eventType, out d))
            {
                Action<T, U, V> Action = d as Action<T, U, V>;

                if (Action != null)
                {
                    Action(arg1, arg2, arg3);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }

        //Four parameters
        public void Broadcast<T, U, V, W>(EventType eventType, T arg1, U arg2, V arg3, W arg4)
        {
#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
		Debug.Log("MESSENGER\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
#endif
            OnBroadcasting(eventType);

            Delegate d;
            if (eventTable.TryGetValue(eventType, out d))
            {
                Action<T, U, V, W> Action = d as Action<T, U, V, W>;

                if (Action != null)
                {
                    Action(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }
        }
        #endregion
    }
}
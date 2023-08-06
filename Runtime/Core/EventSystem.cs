using System;
using GameWarriors.EventDomain.Abstraction;

namespace GameWarriors.EventDomain.Core
{
    /// <summary>
    /// The class which provider all system event features
    /// </summary>
    public class EventSystem : IEvent
    {
        private const int STARTUP_EVENT_ID = -1000;
        private Messenger<int> _eventMessenger;

#if UNITY_2018_1_OR_NEWER
        [UnityEngine.Scripting.Preserve]
#endif
        public EventSystem()
        {
            _eventMessenger = new Messenger<int>();
        }

        public void ListenToEvent(int messageId, Action callEvent)
        {
            _eventMessenger.AddListener(messageId, callEvent);
        }

        public void ListenToEvent<T1>(int messageId, Action<T1> callEvent)
        {
            _eventMessenger.AddListener<T1>(messageId, callEvent);
        }

        public void ListenToEvent<T1, T2>(int messageId, Action<T1, T2> callEvent)
        {
            _eventMessenger.AddListener<T1, T2>(messageId, callEvent);
        }

        public void ListenToEvent<T1, T2, T3>(int messageId, Action<T1, T2, T3> callEvent)
        {
            _eventMessenger.AddListener<T1, T2, T3>(messageId, callEvent);
        }

        public void ListenToEvent<T1, T2, T3, T4>(int messageId, Action<T1, T2, T3, T4> callEvent)
        {
            _eventMessenger.AddListener<T1, T2, T3, T4>(messageId, callEvent);
        }

        public void BroadcastEvent(int messageId)
        {
            _eventMessenger.Broadcast(messageId);
        }

        public void BroadcastEvent<T1>(int messageId, T1 inputValue)
        {
            _eventMessenger.Broadcast<T1>(messageId, inputValue);
        }

        public void BroadcastEvent<T1, T2>(int messageId, T1 inputValue1, T2 inputValue2)
        {
            _eventMessenger.Broadcast(messageId, inputValue1, inputValue2);
        }

        public void BroadcastEvent<T1, T2, T3>(int messageId, T1 inputValue1, T2 inputValue2, T3 inputValue3)
        {
            _eventMessenger.Broadcast(messageId, inputValue1, inputValue2, inputValue3);
        }

        public void BroadcastEvent<T1, T2, T3, T4>(int messageId, T1 inputValue1, T2 inputValue2, T3 inputValue3, T4 inputValue4)
        {
            _eventMessenger.Broadcast(messageId, inputValue1, inputValue2, inputValue3, inputValue4);
        }

        public bool RemoveEventListener<T1>(int messageId, Action<T1> callEvent)
        {
            return _eventMessenger.RemoveListener<T1>(messageId, callEvent);
        }

        public bool RemoveEventListener<T1, T2>(int messageId, Action<T1, T2> callEvent)
        {
            return _eventMessenger.RemoveListener<T1, T2>(messageId, callEvent);
        }

        public bool RemoveEventListener<T1, T2, T3>(int messageId, Action<T1, T2, T3> callEvent)
        {
            return _eventMessenger.RemoveListener<T1, T2, T3>(messageId, callEvent);
        }

        public bool RemoveEventListener<T1, T2, T3, T4>(int messageId, Action<T1, T2, T3, T4> callEvent)
        {
            return _eventMessenger.RemoveListener<T1, T2, T3, T4>(messageId, callEvent);
        }

        public bool RemoveEventListener(int messageId, Action callEvent)
        {
            return _eventMessenger.RemoveListener(messageId, callEvent);
        }

        public void ListenToStartupEvent(Action<IServiceProvider> callEvent)
        {
            _eventMessenger.AddListener(STARTUP_EVENT_ID, callEvent);
        }

        public void BroadcastStartupEvent(IServiceProvider inputValue)
        {
            _eventMessenger.Broadcast(STARTUP_EVENT_ID, inputValue);
        }
    }
}
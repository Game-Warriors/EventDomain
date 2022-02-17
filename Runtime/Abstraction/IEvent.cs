using System;

namespace GameWarriors.EventDomain.Abstraction
{
    public interface IEvent 
    {
        void ListenToEvent(int messageId, Action callEvent);
        void ListenToEvent<T1>(int messageId, Action<T1> callEvent);
        void ListenToEvent<T1, T2>(int messageId, Action<T1, T2> callEvent);
        void ListenToEvent<T1, T2, T3>(int messageId, Action<T1, T2, T3> callEvent);
        void ListenToEvent<T1, T2, T3, T4>(int messageId, Action<T1, T2, T3, T4> callEvent);
        void BroadcastEvent(int messageId);
        void BroadcastEvent<T1>(int messageId, T1 inputValue);
        void BroadcastEvent<T1, T2>(int messageId, T1 inputValue1, T2 inputValue2);
        void BroadcastEvent<T1, T2, T3>(int messageId, T1 inputValue1, T2 inputValue2, T3 inputValue3);
        void BroadcastEvent<T1, T2, T3, T4>(int messageId, T1 inputValue1, T2 inputValue2, T3 inputValue3, T4 inputValue4);
        void RemoveEventListener<T1>(int messageId, Action<T1> callEvent);
        void RemoveEventListener<T1, T2>(int messageId, Action<T1, T2> callEvent);
        void RemoveEventListener<T1, T2, T3>(int messageId, Action<T1, T2, T3> callEvent);
        void RemoveEventListener<T1, T2, T3, T4>(int messageId, Action<T1, T2, T3, T4> callEvent);
        void RemoveEventListener(int messageId, Action callEvent);

        void ListenToInitializeEvent(Action<IServiceProvider> callEvent);
        void BroadcastInitializeEvent(IServiceProvider inputValue);
    }
}

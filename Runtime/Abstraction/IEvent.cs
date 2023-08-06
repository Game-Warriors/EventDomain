using System;

namespace GameWarriors.EventDomain.Abstraction
{
    public interface IEvent 
    {
        /// <summary>
        /// Listen to event by specific id for triggering when broadcasting
        /// </summary>
        /// <param name="messageId">Specific message id for listening</param>
        /// <param name="callEvent">The callback which triggers by event broadcaster</param>
        void ListenToEvent(int messageId, Action callEvent);

        /// <summary>
        /// Listen to event by specific id for triggering when broadcasting
        /// </summary>
        /// <typeparam name="T1">The generic type of callback argument</typeparam>
        /// <param name="messageId">Specific message id for listening</param>
        /// <param name="callEvent">The callback which triggers by event broadcaster</param>
        void ListenToEvent<T1>(int messageId, Action<T1> callEvent);

        /// <summary>
        /// Listen to event by specific id for triggering when broadcasting
        /// </summary>
        /// <typeparam name="T1">The generic type of callback argument</typeparam>
        /// <typeparam name="T2">The generic type of callback argument</typeparam>
        /// <param name="messageId">Specific message id for listening</param>
        /// <param name="callEvent">The callback which triggers by event broadcaster</param>
        void ListenToEvent<T1, T2>(int messageId, Action<T1, T2> callEvent);

        /// <summary>
        /// Listen to event by specific id for triggering when broadcasting
        /// </summary>
        /// <typeparam name="T1">The generic type of callback argument</typeparam>
        /// <typeparam name="T2">The generic type of callback argument</typeparam>
        /// <typeparam name="T3">The generic type of callback argument</typeparam>
        /// <param name="messageId">Specific message id for listening</param>
        /// <param name="callEvent">The callback which triggers by event broadcaster</param>
        void ListenToEvent<T1, T2, T3>(int messageId, Action<T1, T2, T3> callEvent);

        /// <summary>
        /// Listen to event by specific id for triggering when broadcasting
        /// </summary>
        /// <typeparam name="T1">The generic type of callback argument</typeparam>
        /// <typeparam name="T2">The generic type of callback argument</typeparam>
        /// <typeparam name="T3">The generic type of callback argument</typeparam>
        /// <typeparam name="T4">The generic type of callback argument</typeparam>
        /// <param name="messageId">Specific message id for listening</param>
        /// <param name="callEvent">The callback which triggers by event broadcaster</param>
        void ListenToEvent<T1, T2, T3, T4>(int messageId, Action<T1, T2, T3, T4> callEvent);

        /// <summary>
        /// Broadcast the event by specific message id and trigger calback if there is items which listen to event
        /// </summary>
        /// <param name="messageId">Specific message id for broadcasting</param>
        void BroadcastEvent(int messageId);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1">The generic type of argument which pass to callback</typeparam>
        /// <param name="messageId">Specific message id for broadcasting</param>
        /// <param name="inputValue">The input value which pass to registered callback</param>
        void BroadcastEvent<T1>(int messageId, T1 inputValue);

        /// <summary>
        /// Broadcast the event by specific message id and trigger calback if there is items which listen to event
        /// </summary>
        /// <typeparam name="T1">The generic type of argument which pass to callback</typeparam>
        /// <typeparam name="T2">The generic type of argument which pass to callback</typeparam>
        /// <param name="messageId">Specific message id for broadcasting</param>
        /// <param name="inputValue1">The input value which pass to registered callback</param>
        /// <param name="inputValue2">The input value which pass to registered callback</param>
        void BroadcastEvent<T1, T2>(int messageId, T1 inputValue1, T2 inputValue2);

        /// <summary>
        /// Broadcast the event by specific message id and trigger calback if there is items which listen to event
        /// </summary>
        /// <typeparam name="T1">The generic type of argument which pass to callback</typeparam>
        /// <typeparam name="T2">The generic type of argument which pass to callback</typeparam>
        /// <typeparam name="T3">The generic type of argument which pass to callback</typeparam>
        /// <param name="messageId">Specific message id for broadcasting</param>
        /// <param name="inputValue1">The input value which pass to registered callback</param>
        /// <param name="inputValue2">The input value which pass to registered callback</param>
        /// <param name="inputValue3">The input value which pass to registered callback</param>
        void BroadcastEvent<T1, T2, T3>(int messageId, T1 inputValue1, T2 inputValue2, T3 inputValue3);

        /// <summary>
        /// Broadcast the event by specific message id and trigger calback if there is items which listen to event
        /// </summary>
        /// <typeparam name="T1">The generic type of argument which pass to callback</typeparam>
        /// <typeparam name="T2">The generic type of argument which pass to callback</typeparam>
        /// <typeparam name="T3">The generic type of argument which pass to callback</typeparam>
        /// <typeparam name="T4">The generic type of argument which pass to callback</typeparam>
        /// <param name="messageId">Specific message id for broadcasting</param>
        /// <param name="inputValue1">The input value which pass to registered callback</param>
        /// <param name="inputValue2">The input value which pass to registered callback</param>
        /// <param name="inputValue3">The input value which pass to registered callback</param>
        /// <param name="inputValue4">The input value which pass to registered callback</param>
        void BroadcastEvent<T1, T2, T3, T4>(int messageId, T1 inputValue1, T2 inputValue2, T3 inputValue3, T4 inputValue4);

        /// <summary>
        /// Remove and unregister event from container by specific message id
        /// </summary>
        /// <param name="messageId">Specific message id for removing</param>
        /// <param name="callEvent">The callback which registered by ListenToEvent method</param>
        /// <returns>return true if event by specific id and callback exists in container otherwise return false</returns>
        bool RemoveEventListener(int messageId, Action callEvent);

        /// <summary>
        /// Remove and unregister event from container by specific message id
        /// </summary>
        /// <typeparam name="T1">The generic type of argument which considered for callback</typeparam>
        /// <param name="messageId">Specific message id for removing</param>
        /// <param name="callEvent">The callback which registered by ListenToEvent method</param>
        /// <returns>return true if event by specific id and callback exists in container otherwise return false</returns>
        bool RemoveEventListener<T1>(int messageId, Action<T1> callEvent);

        /// <summary>
        /// Remove and unregister event from container by specific message id
        /// </summary>
        /// <typeparam name="T1">The generic type of argument which considered for callback</typeparam>
        /// <typeparam name="T2">The generic type of argument which considered for callback</typeparam>
        /// <param name="messageId">Specific message id for removing</param>
        /// <param name="callEvent">The callback which registered by ListenToEvent method</param>
        /// <returns>return true if event by specific id and callback exists in container otherwise return false</returns>
        bool RemoveEventListener<T1, T2>(int messageId, Action<T1, T2> callEvent);

        /// <summary>
        /// Remove and unregister event from container by specific message id
        /// </summary>
        /// <typeparam name="T1">The generic type of argument which considered for callback</typeparam>
        /// <typeparam name="T2">The generic type of argument which considered for callback</typeparam>
        /// <typeparam name="T3">The generic type of argument which considered for callback</typeparam>
        /// <param name="messageId">Specific message id for removing</param>
        /// <param name="callEvent">The callback which registered by ListenToEvent method</param>
        /// <returns>return true if event by specific id and callback exists in container otherwise return false</returns>
        bool RemoveEventListener<T1, T2, T3>(int messageId, Action<T1, T2, T3> callEvent);

        /// <summary>
        /// Remove and unregister event from container by specific message id
        /// </summary>
        /// <typeparam name="T1">The generic type of argument which considered for callback</typeparam>
        /// <typeparam name="T2">The generic type of argument which considered for callback</typeparam>
        /// <typeparam name="T3">The generic type of argument which considered for callback</typeparam>
        /// <typeparam name="T4">The generic type of argument which considered for callback</typeparam>
        /// <param name="messageId">Specific message id for removing</param>
        /// <param name="callEvent">The callback which registered by ListenToEvent method</param>
        /// <returns>return true if event by specific id and callback exists in container otherwise return false</returns>
        bool RemoveEventListener<T1, T2, T3, T4>(int messageId, Action<T1, T2, T3, T4> callEvent);

        /// <summary>
        /// Listen to specific and very common message by message id -1000
        /// </summary>
        /// <param name="callEvent">The callback which triggers by event broadcaster</param>
        void ListenToStartupEvent(Action<IServiceProvider> callEvent);

        /// <summary>
        /// Broadcast the event by -1000 message id and trigger calback by passing service provider reference
        /// </summary>
        /// <param name="inputValue">The service provider reference which pass to registered callback</param>
        void BroadcastStartupEvent(IServiceProvider inputValue);
    }
}

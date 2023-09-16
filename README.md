# EventDomain
![GitHub](https://img.shields.io/github/license/svermeulen/Extenject)
## Table Of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
<summory>

  - [Introduction](#introduction)
  - [Features](#features)
  - [Installation](#installation)
  - [Startup](#startup)
  - [How To Use](#how-to-use)
</summory>

## Introduction
This library provides the event registering and broadcasting feature base on message Id, this package is simple impression from observer design pattern. the library implemented fully by C# language and just dependent to standard C# library and ready to use in all .NET environments.

Support platforms: 
* PC/Mac/Linux
* iOS
* Android
* WebGL
* UWP App

```text
* Note: The library may work on other platforms, the source code just used C# code and .net standard version 2.0.
        this library has some feature which use Threading.Task library and it doesn't supported in WebGL.
```

* This library is design to be dependecy injection friendly, the recommended DI library is the [Dependency Injection](https://github.com/Game-Warriors/DependencyInjection-Unity3d) to be used.

This library used in the following games and apps:
</br>
[Street Cafe](https://play.google.com/store/apps/details?id=com.aredstudio.streetcafe.food.cooking.tycoon.restaurant.idle.game.simulation)
</br>
[Idle Family Adventure](https://play.google.com/store/apps/details?id=com.aredstudio.idle.merge.farm.game.idlefarmadventure)
</br>
[CLC BA](https://play.google.com/store/apps/details?id=com.palsmobile.clc)

## Features
* Fully decaple and flexible binding by delegates


## Installation
This library can be added by unity package manager form git repository or could be downloaded.

For more information about how to install a package by unity package manager, please read the manual in this link:
[Install a package from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)

## Startup
After adding package to using the library features, the “EventSystem” class should be created. The “EventSystem” class wraps all system features like listening, broadcasting and removing event from event container.
```csharp
private void Awake()
{
    EventSystem eventSystem = new EventSystem();
}
```
If the dependency injection library is used, the initialization process could be like following sample.
```csharp
private void Awake()
{
    serviceCollection = new ServiceCollection(INIT_METHOD_NAME);
    serviceCollection.AddSingleton<IEvent, EventSystem>();
}
```
There is “Messenger” class Inside the “EventSystem” by generic Implementation which encapsulate core logic of event container. Because the Unity3D AOT compiler cannot generate assembly code of generic scheme of the “IEvent” abstraction and “EventSystem”, it is just implemented by int key, but the implementation of other type of messaging by different type of key like string could done by Messenger class.

## How To Use
There are “IEvent“ abstraction which present system features and the “EventSystem” Implemented that.

* ListenToEvent: The group of methods which use to listen to specific event by id and register the event in event container. the listener method receive callback to trigger when the broadcasting call by specific message Id.

* BroadcastEvent: The group of methods which use to broadcasting to specific event by id, look up event in container and trigger registered actions callback for specific message id.

* RemoveEventListener: The group of method which use to remove and unregister event from container by specific message Id. if the action method by specific id exists in container method return true, otherwise return false to show couldn’t find registered callback.

* ListenToStartupEvent: Listen to specific and very common message by message Id -1000 which use to broadcast on project start up.

* BroadcastStartupEvent: Broadcast specific message by message Id -1000 which broadcast in project after start up. this event hard coded to system because its common may use in all projects.

```csharp
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
    bool RemoveEventListener<T1>(int messageId, Action<T1> callEvent);
    bool RemoveEventListener<T1, T2>(int messageId, Action<T1, T2> callEvent);
    bool RemoveEventListener<T1, T2, T3>(int messageId, Action<T1, T2, T3> callEvent);
    bool RemoveEventListener<T1, T2, T3, T4>(int messageId, Action<T1, T2, T3, T4> callEvent);
    bool RemoveEventListener(int messageId, Action callEvent);

    void ListenToStartupEvent(Action<IServiceProvider> callEvent);
    void BroadcastStartupEvent(IServiceProvider inputValue);
}
```
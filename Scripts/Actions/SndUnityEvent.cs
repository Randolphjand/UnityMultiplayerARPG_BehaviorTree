using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitBehaviorTree;
using MultiplayerARPG.KitBT;
using UnityEngine.Events;

public class SndUnityEvent : ActionNode
{
    public UnityEvent m_MyEvent;
    protected override void OnStart() 
    {
        Debug.Log("  Send events from SendEvent node");
        m_MyEvent?.Invoke();
    }

    protected override void OnStop() 
    {
    }

    protected override State OnUpdate() 
    {
        return State.Success;
    }
}

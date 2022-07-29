using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitBehaviorTree;
using MultiplayerARPG.KitBT;
using UnityEngine.Events;

public class SndUnityAction : ActionNode
{
    public delegate SomeDelegate SomeDelegate();
    public static SomeDelegate OnDelegate;
    public static UnityAction OnAction;
    protected override void OnStart()
    {
        //OnDelegate?.Invoke();
        OnAction.Invoke();
    }
    protected override void OnStop() 
    {
    }

    protected override State OnUpdate() 
    {
        return State.Success;
    }
}

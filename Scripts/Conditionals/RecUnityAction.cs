using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitBehaviorTree;
using MultiplayerARPG.KitBT;

//This waits for the event and then stops running.
public class RecUnityAction : ConditionalNode
{
    bool eventReceived;
    protected override void OnStart()
    {
        if (eventReceived != true)
        {
            eventReceived = false;
        }
        SndUnityAction.OnAction += DoWhenEventReceived;
    }
    void DoWhenEventReceived()
    {
        eventReceived = true;
    }
    protected override void OnStop()
    {
    }
    private void OnDestroy()
    {
        SndUnityAction.OnAction -= DoWhenEventReceived;
    }
    protected override State OnUpdate()
    {
        if (!eventReceived)
        {
            return child.Update();
        }
        else
        {
            return State.Failure;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitBehaviorTree;
using MultiplayerARPG.KitBT;
using UnityEngine.Events;
using System;

//This waits for the event and then stops running.

public class RecUnityEvent : ConditionalNode
{
    bool someCondition;
    protected override void OnStart()
    {
        if (someCondition != true)
        {
            someCondition = false;
        }
    }
    public void SetCondition()
    {
        Debug.Log("  RecUnityEvent someCondition " + someCondition);
        someCondition = true;
        Debug.Log("  someCondition has been set to true.  " + someCondition);
    }
    protected override void OnStop()
    {
    }
    protected override State OnUpdate()
    {
        if (!someCondition)
        {
            Debug.Log("  RecUnityEvent Returning child.Update()" + someCondition);
            return child.Update();

        }
        else
        {
            Debug.Log("  RecUnityEvent Returning state failure" + someCondition);
            return State.Failure;
        }

        //return !someCondition ? child.Update() : State.Failure;
    }
}

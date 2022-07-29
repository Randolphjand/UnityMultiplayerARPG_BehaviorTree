using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiplayerARPG.KitBT;

//Similar to Selector, but children are constantly
//reevaluated each tick. If a child with higher priority
//succeeds, the current running child is aborted.
//Success = When all children return success.
//Failure = When one child returns failure.
//Can be used to create Utility AI

namespace KitBehaviorTree
{
    public class InterruptSelector : Selector
    {
        protected override State OnUpdate()
        {
            int previous = current;
            base.OnStart();
            var status = base.OnUpdate();
            if (previous != current)
            {
                if (children[previous].state == State.Running)
                {
                    children[previous].Abort();
                }
            }

            return status;
        }
    }
}
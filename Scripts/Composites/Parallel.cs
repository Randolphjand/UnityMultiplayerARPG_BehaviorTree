using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Executes all children 'at once' concurrently. Multiple
//children can be in the running state at the same time.
//Success = When all children return success.
//Failure = When one child returns failure. Remaining children are aborted.

namespace KitBehaviorTree
{
    public class Parallel : CompositeNode
    {
        List<State> childrenLeftToExecute = new List<State>();

        protected override void OnStart()
        {
            childrenLeftToExecute.Clear();
            children.ForEach(a =>
            {
                childrenLeftToExecute.Add(State.Running);
            });
        }
        protected override void OnStop()
        {
        }
        protected override State OnUpdate()
        {
            bool stillRunning = false;
            for (int i = 0; i < childrenLeftToExecute.Count(); ++i)
            {
                if (childrenLeftToExecute[i] == State.Running)
                {
                    var status = children[i].Update();
                    if (status == State.Failure)
                    {
                        AbortRunningChildren();
                        return State.Failure;
                    }

                    if (status == State.Running)
                    {
                        stillRunning = true;
                    }

                    childrenLeftToExecute[i] = status;
                }
            }

            return stillRunning ? State.Running : State.Success;
        }
        void AbortRunningChildren()
        {
            for (int i = 0; i < childrenLeftToExecute.Count(); ++i)
            {
                if (childrenLeftToExecute[i] == State.Running)
                {
                    children[i].Abort();
                }
            }
        }
    }
}
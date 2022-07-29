using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Selector Executes children one at a time left to right
//Success = When one child returns success
//Failure = When all children return failure

namespace KitBehaviorTree
{
    public class Selector : CompositeNode
    {
        protected int current;

        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            for (int i = current; i < children.Count; ++i)
            {
                current = i;
                var child = children[current];

                switch (child.Update())
                {
                    case State.Running:
                        return State.Running;
                    case State.Success:
                        return State.Success;
                    case State.Failure:
                        continue;
                    default:
                        break;
                }
            }

            return State.Failure;
        }
    }
}
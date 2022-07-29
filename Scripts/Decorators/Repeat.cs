using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If it's child fails, it will try and again, and return Running to it's parent.

namespace KitBehaviorTree
{
    public class Repeat : DecoratorNode
    {
        public bool restartOnSuccess = true;
        public bool restartOnFailure = false;

        protected override void OnStart()
        {

        }
        protected override void OnStop()
        {

        }
        protected override State OnUpdate()
        {
            switch (child.Update())
            {
                case State.Running:
                    break;
                case State.Failure:
                    if (restartOnFailure)
                    {
                        return State.Running;
                    }
                    else
                    {
                        return State.Failure;
                    }
                case State.Success:
                    if (restartOnSuccess)
                    {
                        return State.Running;
                    }
                    else
                    {
                        return State.Success;
                    }
            }
            return State.Running;
        }
    }


}

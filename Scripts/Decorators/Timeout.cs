using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Timeout - Aborts it's child after the chosen time duration even if it hasn't finished.

namespace KitBehaviorTree
{
    public class Timeout : DecoratorNode
    {
        public float duration = 1.0f;
        float startTime;

        protected override void OnStart()
        {
            startTime = Time.time;
        }
        protected override void OnStop()
        {
        }
        protected override State OnUpdate()
        {
            if (Time.time - startTime > duration)
            {
                return State.Failure;
            }

            return child.Update();
        }
    }
}
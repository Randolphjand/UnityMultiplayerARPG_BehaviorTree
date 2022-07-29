using UnityEngine;
using KitBehaviorTree;

namespace MultiplayerARPG.KitBT
{
    public class RandomWait : ActionNode
    {
        public float durationMin = 2;
        public float durationMax = 4;
        float startTime;
        float duration;

        protected override void OnStart()
        {
            startTime = Time.time;
            duration = Random.Range(durationMin, durationMax);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Time.time - startTime > duration)
            {
                return State.Success;
            }
            return State.Running;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Randomly selects one child to execute.
//Success = When the child returns success.
//Failure = When the child returns failure.

namespace KitBehaviorTree
{
    public class RandomSelector : CompositeNode
    {
        protected int current;

        protected override void OnStart()
        {
            current = Random.Range(0, children.Count);
        }
        protected override void OnStop()
        {
        }
        protected override State OnUpdate()
        {
            var child = children[current];
            return child.Update();
        }
    }
}
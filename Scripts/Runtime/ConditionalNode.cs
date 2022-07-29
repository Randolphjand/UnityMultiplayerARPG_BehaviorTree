using System.Collections.Generic;
using UnityEngine;

namespace KitBehaviorTree
{
    public abstract class ConditionalNode : Node
    {
        [HideInInspector] public Node child;

        public override Node Clone()
        {
            ConditionalNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}
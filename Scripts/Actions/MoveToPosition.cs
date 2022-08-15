using UnityEngine;

namespace MultiplayerARPG.KitBT
{
    public class MoveToPosition : ActionNode
    {
        public float tolerance = 1.0f;
        public ExtraMovementState extraMovementState = ExtraMovementState.None;

        protected override void OnStart()
        {
            Entity.SetTargetEntity(null);  //basecharacterentity
            Entity.SetExtraMovementState(extraMovementState);  //basegameentity
            Entity.PointClickMovement(blackboard.moveToPosition);  //basegameentity
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Vector3.Distance(Entity.EntityTransform.position, blackboard.moveToPosition) < tolerance)  //CacheTransform basegameentity
            {
                return State.Success;
            }
            return State.Running;
        }
    }
}

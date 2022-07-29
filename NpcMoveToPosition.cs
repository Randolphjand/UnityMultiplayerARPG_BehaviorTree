using UnityEngine;
using MultiplayerARPG.KitBT;
using MultiplayerARPG;
using UnityEngine.AI;

public class NpcMoveToPosition : ActionNode
{
    private Vector3 newLocation;
    private GameObject objectToMove;
    //private MovementSpeeds movementSpeed;
    private NavMeshAgent movingAgent;
    //private NpcEntityExtension npcEntity;
    private NpcEntity npcEntity;
    private bool isMoving;
    private Vector3 destination;
    //private float destinationReachedThreshold = 0.2f;
    public float tolerance = 1.0f;
    public ExtraMovementState extraMovementState = ExtraMovementState.None;
    
    private NavMeshEntityMovement NavMeshEntityMovement;

    //public void DoActionMoveToVector()
    //{
    //    if (movingAgent == null || npcEntity == null) return State.Failure;


        
    //}
    protected override void OnStart() 
    {
        Entity.SetTargetEntity(null);
        Entity.SetExtraMovementState(extraMovementState);
        Entity.PointClickMovement(blackboard.moveToPosition);
        
    }

    protected override void OnStop() 
    {
    }

    protected override State OnUpdate() 
    {
        //if (!isMoving)
        //{
        //    // Disabled the SnapTpGround for the move since this requires your terrain to have a TAG ("Terrain") and this is just an extra requirement
        //    //m_destination = VectorMathsTools.SnapToGround(m_objectToMove.transform, newLocation.Value);

        //    // updated with Kit snap to terrain code
        //    npcEntity.FindGroundedPosition(m_newLocation, 20, out destination);

        //    npcEntity.StopMove();

        //    npcEntity.PointClickMovement(destination);

        //    isMoving = true;
        //}
        //else
        //{
        //    // did we get there yet?
        //    float distanceToTarget = Vector3.Distance(objectToMove.transform.position, destination);
        //    if (distanceToTarget < destinationReachedTreshold)
        //    {
        //        isMoving = false;
        //        return State.Success;
        //    }
        //}
        //return State.Running;
        //return State.Success;
        if (!isMoving) // ??
        {
            npcEntity.FindGroundedPosition(newLocation, 20, out destination);
            npcEntity.StopMove();
            npcEntity.PointClickMovement(destination);
            isMoving = true;
        }
        else
        {
            float distanceToTarget = Vector3.Distance(objectToMove.transform.position, destination);

            //if(distanceToTarget < destinationReachedThreshold)
            //{
            //    isMoving = false;
            //    return State.Success;
            //}
            if (Vector3.Distance(npcEntity.CacheTransform.position, blackboard.moveToPosition) < tolerance)
            {
                isMoving = false;
                return State.Success;
            }
        }
        ////if (Vector3.Distance(Entity.CacheTransform.position, blackboard.moveToPosition) < tolerance)
        //if (Vector3.Distance(npcEntity.CacheTransform.position, blackboard.moveToPosition) < tolerance)
        //{
        //    return State.Success;
        //}
        return State.Running;
    }
}

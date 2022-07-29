using UnityEngine;

namespace MultiplayerARPG.KitBT
{
    public class TurnToEnemy : ActionNode
    {
        [Tooltip("Turn to enemy speed")]
        public float turnToEnemySpeed = 800f;
        [Min(1f)]
        public float successAngle = 10f;

        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            IDamageableEntity tempTargetEnemy;
            if (!Entity.TryGetTargetEntity(out tempTargetEnemy) || Entity.Characteristic == MonsterCharacteristic.NoHarm)
            {
                // No target, stop attacking
                ClearActionState();
                return State.Failure;
            }

            if (tempTargetEnemy.Entity == Entity.Entity || tempTargetEnemy.IsHideOrDead() || !tempTargetEnemy.CanReceiveDamageFrom(Entity.GetInfo()))
            {
                // If target is dead or in safe area stop attacking
                Entity.SetTargetEntity(null);
                ClearActionState();
                return State.Failure;
            }

            Vector3 currentPosition = Entity.CacheTransform.position;
            Vector3 targetPosition = tempTargetEnemy.GetTransform().position;
            Vector3 lookAtDirection = (targetPosition - currentPosition).normalized;
            if (lookAtDirection.sqrMagnitude > 0)
            {
                if (CurrentGameInstance.DimensionType == DimensionType.Dimension3D)
                {
                    Quaternion currentLookAtRotation = Entity.GetLookRotation();
                    Vector3 lookRotationEuler = Quaternion.LookRotation(lookAtDirection).eulerAngles;
                    lookRotationEuler.x = 0;
                    lookRotationEuler.z = 0;
                    Quaternion nextLookAtRotation = Quaternion.RotateTowards(currentLookAtRotation, Quaternion.Euler(lookRotationEuler), turnToEnemySpeed * Time.deltaTime);
                    Entity.SetLookRotation(nextLookAtRotation);

                    if (Quaternion.Angle(currentLookAtRotation, nextLookAtRotation) >= successAngle)
                        return State.Running;
                }
                else
                {
                    // Update 2D direction
                    Entity.SetLookRotation(Quaternion.LookRotation(lookAtDirection));
                    return State.Success;
                }
            }
            return State.Success;
        }
    }
}

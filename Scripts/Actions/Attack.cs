using UnityEngine;
using KitBehaviorTree;

namespace MultiplayerARPG.KitBT
{
    public class Attack : ActionNode
    {
        private bool didAction;

        protected override void OnStart()
        {
            didAction = false;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (didAction && Entity.IsPlayingActionAnimation())
            {
                ClearActionState();
                return State.Success;
            }

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

            Entity.AimPosition = Entity.GetAttackAimPosition(ref blackboard.isLeftHandAttacking);
            if (Entity.IsPlayingActionAnimation())
            {
                return State.Running;
            }

            if (blackboard.queueSkill != null && Entity.IndexOfSkillUsage(blackboard.queueSkill.DataId, SkillUsageType.Skill) < 0)
            {
                // Use skill when there is queue skill or randomed skill that can be used
                Entity.UseSkill(blackboard.queueSkill.DataId, false, 0, new AimPosition()
                {
                    type = AimPositionType.Position,
                    position = tempTargetEnemy.OpponentAimTransform.position,
                });
            }
            else
            {
                // Attack when no queue skill
                Entity.Attack(false);
            }

            didAction = true;
            return State.Running;
        }
    }
}

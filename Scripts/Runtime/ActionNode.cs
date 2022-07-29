using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitBehaviorTree;


namespace MultiplayerARPG.KitBT
{
    public abstract class ActionNode : Node
    {
        public BaseMonsterCharacterEntity Entity { get { return blackboard.activityComp.Entity; } }
        public MonsterCharacter CharacterDatabase { get { return blackboard.activityComp.CharacterDatabase; } }
        public GameInstance CurrentGameInstance { get { return GameInstance.Singleton; } }

        protected void ClearActionState()
        {
            blackboard.queueSkill = null;
            blackboard.isLeftHandAttacking = false;
        }

        protected Transform GetDamageTransform()
        {
            return blackboard.queueSkill != null ? blackboard.queueSkill.GetApplyTransform(Entity, blackboard.isLeftHandAttacking) :
                Entity.GetWeaponDamageInfo(null).GetDamageTransform(Entity, blackboard.isLeftHandAttacking);
        }

        protected float GetAttackDistance()
        {
            return blackboard.queueSkill != null && blackboard.queueSkill.IsAttack ? blackboard.queueSkill.GetCastDistance(Entity, blackboard.queueSkillLevel, blackboard.isLeftHandAttacking) :
                Entity.GetAttackDistance(blackboard.isLeftHandAttacking);
        }

        protected bool OverlappedEntity<T>(T entity, Vector3 measuringPosition, Vector3 targetPosition, float distance)
            where T : BaseGameEntity
        {
            if (Vector3.Distance(measuringPosition, targetPosition) <= distance)
                return true;
            // Target is far from controlling entity, try overlap the entity
            return Entity.FindPhysicFunctions.IsGameEntityInDistance(entity, measuringPosition, distance, false);
        }

    }
}
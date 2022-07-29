using MultiplayerARPG;
using MultiplayerARPG.KitBT;
using System.Collections.Generic;
using UnityEngine;

namespace KitBehaviorTree
{

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public partial class Blackboard
    {
        public Vector3 moveToPosition;
        public MonsterActivityComponentKitBT activityComp;
        public BaseSkill queueSkill;
        public short queueSkillLevel;
        public bool isLeftHandAttacking;
        public List<BaseCharacterEntity> enemies = new List<BaseCharacterEntity>();
    }
}
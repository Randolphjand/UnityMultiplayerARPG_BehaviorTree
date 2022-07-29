using KitBehaviorTree;
using UnityEngine;

namespace MultiplayerARPG.KitBT
{
    [DefaultExecutionOrder(int.MaxValue)]
    [RequireComponent(typeof(BehaviourTreeRunner))]
    public class MonsterActivityComponentKitBT : BaseMonsterActivityComponent
    {
        private BehaviourTreeRunner runner;

        private void Start()
        {
            runner = GetComponent<BehaviourTreeRunner>();
            runner.tree.blackboard.activityComp = this;
            if (!Entity.IsServer)
                runner.enabled = false;
        }

        private void Update()
        {
            if (!Entity.IsServer || Entity.Identity.CountSubscribers() == 0 || CharacterDatabase == null)
            {
                runner.enabled = false;
                return;
            }
            runner.enabled = true;
        }
    }
}

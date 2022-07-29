
using KitBehaviorTree.Utilities;
using KitBehaviorTree.Utilities.Editor;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class MonsterCharacterEntity
    {
        [Header("Combat BT AI Settings")]
        [Tooltip("Do we want the Monster Character Entity to remember previous attackers? (Useful for boss monsters)")]
        public bool rememberAttackers = false;

        [Tooltip("Do we want the Monster Character Entity support KIT to Behaviour Tree AI Messages? (For example damage recieved)")]
        public bool enableBTAIMessage = true;        

        [Tooltip("Do we want this Monster to communicate it's actions to others?")]
        public bool sendCommunications = false;

        [Tooltip("Do we want this Monster to recieve communications and pass to Behavior Tree?")]
        public bool recieveCommunications = false;

	    [Tooltip("How far should radio messages be transmitted between entities?")]
	    public float radioDistance = 100;

	    [Header("Combat BT AI Formation Systems")]
	    [Tooltip("Is this Monster Entity a LEADER of other entities?")]
	    public bool isLeader = false;

	    [Tooltip("How many followers can this leader control?")]
	    public int followerLimit = 5;

	    [Tooltip("List of current followers")]
	    #if UNITY_EDITOR 
	    	[ReadOnly] 
	    #endif
	    public List<MonsterCharacterEntity> followers;
		
	    [Tooltip("Link to the current leader (if any)")]
	    #if UNITY_EDITOR 
	    	[ReadOnly] 
	    #endif
	    public MonsterCharacterEntity currentLeader;		

	    [Tooltip("Link to the previous leader (if any)")]
	    #if UNITY_EDITOR 
	    	[ReadOnly] 
	    #endif
	    public MonsterCharacterEntity previousLeader;		

        private Dictionary<long, long> attackers;
	    private MonsterCharacterEntity monsterEntity;
	    
#if BEHAVIOR_DESIGNER
	    private BehaviorTree mobBehaviourTree;
#endif

        [DevExtMethods("Awake")]
        protected void DevExtAwakeAIMemory()
        {
            attackers = new Dictionary<long, long>();

	        followers = new List<MonsterCharacterEntity>();

#if BEHAVIOR_DESIGNER
            mobBehaviourTree = gameObject.GetComponent<BehaviorTree>();
#endif	       

            monsterEntity = gameObject.GetComponent(typeof(MonsterCharacterEntity)) as MonsterCharacterEntity;	        

	        //onReceivedDamage += DevExtReceivedDamageAIMemory;
            onDead.AddListener(DevExtOnDead);

            if (recieveCommunications)
            {
                KitEntityEvents.Instance.OnAttacking += Instance_OnAttacking;
                KitEntityEvents.Instance.OnFleeing += Instance_OnFleeing;
                KitEntityEvents.Instance.OnDied += Instance_OnDied;
                KitEntityEvents.Instance.OnGoingHome += Instance_OnGoingHome;
                KitEntityEvents.Instance.OnWaiting += Instance_OnWaiting;
                KitEntityEvents.Instance.OnNeedHealing += Instance_OnNeedHealing;
                KitEntityEvents.Instance.OnNeedBuffing += Instance_OnNeedBuffing;
                KitEntityEvents.Instance.OnNeedHelp += Instance_OnNeedHelp;
            }

        }

        [DevExtMethods("OnDestroy")]
        protected void DevExtOnDestroyAIMemory()
        {
            //onReceivedDamage -= DevExtReceivedDamageAIMemory;
            onDead.RemoveListener(DevExtOnDead);
        }

#region Monster events passed to BT Events
        private void Instance_OnNeedHelp(object param1, object param2, object param3)
        {
            if (notMyMessage(param1))
            {
#if BEHAVIOR_DESIGNER
	            if (mobBehaviourTree != null) mobBehaviourTree.SendEvent<object, object>("MonsterNeedHelp", param2, param3);
#endif
            }
        }

        private void Instance_OnNeedBuffing(object param1, object param2, object param3)
        {
            if (notMyMessage(param1))
            {
#if BEHAVIOR_DESIGNER
                if (mobBehaviourTree != null) mobBehaviourTree.SendEvent<object, object>("MonsterNeedBuffing", param2, param3);
#endif
            }
        }

        private void Instance_OnNeedHealing(object param1, object param2, object param3)
        {
            if (notMyMessage(param1))
            {
#if BEHAVIOR_DESIGNER
                if (mobBehaviourTree != null) mobBehaviourTree.SendEvent<object, object>("MonsterNeedHealing", param2, param3);
#endif
            }
        }

        private void Instance_OnWaiting(object param1, object param2, object param3)
        {
            if (notMyMessage(param1))
            {
#if BEHAVIOR_DESIGNER
                if (mobBehaviourTree != null) mobBehaviourTree.SendEvent<object, object>("MonsterWaiting", param2, param3);
#endif
            }
        }

        private void Instance_OnGoingHome(object param1, object param2, object param3)
        {
            if (notMyMessage(param1))
            {
#if BEHAVIOR_DESIGNER
                if (mobBehaviourTree != null) mobBehaviourTree.SendEvent<object, object>("MonsterGoingHome", param2, param3);
#endif
            }
        }

        private void Instance_OnDied(object param1, object param2, object param3)
        {
            if (notMyMessage(param1))
            {
#if BEHAVIOR_DESIGNER
                if (mobBehaviourTree != null) mobBehaviourTree.SendEvent<object, object>("MonsterDied", param2, param3);
#endif
            }
        }

        private void Instance_OnFleeing(object param1, object param2, object param3)
        {
            if (notMyMessage(param1))
            {
#if BEHAVIOR_DESIGNER
                if (mobBehaviourTree != null) mobBehaviourTree.SendEvent<object, object>("MonsterFleeing", param2, param3);
#endif
            }
        }

        private void Instance_OnAttacking(object param1, object param2, object param3)
        {
            if (notMyMessage(param1))
            {
#if BEHAVIOR_DESIGNER
	            if (mobBehaviourTree != null) mobBehaviourTree.SendEvent<object, object>("MonsterAttacking", param2, param3);
#endif
            }
        }

        private bool notMyMessage(object param1)
        {
            uint checkID = this.GetObjectId();
            if (param1 != null && (param1 as MonsterCharacterEntity).GetObjectId() != checkID)
            {
                // this message was not sent by ME so I should respond to it if I can (and want to)
                // also confirm we are within required distance
                return Vector3.Distance(this.transform.position, (param1 as MonsterCharacterEntity).transform.position) < radioDistance;
            }
	        return false; // I sent this message, I shouldn't respond to my own message
        }
#endregion

	    public void RemoveFromFormation(MonsterCharacterEntity removeMonster)
	    {
            if (followers == null) followers = new List<MonsterCharacterEntity>();
	    	if (followers.Count > 0)
	    	{
			    try
			    {
				    followers.RemoveAll(r => r.GetObjectId() == removeMonster.GetObjectId());
			    }
			    catch { }
	    	}
	    }
	    
#region MMORPG KIT EVENTS to BT Events
        protected void DevExtOnDead()
        {
            // once we die we better clear any connection we have with the list of players
            if (isLeader)
            {
                if (followers == null) followers = new List<MonsterCharacterEntity>();
                if (followers.Count > 0)
                {
                    foreach (MonsterCharacterEntity mons in followers)
                    {
                        try
                        {
                            mons.previousLeader = mons.currentLeader;
                            mons.currentLeader = null;
                        }
                        catch { }
                    }
                }
                followers.Clear();
                isLeader = false;
            }
            else
            {
                if (currentLeader != null) currentLeader.RemoveFromFormation(this);
            }

            previousLeader = currentLeader;
            currentLeader = null;
        }

	    protected void DevExtReceivedDamageAIMemory(HitBoxPosition position,
		    Vector3 fromPosition,
		    IGameEntity attacker,
		    CombatAmountType combatAmountType,
		    int totalDamage,
		    CharacterItem weapon,
		    BaseSkill skill,
		    short skillLevel)
        {
#if BEHAVIOR_DESIGNER
	        if (enableBTAIMessage && mobBehaviourTree != null)
	        {                
		        mobBehaviourTree.SendEvent<object>("MonsterRecievedDamage", (object)attacker.GetObjectId());
            }
#endif
          
            if (rememberAttackers)
            {
                if (attackers.ContainsKey(attacker.GetObjectId()))
                {
                    // add damage to our little list
                    attackers[attacker.GetObjectId()] += totalDamage;
                }
                else
                    attackers.Add(attacker.GetObjectId(), totalDamage);
            }
        }
        #endregion

        #region Get functions for memory routines
        public BasePlayerCharacterEntity GetHighestDamageDamager()
        {
            if (!rememberAttackers) return null;

            List<BasePlayerCharacterEntity> AllCharacters = monsterEntity.FindAliveCharacters<BasePlayerCharacterEntity>(
                monsterEntity.CharacterDatabase.VisualRange * 4,
                false, /* Don't find an allies */
                true,  /* Always find an enemies */
                monsterEntity.IsSummoned && (monsterEntity.CharacterDatabase.Characteristic == MonsterCharacteristic.Aggressive) /* Find enemy while summoned and aggresively */);

            //List<BasePlayerCharacterEntity> AllCharacters = new List<BasePlayerCharacterEntity>(CurrentGameManager.GetPlayerCharacters());

            long currentHighDamage = 0;
            BasePlayerCharacterEntity result = null;
            foreach (KeyValuePair<long, long> entry in attackers)
            {
                if (entry.Value > currentHighDamage)
                {
                    // is this a player? Get them if we can!
                    foreach (BasePlayerCharacterEntity character in AllCharacters)
                    {
                        if (character.GetObjectId() == entry.Key)
                        {
                            result = character;
                            currentHighDamage = entry.Value;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public BasePlayerCharacterEntity GetLowestDamageDamager()
        {
            if (!rememberAttackers) return null;

            List<BasePlayerCharacterEntity> AllCharacters = monsterEntity.FindAliveCharacters<BasePlayerCharacterEntity>(
                monsterEntity.CharacterDatabase.VisualRange * 4,
                false, /* Don't find an allies */
                true,  /* Always find an enemies */
                monsterEntity.IsSummoned && (monsterEntity.CharacterDatabase.Characteristic == MonsterCharacteristic.Aggressive) /* Find enemy while summoned and aggresively */);


            //List<BasePlayerCharacterEntity> AllCharacters = new List<BasePlayerCharacterEntity>(CurrentGameManager.GetPlayerCharacters());

            long currentLowDamage = long.MaxValue;
            BasePlayerCharacterEntity result = null;
            foreach (KeyValuePair<long, long> entry in attackers)
            {
                if (entry.Value < currentLowDamage)
                {
                    // is this a player? Get them if we can!
                    foreach (BasePlayerCharacterEntity character in AllCharacters)
                    {
                        if (character.GetObjectId() == entry.Key)
                        {
                            result = character;
                            currentLowDamage = entry.Value;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public BasePlayerCharacterEntity GetHighestLevelDamager()
        {
            if (!rememberAttackers) return null;

            List<BasePlayerCharacterEntity> AllCharacters = monsterEntity.FindAliveCharacters<BasePlayerCharacterEntity>(
                monsterEntity.CharacterDatabase.VisualRange * 4,
                false, /* Don't find an allies */
                true,  /* Always find an enemies */
                monsterEntity.IsSummoned && (monsterEntity.CharacterDatabase.Characteristic == MonsterCharacteristic.Aggressive) /* Find enemy while summoned and aggresively */);

            //List<BasePlayerCharacterEntity> AllCharacters = new List<BasePlayerCharacterEntity>(CurrentGameManager.GetPlayerCharacters());

            int currentHighestLevel = 0;
            BasePlayerCharacterEntity result = null;
            foreach (KeyValuePair<long, long> entry in attackers)
            {
                // is this a player? Get them if we can!
                foreach (BasePlayerCharacterEntity character in AllCharacters)
                {
                    if (character.GetObjectId() == entry.Key && character.Level > currentHighestLevel)
                    {
                        result = character;
                        currentHighestLevel = character.Level;
                        break;
                    }
                }
            }
            return result;
        }

        public BasePlayerCharacterEntity GetLowestLevelDamager()
        {
            if (!rememberAttackers) return null;

            List<BasePlayerCharacterEntity> AllCharacters = monsterEntity.FindAliveCharacters<BasePlayerCharacterEntity>(
                monsterEntity.CharacterDatabase.VisualRange * 4,
                false, /* Don't find an allies */
                true,  /* Always find an enemies */
                monsterEntity.IsSummoned && (monsterEntity.CharacterDatabase.Characteristic == MonsterCharacteristic.Aggressive) /* Find enemy while summoned and aggresively */);

//            List<BasePlayerCharacterEntity> AllCharacters = new List<BasePlayerCharacterEntity>(CurrentGameManager.GetPlayerCharacters());

            int currentLowestLevel = int.MaxValue;
            BasePlayerCharacterEntity result = null;
            foreach (KeyValuePair<long, long> entry in attackers)
            {
                // is this a player? Get them if we can!
                foreach (BasePlayerCharacterEntity character in AllCharacters)
                {
                    if (character.GetObjectId() == entry.Key && character.Level < currentLowestLevel)
                    {
                        result = character;
                        currentLowestLevel = character.Level;
                        break;
                    }
                }
            }
            return result;
        }
#endregion
    }
}

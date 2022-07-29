using MultiplayerARPG;
using System.Collections.Generic;
using UnityEngine;

#if BEHAVIOR_DESIGNER
using BehaviorDesigner.Runtime;

#region Behavior Designer Wrappers
[System.Serializable]
public class SharedSkill : SharedVariable<Skill>
{
    public static implicit operator SharedSkill(Skill value) { return new SharedSkill { Value = value }; }
}

[System.Serializable]
public class SharedNpcEntity : SharedVariable<NpcEntity>
{
    public static implicit operator SharedNpcEntity(NpcEntity value) { return new SharedNpcEntity { Value = value }; }
}

[System.Serializable]
public class SharedCharacterEntity : SharedVariable<BaseCharacterEntity>
{
	public static implicit operator SharedCharacterEntity(BaseCharacterEntity value) { return new SharedCharacterEntity { Value = value }; }
}

//[System.Serializable]
//public class SharedPlayerEntity : SharedVariable<BasePlayerCharacterEntity>
//{
//    public static implicit operator SharedPlayerEntity(BasePlayerCharacterEntity value) { return new SharedPlayerEntity { Value = value }; }
//}

[System.Serializable]
public class SharedMonsterEntity : SharedVariable<BaseMonsterCharacterEntity>
{
    public static implicit operator SharedMonsterEntity(BaseMonsterCharacterEntity value) { return new SharedMonsterEntity { Value = value }; }
}

[System.Serializable]
public class SharedItem : SharedVariable<Item>
{
    public static implicit operator SharedItem(Item value) { return new SharedItem { Value = value }; }
}

[System.Serializable]
public class SharedAIBraveryState : SharedVariable<AIBraveryState>
{
    public static implicit operator SharedAIBraveryState(AIBraveryState value) { return new SharedAIBraveryState { Value = value }; }
}

[System.Serializable]
public class SharedAIActivityState : SharedVariable<AIActivityState>
{
    public static implicit operator SharedAIActivityState(AIActivityState value) { return new SharedAIActivityState { Value = value }; }
}

[System.Serializable]
public class SharedMoraleList : SharedVariable<List<MoraleList>>
{
    public static implicit operator SharedMoraleList(List<MoraleList> value) { return new SharedMoraleList { mValue = value }; }
}

[System.Serializable]
public class SharedAudioClipList : SharedVariable<List<AudioClip>>
{
	public static implicit operator SharedAudioClipList(List<AudioClip> value) { return new SharedAudioClipList { mValue = value }; }
}


[System.Serializable]
public class SharedTargetSelectionTypes : SharedVariable<TargetSelectionTypes>
{
    public static implicit operator SharedTargetSelectionTypes(TargetSelectionTypes value) { return new SharedTargetSelectionTypes { mValue = value }; }
}

[System.Serializable]
public class SharedDetectionType : SharedVariable<DetectionType>
{
    public static implicit operator SharedDetectionType(DetectionType value) { return new SharedDetectionType { mValue = value }; }
}

[System.Serializable]
public class SharedDistanceCheckType : SharedVariable<DistanceCheckType>
{
    public static implicit operator SharedDistanceCheckType(DistanceCheckType value) { return new SharedDistanceCheckType { mValue = value }; }
}

[System.Serializable]
public class SharedEnemySearchType : SharedVariable<EnemySearchType>
{
	public static implicit operator SharedEnemySearchType(EnemySearchType value) { return new SharedEnemySearchType { mValue = value }; }
}

[System.Serializable]
public class SharedMonsterAttackRangeType : SharedVariable<MonsterAttackRangeType>
{
	public static implicit operator SharedMonsterAttackRangeType(MonsterAttackRangeType value) { return new SharedMonsterAttackRangeType { mValue = value }; }
}

[System.Serializable]
public class SharedRelocationType : SharedVariable<RelocationType>
{
	public static implicit operator SharedRelocationType(RelocationType value) { return new SharedRelocationType { mValue = value }; }
}

[System.Serializable]
public class SharedRadioMessageType : SharedVariable<RadioMessageType>
{
    public static implicit operator SharedRadioMessageType(RadioMessageType value) { return new SharedRadioMessageType { mValue = value }; }
}

[System.Serializable]
public class SharedMonsterVariableState : SharedVariable<MonsterVariableState>
{
	public static implicit operator SharedMonsterVariableState(MonsterVariableState value) { return new SharedMonsterVariableState { mValue = value }; }
}


[System.Serializable]
public class SharedMonsterMaths : SharedVariable<MonsterMaths>
{
	public static implicit operator SharedMonsterMaths(MonsterMaths value) { return new SharedMonsterMaths { mValue = value }; }
}


[System.Serializable]
public class SharedMovementSpeeds : SharedVariable<MovementSpeeds>
{
	public static implicit operator SharedMovementSpeeds(MovementSpeeds value) { return new SharedMovementSpeeds { mValue = value }; }
}


[System.Serializable]
public class SharedMonsterCharacteristic : SharedVariable<MonsterCharacteristic>
{
	public static implicit operator SharedMonsterCharacteristic(MonsterCharacteristic value) { return new SharedMonsterCharacteristic { mValue = value }; }
}

[System.Serializable]
public class SharedFormationStyle : SharedVariable<FormationStyle>
{
	public static implicit operator SharedFormationStyle(FormationStyle value) { return new SharedFormationStyle { mValue = value }; }
}

[System.Serializable]
public class SharedTargetAngle : SharedVariable<TargetAngle>
{
	public static implicit operator SharedTargetAngle(TargetAngle value) { return new SharedTargetAngle { mValue = value }; }
}

#endregion

#endif

#region Behavior Tree Combat AI Classes and ENUMS
[System.Serializable]
public enum DetectionType
{
    See,
    Hear
}

[System.Serializable]
public enum EnemySearchType
{
	BelongToAllyGroup,
	DoNotBelongToAllyGroup,
	Anyone
}

[System.Serializable]
public enum DistanceCheckType
{
    First,
    Closest,
    Furthest,
    Random
}

[System.Serializable]
public enum RelocationType
{
    KeepBehind,
    KeepAway,
}

[System.Serializable]
public enum MonsterAttackRangeType
{
    Melee,
    Range,
}

[System.Serializable]
public enum AIBraveryState
{
    Brave,
    Cautious,
    Cowardly,
    Steadfast
}

[System.Serializable]
public enum RadioMessageType
{
    Attacking,
    Died,
    Fleeing,
    Waiting,
    GoingHome,
    NeedHealing,
    NeedBuffing,
    NeedHelp,
}

[System.Serializable]
public enum TargetSelectionTypes
{
    Random,
    RandomClosest,
    RandomFurthest,
    RandomAttacker,
    HighestLevelAttacker,
    LowestLevelAttacker,
    HighestDamageAttacker,
    LowestDamageAttacker
}

[System.Serializable]
public class MoraleList
{
    public float PercentHPFleeTrigger = 50f;
    public float PercentChanceToFlee = 50f;
    [HideInInspector]
    public bool PassedCheck = false;
}

[System.Serializable]
public enum AIActivityState
{
    Idle,
    Patrolling,
    Attacking,
    Fleeing,
    Homeseeking,
    Relocating
}

[System.Serializable]
public enum MonsterVariableState
{
	HP,
	Mana,
	Stamina,	
}

[System.Serializable]
public enum MonsterMaths
{
	LessThan,
	LessThanPercent,
	GreaterThan,	
	GreaterThanPercent,	
}

[System.Serializable]
public enum MovementSpeeds
{
	NoneSet,
	Walking,
	Sprinting,	
	Crouching,	
	Crawling,
}

[System.Serializable]
public enum FormationStyle
{
	Circle,
	Arc,
	Square,
	Mob,
}


[System.Serializable]
public enum TargetAngle
{
	Front,
	Right,
	Behind,
	Left,
	Unknown
}

#endregion
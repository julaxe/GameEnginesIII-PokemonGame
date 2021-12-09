using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Effect", order = 3)]

public class AbillitiesEffect : ScriptableObject
{
    public string name;
    public string description;
    [Range(0,100)]
    public float chanceOfEffectApplied;
    public int durationInRounds;
    public int damageOverRound;
    public EffectType effectType;
    public bool affectsTheEnemy;
}

public enum EffectType
{
    PARALIZE = 0,
    BURN,
    IMMUNITY,
    NONE
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability", order = 2)]
public class Ability : ScriptableObject
{
    public string abilityName;
    public Type type;
    public int damage;
    public int accuracy;
    public int maxPP;
    public int pp;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Pokemon", menuName = "ScriptableObjects/Pokemon", order = 1)]
public class Pokemon : ScriptableObject
{
    public Sprite image;
    public string pokemonName;
    public Type type;
    public int hp;
    public int maxHp;
    public int attack;
    public int defense;
    public int speed;
    public int level = 5;
    public Ability[] abilities = new Ability[4];
    public Ability currentAbility = null;
}

public enum Type{
    Normal,
    Water,
    Fire,
    Grass
}

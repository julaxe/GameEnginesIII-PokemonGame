using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Pokemon[] pokemons = new Pokemon[6];
    public Pokemon ActivePokemon;
    public string CharacterName;
    public bool NPC = false;
    public Sprite sprite;

    private void Start()
    {
        ActivePokemon = pokemons[0];
    }
}

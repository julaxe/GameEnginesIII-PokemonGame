using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Pokemon[] InitPokemons = new Pokemon[6];
    [HideInInspector] public  Pokemon[] pokemons = new Pokemon[6];
    public Pokemon ActivePokemon;
    public string CharacterName;
    public bool NPC = false;
    public Sprite sprite;
    public string winPhrase = "just a Default win phrase here";
    public string losePhrase = "just a Default lose phrase here";

    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            if (InitPokemons[i] != null)
            {
                pokemons[i] = Instantiate(InitPokemons[i]);
            }
        }
        ActivePokemon = pokemons[0];
    }
}

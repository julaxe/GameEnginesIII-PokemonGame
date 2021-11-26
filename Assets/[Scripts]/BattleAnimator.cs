using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleAnimator : MonoBehaviour
{
    private Animator animator;
    private Character _character1;
    private Character _character2;
    public Image ImageP1;
    public Image ImageP2;

    
    public TMPro.TextMeshProUGUI PokemonName1;
    public TMPro.TextMeshProUGUI PokemonName2;
    public TMPro.TextMeshProUGUI PokemonLevel1;
    public TMPro.TextMeshProUGUI PokemonLevel2;


    public void  StartAnimation(Character character1, Character character2)
    {
        animator = GetComponent<Animator>();
        if(Time.timeScale == 0.0f)
        {
            animator.SetBool("endBattle", false);
        }
        _character1 = character1;
        _character2 = character2;
        
    }

    public void SetCharacters()
    {
        ImageP1.sprite = _character1.sprite;
        ImageP2.sprite = _character2.sprite;

    }
    public void ChangePokemonCharacter2()
    {
        ImageP2.sprite = _character2.pokemons[0].image;
        PokemonLevel2.text = "Lv" + _character2.pokemons[0].level.ToString();
        PokemonName2.text = _character2.pokemons[0].pokemonName;
    }
    public void ChangePokemonCharacter1()
    {
        ImageP1.sprite = _character1.pokemons[0].image;
        PokemonLevel1.text = "Lv" + _character1.pokemons[0].level.ToString();
        PokemonName1.text = _character1.pokemons[0].pokemonName;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame

    public void EndBattle()
    {
        animator.SetBool("endBattle", true);
    }
}

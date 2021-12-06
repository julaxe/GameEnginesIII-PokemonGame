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
    public List<string> QueueClips;
    
    public TMPro.TextMeshProUGUI PokemonName1;
    public TMPro.TextMeshProUGUI PokemonName2;
    public TMPro.TextMeshProUGUI PokemonLevel1;
    public TMPro.TextMeshProUGUI PokemonLevel2;


    IEnumerator QueueAnimation(int index)
    {
        if (index >= QueueClips.Count)
        {
            yield break;
        }

        string currentClip = QueueClips[index];
        
        animator.Play(currentClip);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        StartCoroutine(QueueAnimation(index + 1));
    }
    public void  StartAnimation(Character character1, Character character2)
    {
        animator = GetComponent<Animator>();
        _character1 = character1;
        _character2 = character2;
        QueueClips.Add("ShowBattle");
        QueueClips.Add("SlideInPlayers");
        StartCoroutine(QueueAnimation(0));
    }

    public void IntroduceFirstPokemons()
    {
        
    }
    

    public void SetCharacters()
    {
        ImageP1.sprite = _character1.sprite;
        ImageP2.sprite = _character2.sprite;
    }
    public void ChangePokemonCharacter2()
    {
        ImageP2.sprite = _character2.ActivePokemon.image;
        PokemonLevel2.text = "Lv" + _character2.ActivePokemon.level.ToString();
        PokemonName2.text = _character2.ActivePokemon.pokemonName;
    }
    public void ChangePokemonCharacter1()
    {
        ImageP1.sprite = _character1.ActivePokemon.image;
        PokemonLevel1.text = "Lv" + _character1.ActivePokemon.level.ToString();
        PokemonName1.text = _character1.ActivePokemon.pokemonName;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame

    public void EndBattle()
    {
        //animator.SetBool("endBattle", true);
    }
}

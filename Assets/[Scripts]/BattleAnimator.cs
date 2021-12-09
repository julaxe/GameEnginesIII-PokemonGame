using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleAnimator : MonoBehaviour
{
    private Animator animator;
    private Character _character1;
    private Character _character2;
    private Image ImageP1;
    private Image ImageP2;
    public List<string> QueueClips;
    public bool _isUpdatingHP = false;
    public float _HPUpdatingSpeed = 0.01f;
    
    private TMPro.TextMeshProUGUI PokemonName1;
    private TMPro.TextMeshProUGUI PokemonName2;
    private TMPro.TextMeshProUGUI PokemonLevel1;
    private TMPro.TextMeshProUGUI PokemonLevel2;

    public bool isPlaying = false;


    IEnumerator QueueAnimation(int index)
    {
        isPlaying = true;
        if (index >= QueueClips.Count)
        {
            isPlaying = false;
            yield break;
        }

        string currentClip = QueueClips[index];
        
        animator.Play(currentClip);

        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorStateInfo(0).length);
        

        StartCoroutine(QueueAnimation(index + 1));
    }

    private void Start()
    {
        ImageP1 = transform
            .Find("PlayerUI").GetComponent<Image>();
        ImageP2 = transform
            .Find("EnemyUI").GetComponent<Image>();
        PokemonName1 = transform
            .Find("BattleHeaders/PlayerHeader/PokemonName/Text").GetComponent<TextMeshProUGUI>();
        PokemonName2 = transform
            .Find("BattleHeaders/EnemyHeader/PokemonName/Text").GetComponent<TextMeshProUGUI>();
        PokemonLevel1 = transform
            .Find("BattleHeaders/PlayerHeader/PokemonLevel/Text").GetComponent<TextMeshProUGUI>();
        PokemonLevel2 = transform
            .Find("BattleHeaders/EnemyHeader/PokemonLevel/Text").GetComponent<TextMeshProUGUI>();
    }

    public bool IsQueuePlaying()
    {
        return isPlaying;
    }
    public void  StartBattleAnimator(Character character1, Character character2)
    {
        animator = GetComponent<Animator>();
        _character1 = character1;
        _character2 = character2;
    }

    public void StartQueueAnimation(string[] animations)
    {
        QueueClips.Clear();
        foreach (var animation in animations)
        {
            QueueClips.Add(animation);
        }
        StartCoroutine(QueueAnimation(0));
    }

    public bool IsUpdatingHP()
    {
        return _isUpdatingHP;
    }
    public void UpdateCharacter1HP(int damage)
    {
        GameObject character1HP = transform
            .Find("BattleHeaders/PlayerHeader/PokemonHealth/HealthBar/BlackBackground/Health").gameObject;
        StartCoroutine(UpdateHP(character1HP, damage));

    }
    public void UpdateCharacter2HP(int damage)
    {
        GameObject character2HP = transform
            .Find("BattleHeaders/EnemyHeader/PokemonHealth/HealthBar/BlackBackground/Health").gameObject;
        StartCoroutine(UpdateHP(character2HP, damage));

    }
    IEnumerator UpdateHP(GameObject CharacterHp, int damage)
    {
        _isUpdatingHP = true;
        for(int i = 0; i < damage; i++)
        {
            Vector3 newPosition = CharacterHp.transform.localPosition;
            newPosition.x -= 1;
            CharacterHp.transform.localPosition = newPosition;
            yield return new WaitForSecondsRealtime(_HPUpdatingSpeed);
        }

        _isUpdatingHP = false;
    }
    
    //call by animation events
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
        GameObject character2HP = transform
            .Find("BattleHeaders/EnemyHeader/PokemonHealth/HealthBar/BlackBackground/Health").gameObject;
        character2HP.transform.localPosition = Vector3.zero;
    }
    public void ChangePokemonCharacter1()
    {
        ImageP1.sprite = _character1.ActivePokemon.image;
        PokemonLevel1.text = "Lv" + _character1.ActivePokemon.level.ToString();
        PokemonName1.text = _character1.ActivePokemon.pokemonName;
        GameObject character1HP = transform
            .Find("BattleHeaders/PlayerHeader/PokemonHealth/HealthBar/BlackBackground/Health").gameObject;
        character1HP.transform.localPosition = Vector3.zero;
        
    }

    public void SetEnemySprite()
    {
        ImageP2.sprite = _character2.sprite;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame

    public void EndBattle()
    {
        ResumeGame();
    }
}

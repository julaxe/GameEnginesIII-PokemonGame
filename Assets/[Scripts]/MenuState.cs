
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuState : IStateBase
{
    private Button FightButton, PokemonButton, RunButton, InsultButton;
    private Button Ability1Button, Ability2Button, Ability3Button, Ability4Button;
    private GameObject abilityInfoGameObject;
    private TextMeshProUGUI PPCountText, TypeText;
    private Character character1;
    public override void OnEnterState(BattleManager battleManager)
    {
        base.OnEnterState(battleManager);
        character1 = _battleManager.GetCharacter1();
        _battleManager.ShowBattleOptions();
        Debug.Log("Menu state entered");

        InitializeMenuOptionsButtons();
        InitializeAbilitiesButtons();
        FightButton.onClick.AddListener(Fight);
        
        FightButton.Select();
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        UpdateAbilityStatus();
    }
    public void Fight()
    {
        
        //set the text
        Ability1Button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
            character1.ActivePokemon.abilities[0]
                ? character1.ActivePokemon.abilities[0].abilityName
                : "";
        Ability2Button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
            character1.ActivePokemon.abilities[1]
                ? character1.ActivePokemon.abilities[1].abilityName
                : "";
        Ability3Button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
            character1.ActivePokemon.abilities[2]
                ? character1.ActivePokemon.abilities[2].abilityName
                : "";
        Ability4Button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
            character1.ActivePokemon.abilities[3]
                ? character1.ActivePokemon.abilities[3].abilityName
                : "";
        
        
        _battleManager.ShowBattleAbilities();
        
        Ability1Button.Select();
        
    }

    public void UpdateAbilityStatus()
    {
        if (EventSystem.current.currentSelectedGameObject == Ability1Button.gameObject)
        {
            if(character1.ActivePokemon.abilities[0])
                ChangeAbilityStatusText(character1.ActivePokemon.abilities[0]);
        }
        else if (EventSystem.current.currentSelectedGameObject == Ability2Button.gameObject)
        {
            if(character1.ActivePokemon.abilities[1])
                ChangeAbilityStatusText(character1.ActivePokemon.abilities[1]);
        }
        else if (EventSystem.current.currentSelectedGameObject == Ability3Button.gameObject)
        {
            if(character1.ActivePokemon.abilities[2])
                ChangeAbilityStatusText(character1.ActivePokemon.abilities[2]);
        }
        else if (EventSystem.current.currentSelectedGameObject == Ability4Button.gameObject)
        {
            if(character1.ActivePokemon.abilities[3])
                ChangeAbilityStatusText(character1.ActivePokemon.abilities[3]);
        }
    }

    public void ChangeAbilityStatusText(Ability ability)
    {
        PPCountText.text = ability.pp + "/" + ability.maxPP;
        TypeText.text = "TYPE/" + ability.type;
    }


    private void InitializeMenuOptionsButtons()
    {
        FightButton = _battleManager.GetBattleChatBox().GetBattleOptions().transform
            .Find("RightSide/Options/FightButton").GetComponent<Button>();
        PokemonButton = _battleManager.GetBattleChatBox().GetBattleOptions().transform
            .Find("RightSide/Options/PokemonButton").GetComponent<Button>();
        RunButton = _battleManager.GetBattleChatBox().GetBattleOptions().transform
            .Find("RightSide/Options/RunButton").GetComponent<Button>();
        InsultButton = _battleManager.GetBattleChatBox().GetBattleOptions().transform
            .Find("RightSide/Options/InsultButton").GetComponent<Button>();
    }
    
    private void InitializeAbilitiesButtons()
    {
        Ability1Button = _battleManager.GetBattleChatBox().GetAbiltiesOptions().transform
            .Find("LeftSide/Options/Ability1Button").GetComponent<Button>();
        Ability2Button = _battleManager.GetBattleChatBox().GetAbiltiesOptions().transform
            .Find("LeftSide/Options/Ability2Button").GetComponent<Button>();
        Ability3Button = _battleManager.GetBattleChatBox().GetAbiltiesOptions().transform
            .Find("LeftSide/Options/Ability3Button").GetComponent<Button>();
        Ability4Button = _battleManager.GetBattleChatBox().GetAbiltiesOptions().transform
            .Find("LeftSide/Options/Ability4Button").GetComponent<Button>();

        abilityInfoGameObject = _battleManager.GetBattleChatBox().GetAbiltiesOptions().transform
            .Find("RightSide/AbilityInfo").gameObject;
        PPCountText = abilityInfoGameObject.transform.Find("PPCount/Text").GetComponent<TextMeshProUGUI>();
        TypeText = abilityInfoGameObject.transform.Find("Type/Text").GetComponent<TextMeshProUGUI>();
    }
    
}
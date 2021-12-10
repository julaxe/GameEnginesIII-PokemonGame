
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
    private bool inAbilitiesMenu = false;
    
    public override void OnEnterState(BattleManager battleManager)
    {
        base.OnEnterState(battleManager);
        _battleManager.ShowBattleOptions();
        Debug.Log("Menu state entered");

        InitializeMenuOptionsButtons();
        InitializeAbilitiesButtons();
        FightButton.onClick.AddListener(Fight);
        PokemonButton.onClick.AddListener(ChangePokemon);
        RunButton.onClick.AddListener(Run);
        InsultButton.onClick.AddListener(Insult);
        
        FightButton.Select();
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        UpdateAbilityStatus();
        if (inAbilitiesMenu)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Return))
            {
                inAbilitiesMenu = false;
                _battleManager.ShowBattleOptions(); 
                FightButton.Select();
            }
        }
        
        
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

        inAbilitiesMenu = true;
        _battleManager.ShowBattleAbilities();
        
        Ability1Button.Select();
    }

    private void ChangePokemon()
    {
        _battleManager.GetBattleStateMachine().ChangeStateByKey("ChangePokemonState");
    }

    private void Run()
    {
        _battleManager.GetBattleStateMachine().ChangeStateByKey("RunState");
    }
    private void Insult()
    {
        _battleManager.GetBattleStateMachine().ChangeStateByKey("InsultState");
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

    public void OnClickAbility(int number)
    {
        character1.ActivePokemon.currentAbility = character1.ActivePokemon.abilities[number];
        if (character1.ActivePokemon.currentAbility != null)
        {
            //go to battle
            _battleManager.GetBattleStateMachine().ChangeStateByKey("BattleState");
        }
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

        //initialize ability info UI
        abilityInfoGameObject = _battleManager.GetBattleChatBox().GetAbiltiesOptions().transform
            .Find("RightSide/AbilityInfo").gameObject;
        PPCountText = abilityInfoGameObject.transform.Find("PPCount/Text").GetComponent<TextMeshProUGUI>();
        TypeText = abilityInfoGameObject.transform.Find("Type/Text").GetComponent<TextMeshProUGUI>();
        
        //add function to onClick event to the buttons.
        Ability1Button.onClick.AddListener(delegate{OnClickAbility(0);});
        Ability2Button.onClick.AddListener(delegate{OnClickAbility(1);});
        Ability3Button.onClick.AddListener(delegate{OnClickAbility(2);});
        Ability4Button.onClick.AddListener(delegate{OnClickAbility(3);});
        
    }
    
}
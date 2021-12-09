using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts_;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    private BattleAnimator _battleAnimator;
    private BattleChatBox _battleChatBox = null;
    private Character _character1;
    private Character _character2;
    private StateManager _battleStateMachine;
    


    void Start()
    {
        _battleChatBox = GameObject.Find("UI/Canvas/Battle/BattleChatBox").GetComponent<BattleChatBox>();
        _battleAnimator = GameObject.Find("UI/Canvas/Battle").GetComponent<BattleAnimator>();
        _battleStateMachine = GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public StateManager GetBattleStateMachine()
    {
        return _battleStateMachine;
    }

    public BattleChatBox GetBattleChatBox()
    {
        return _battleChatBox;
    }

    public Character GetCharacter1()
    {
        return _character1;
    }
    public Character GetCharacter2()
    {
        return _character2;
    }
    
    public void SetCharacters(Character character1, Character character2)
    {
        _character1 = character1;
        _character2 = character2;
    }

    public string GetTypeEffectiveness(Type type1, Type type2)
    {
        string effectiveness = "";
        if (type1 == Type.Fire)
        {
            if (type2 == Type.Grass)
            {
                effectiveness =  "Super Effective";
            }
            else if (type2 == Type.Water)
            {
                effectiveness = "Not very effective";
            }
        }
        else if (type1 == Type.Water)
        {
            if (type2 == Type.Fire)
            {
                effectiveness =  "Super Effective";
            }
            else if (type2 == Type.Grass)
            {
                effectiveness = "Not very effective";
            }
        }
        else if (type1 == Type.Grass)
        {
            if (type2 == Type.Water)
            {
                effectiveness =  "Super Effective";
            }
            else if (type2 == Type.Fire)
            {
                effectiveness = "Not very effective";
            }
        }

        return effectiveness;

    }
    public void StartBattle()
    {
        //first pause the game
        Time.timeScale = 0;
        //then show the battle
        _battleAnimator.gameObject.SetActive(true);
        _battleAnimator.StartBattleAnimator(_character1, _character2);
        _battleChatBox.StartBattleChatBox();
        
        _battleStateMachine.StartStateMachine("StartState");
    }

    public BattleAnimator GetBattleAnimator()
    {
        return _battleAnimator;
    }
    public void ShowBattleMessage()
    {
        _battleChatBox.StartBattleMessage();
    }
    public void ShowBattleOptions()
    {
        _battleChatBox.StartBattleOptions();
    }

    public void ShowBattleAbilities()
    {
        _battleChatBox.StartAbilitiesOptions();
    }
    public void EndBattle()
    {
        _battleAnimator.EndBattle();
    }
}

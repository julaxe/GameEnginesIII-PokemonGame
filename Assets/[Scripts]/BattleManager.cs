using System.Collections;
using System.Collections.Generic;
using _Scripts_;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public BattleAnimator battleAnimator;
    public GameObject _GOBattleChatBox;
    private BattleChatBox battleChatBox = null;
    private Character _character1;
    private Character _character2;




    void Start()
    {
        if(_GOBattleChatBox == null) _GOBattleChatBox = GameObject.Find("UI/Canvas/Battle/BattleChatBox");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle(Character character1, Character character2)
    {
        _character1 = character1;
        _character2 = character2;
        //first pause the game
        Time.timeScale = 0;
        //then show the battle
        battleAnimator.gameObject.SetActive(true);
        battleAnimator.StartAnimation(character1, character2);
        battleChatBox = new BattleChatBox(_GOBattleChatBox, character1);
        battleChatBox.StartBattleMessage();
    }

    public void ShowBattleOptions()
    {
        battleChatBox.StartBattleOptions();
    }
    public void EndBattle()
    {
        battleAnimator.EndBattle();
    }
}

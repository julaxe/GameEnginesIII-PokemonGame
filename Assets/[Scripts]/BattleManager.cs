using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public BattleAnimator battle;
   
    private Character _character1;
    private Character _character2;




    void Start()
    {

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
        battle.gameObject.SetActive(true);
        battle.StartAnimation(character1, character2);
    }
    public void EndBattle()
    {
        battle.EndBattle();
    }
}

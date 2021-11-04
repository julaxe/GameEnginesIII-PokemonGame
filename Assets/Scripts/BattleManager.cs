using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public BattleAnimator battle;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Show the battle
            if (!battle.gameObject.activeInHierarchy)
            {
                StartBattle();
            }
            else
            {
                EndBattle();
            }
        }
    }

    public void StartBattle()
    {
        //first pause the game
        Time.timeScale = 0;
        //then show the battle
        battle.gameObject.SetActive(true);
        battle.Start();
    }
    public void EndBattle()
    {
        battle.EndBattle();
    }
}

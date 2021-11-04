using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimator : MonoBehaviour
{
    private Animator animator;
    public void  Start()
    {
        animator = GetComponent<Animator>();
        if(Time.timeScale == 0.0f)
        {
            animator.SetBool("endBattle", false);
        }
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

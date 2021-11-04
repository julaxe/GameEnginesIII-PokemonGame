using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float Fspeed;

    private Vector3 newVelocity;

    private Rigidbody2D rb;

    private bool onGrass = false;

    private BattleManager battleManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        battleManager = GameObject.FindObjectOfType<BattleManager>();
    }

    // Update is called once per frame
    
    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(inputX, inputY) * Fspeed * Time.fixedDeltaTime;
        if (onGrass)
        {
            if(rb.velocity != Vector2.zero) //if I'm moving
            {
                if(Random.Range(0,100) == 0) // 1/100 chance every frame to encounter a battle
                {
                    battleManager.StartBattle();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            onGrass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            onGrass = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer1 : MonoBehaviour
{
    StateManger statemanger1;
    GameObject PLayer1_ship_AI;
    GameObject PickedShip;
    Player ThePlayer1_AI;

    PlayerShips ThePlayerships_ai_1;

    void Start()
    {
        ThePlayerships_ai_1 = GameObject.FindObjectOfType<PlayerShips>();
    }

    public AIPlayer1()
    {
        statemanger1 = GameObject.FindObjectOfType<StateManger>();

    }

    virtual public void DoAI()
    {


        if (statemanger1.IsDoneRolling == false)
        {
            // we need to roll the dice

            DoRoll();

            return;
        }

        if (statemanger1.IsDoneClicking == false)
        {

            // 
            // we have a die roll but still need to pick ship and move
            DoClick();
            return;

        }
    }

    virtual protected void DoRoll()
    {
        // roll without clicking the button
        GameObject.FindObjectOfType<DiceRoller>().RollTheDice();       // find the script diceroller and run the function "rollthedice

    }

    virtual protected void DoClick()
    {
        
        ThePlayer1_AI = GameObject.FindObjectOfType<Player>();
        PickedShip = GameObject.Find("PLAYER1");
        Debug.Log("PickedShip" + PickedShip);


        // Try slow the AI down when all 4 are AI

        ThePlayerships_ai_1.MoveMe();


    }


  
}

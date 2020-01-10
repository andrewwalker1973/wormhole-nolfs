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
        // Pick a ship to move and then click it



        //  PlayerShips[] legalStones = GetLegalMoves();

        //    if (legalStones == null || legalStones.Length == 0)
        //     {
        // we have no legal moves, how did we get here
        //         return;
        //     }

        // basic AI Picks a legal move at random
        // PlayerShips PickedShip = legalStones[Random.Range(0, legalStones.Length)];
        ThePlayer1_AI = GameObject.FindObjectOfType<Player>();
        PickedShip = GameObject.Find("PLAYER1");
        Debug.Log("PickedShip" + PickedShip);


        // Try slow the AI down when all 4 are AI

        ThePlayerships_ai_1.MoveMe();


    }


  /*  protected PlayerShips GetLegalMoves()
    {

        List<PlayerShips> legalStones = new List<PlayerShips>();

        
        //loop through all the player stones

        ThePlayer1_AI = GameObject.FindObjectOfType<Player>();
        PLayer1_ship_AI = GameObject.Find("PLAYER1");

     // ThePlayer1_AI = GameObject.FindObjectOfType<Player>();
     // PlayerShips PLayer1_ship_AI =  GameObject.Find("PLAYER1");
     //   foreach (PlayerShips ps in ThePlayer1_AI)
     //   {

            if (ThePlayerships_ai_1.PlayerId == statemanger1.CurrentPlayerId)
            {
                if (ThePlayerships_ai_1.CanLegallyMoveAhead(statemanger1.DiceTotal))
                {
                    legalStones.Add(ThePlayerships_ai_1);
                }
            }

        //Highlight the ones that can be legal moved
        // if no logal moves wait a second then move to next player with message


    }

        return legalStones.ToArray();
    }

    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIPlayer
{


    StateManger statemanger;

    public AIPlayer()
    {
        statemanger = GameObject.FindObjectOfType<StateManger>();

    }
    
   virtual public void  DoAI()
    {
       

        if (statemanger.IsDoneRolling == false)
        {
            // we need to roll the dice
            
            DoRoll();

            return;
        }

        if (statemanger.IsDoneClicking == false)
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


        
        PlayerShips[] legalStones = GetLegalMoves();

        if (legalStones == null || legalStones.Length == 0)
        {
            // we have no legal moves, how did we get here
            return; 
        }

        // basic AI Picks a legal move at random
        PlayerShips PickedShip = legalStones[Random.Range(0, legalStones.Length)];


        // Try slow the AI down when all 4 are AI
      
        PickedShip.MoveMe();


    }


    protected PlayerShips[] GetLegalMoves()
    {

        List<PlayerShips> legalStones = new List<PlayerShips>();
        
        // if we roll a 0 we have no legal moves
        if (statemanger.DiceTotal == 0)
        {
           return legalStones.ToArray();

        }
        //loop through all the player stones

        PlayerShips[] pss = GameObject.FindObjectsOfType<PlayerShips>();
        foreach ( PlayerShips ps in pss)
        {
            if (ps.PlayerId == statemanger.CurrentPlayerId)
            {
                if (ps.CanLegallyMoveAhead(statemanger.DiceTotal))
                {
                    legalStones.Add(ps);
                }
            }

        //Highlight the ones that can be legal moved
        // if no logal moves wait a second then move to next player with message

        
    }

        return legalStones.ToArray();
}
}

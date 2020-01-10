using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIPlayer
{

    StateManger theStateManager;    // create access to statmanger vars
    DiceRoller TheDiceRoller;        // create access to DiceRoller vars



    public AIPlayer()
    {
        theStateManager = GameObject.FindObjectOfType<StateManger>();   // create access to statmanger vars
        TheDiceRoller = GameObject.FindObjectOfType<DiceRoller>();      // create access to DiceRoller vars




    }
    
   virtual public void  DoAI()
    {
        if (theStateManager.IsDoneRolling == false)
        {
            // we need to roll the dice            
            DoRoll();          
            return;
        }

        if (theStateManager.IsDoneClicking == false)
        {
           
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
        // Pick a stone to move, then "click" it.
      
        PlayerShips[] legalStones = GetLegalMoves();

        if (legalStones == null || legalStones.Length == 0)
        {
            // We have no legal moves.  How did we get here?
            // We might still be in a delayed coroutine somewhere. Let's not freak out.
            return;
        }

        // BasicAI simply picks a legal move at random

        PlayerShips pickedStone = PickStoneToMove(legalStones);

           pickedStone.MoveMe();            // run the moveme function in playerships script
       
    }




virtual protected PlayerShips PickStoneToMove(PlayerShips[] legalStones)
{
        
        return legalStones[Random.Range(0, 0)];
        

    }



/// Returns a list of stones that can be legally moved

protected PlayerShips[] GetLegalMoves()
{
     
       List<PlayerShips> legalStones = new List<PlayerShips>();
     
        if (theStateManager.DiceTotal == 0)   // make sure we dont roll a 0
    {
        return legalStones.ToArray();
    }

    // Loop through all of a player's stones
    PlayerShips[] pss = GameObject.FindObjectsOfType<PlayerShips>();

        foreach (PlayerShips ps in pss)             //check on every ship found to see if it can move
    {
        if (ps.PlayerId == theStateManager.CurrentPlayerId)
        {
            if (ps.CanLegallyMoveAhead(theStateManager.DiceTotal))  // check if ship can move based on dice total display
            {

                    legalStones.Add(ps);   // send back values to array
            }
        }
    }
       
        return legalStones.ToArray();
}

}

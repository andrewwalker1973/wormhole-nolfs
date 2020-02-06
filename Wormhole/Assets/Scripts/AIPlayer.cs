using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AIPlayer
{

    StateManger theStateManager;    // create access to statmanger vars
    DiceRoller TheDiceRoller;        // create access to DiceRoller vars

    Player ThePlayer1;

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
        //    Debug.Log("Do roll Done");
            return;
        }

        if (theStateManager.IsDoneClicking == false)
        {

            // we have a die roll but still need to pick ship and move
        //    Debug.Log("Do Click start");
            DoClick();
         //   Debug.Log("Do Click Done");
            return;

        }
    }

    virtual protected void DoRoll()
    {
       // Debug.Log("DoRoll");
        // roll without clicking the button      
        GameObject.FindObjectOfType<DiceRoller>().RollTheDice();       // find the script diceroller and run the function "rollthedice
        
    }

    virtual protected void DoClick()
    {

        Debug.Log("DoClick");
        // Pick a stone to move, then "click" it.


      
         PlayerShips[] legalStones = GetLegalMoves();
           Debug.Log("DoClick   legal Stones" + legalStones[0]);
          if (legalStones == null || legalStones.Length == 0)
          {
              if (legalStones == null)
              {
                  Debug.Log(" legalStones == null");

              }
              if (legalStones.Length == 0)
              {
                  Debug.Log("legalStones.Length setting");
                  //   PlayerShips[] legalStones1 = GetLegalMoves();
                  //    Debug.Log(" force legalStones1.Length " + legalStones1.Length);
                 // legalStones = GetLegalMoves();

              }
              // We have no legal moves.  How did we get here?
              // We might still be in a delayed coroutine somewhere. Let's not freak out.
              Debug.Log("!!!!!!!!!!!!!!!!!  in no legal stones fucntion");
              Debug.Log("legalStones.Length " + legalStones.Length);
              Debug.Log("legalStones " + legalStones);
              return;
          }





          // BasicAI simply picks a legal move at random

          PlayerShips pickedStone = PickStoneToMove(legalStones);
        Debug.Log("Picked stone  legal sones " + pickedStone);
          Debug.Log("legalStones" + legalStones[0]);
          Debug.Log("Legalstones.length " + legalStones.Length);
        Debug.Log("AI Move me" + pickedStone );
        if (pickedStone == null)
        {
            Debug.Log("&&&&&&&&&& pickedStone is null");
        }
        
             pickedStone.MoveMe();            // run the moveme function in playerships script


      


        
    }




virtual protected PlayerShips PickStoneToMove(PlayerShips[] legalStones)
{
       Debug.Log("PickStoneToMove");
        // return legalStones[Random.Range(0, legalStones.Length)];
        return legalStones[UnityEngine.Random.Range(0,1)];

    }



/// Returns a list of stones that can be legally moved

protected PlayerShips[] GetLegalMoves()
{

      
      //  Debug.Log("GetLegalMoves");
            List<PlayerShips> legalStones = new List<PlayerShips>();
        if (legalStones == null)
        {
            Debug.Log(" GetLegalMoves legalStones == null");

        }

        if (theStateManager.DiceTotal == 0)   // make sure we dont roll a 0
    {
            //       Debug.Log("DiceRoller total = 0");
            Debug.Log("sssSSSSSSS" + legalStones.ToArray());
        return legalStones.ToArray();
    }
        
        // Loop through all of a player's stones
        PlayerShips[] pss = GameObject.FindObjectsOfType<PlayerShips>();

        foreach (PlayerShips ps in pss)             //check on every ship found to see if it can move
    {
        if (ps.PlayerId == theStateManager.CurrentPlayerId)
        {
               Debug.Log("ps.name " + ps.name);
                Debug.Log("ps.playerid " + ps.PlayerId);
              //  Debug.Log("theStateManager.CurrentPlayerId " + theStateManager.CurrentPlayerId);
             //   Debug.Log("theStateManager.DiceTotal " + theStateManager.DiceTotal);
                if (ps.CanLegallyMoveAhead(theStateManager.DiceTotal))  // check if ship can move based on dice total display  equates to true/false
            {
                    //       Debug.Log("Add stone to array");
               //     Debug.Log("FindObjectsOfType " + legalStones);
                    Debug.Log("PS is " + ps);
                    Debug.Log("ps.CanLegallyMoveAhead " + ps.CanLegallyMoveAhead(theStateManager.DiceTotal));
                    //Debug.Log("GetLegalMoves  legalStones" + legalStones[0]);
                    legalStones.Add(ps);   // send back values to array
            }
                else
                {
                    Debug.Log("############# ps.CanLegallyMoveAhead(theStateManager.DiceTotal)  is false");
                   // DoClick();
                }
        }
    }
        // Debug.Log("legalStones.ToArray " );

        //Debug.Log("sss" + legalStones.ToArray());
        return legalStones.ToArray();
}

    
    }

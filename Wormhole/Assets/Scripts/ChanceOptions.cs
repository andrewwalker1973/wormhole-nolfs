using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceOptions : MonoBehaviour
{

    StateManger theStateManager;
    PlayerShips ThePlayerships;         // gain access to the playerships vars

    Tile[] ChancemoveQueue;
    public int ChancemoveQueueIndex;
    Tile currentTile;
    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManger>();
        ThePlayerships = GameObject.FindObjectOfType<PlayerShips>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public  void  DoChanceClick()
    {
        // Pick a stone to move, then "click" it.

        PlayerShips[] ChancelegalStones = GetChanceLegalMoves();

        if (ChancelegalStones == null || ChancelegalStones.Length == 0)
        {
            // We have no legal moves.  How did we get here?
            // We might still be in a delayed coroutine somewhere. Let's not freak out.
            return;
        }

        // BasicAI simply picks a legal move at random

        PlayerShips pickedStone = PickChanceStoneToMove(ChancelegalStones);

        pickedStone.MoveMe();            // run the moveme function in playerships script

    }

    protected PlayerShips[] GetChanceLegalMoves()
    {

        List<PlayerShips> ChancelegalStones = new List<PlayerShips>();

        if (theStateManager.DiceTotal == 0)   // make sure we dont roll a 0
        {
            return ChancelegalStones.ToArray();
        }

        // Loop through all of a player's stones
        PlayerShips[] pss = GameObject.FindObjectsOfType<PlayerShips>();

        foreach (PlayerShips ps in pss)             //check on every ship found to see if it can move
        {
            if (ps.PlayerId == theStateManager.CurrentPlayerId)
            {
                if (ps.CanLegallyMoveAhead(theStateManager.DiceTotal))  // check if ship can move based on dice total display
                {

                    ChancelegalStones.Add(ps);   // send back values to array
                }
            }
        }

        return ChancelegalStones.ToArray();
    }


    virtual protected PlayerShips PickChanceStoneToMove(PlayerShips[] ChancelegalStones)
    {

        return ChancelegalStones[Random.Range(0, 0)];


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShips : MonoBehaviour
{


    StateManger theStateManager;
  //  ChanceCards theChanceCards;

    //CameraController theCameraController;

    //added in for smooth move
    Vector3 targetPosition;
    Vector3 velocity;
    float smoothTime = 0.15f;
    float smoothTimeVertical = 0.1f;
    float smoothDistance = 0.01f;
    float smoothHeight = 35f;

    public Tile startingTile;
    Tile currentTile;

    public int PlayerId;
    






    // added into handle player moving off board
    bool scoreMe = false;

    // added in to keep record of tiles to move across
    Tile[] moveQueue;
   public  int moveQueueIndex;

    bool isAnimating = false;

    Player ThePlayers;
    private readonly object PlayerShipss;
    Tile theTiles;

    //  DiceRoller theDiceRoller;



    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManger>();
      //  theChanceCards = GameObject.FindObjectOfType<ChanceCards>();

        targetPosition = this.transform.position;

        ThePlayers = GameObject.FindObjectOfType<Player>();
        theTiles = GameObject.FindObjectOfType<Tile>();
        
    }



    




    // Update is called once per frame
    void Update()
    {
        if (isAnimating == false)
        {
            // Nothiog for us to do
            return;
        }
        if (Vector3.Distance(
            new Vector3(this.transform.position.x, targetPosition.y, this.transform.position.z),
            targetPosition)
            < smoothDistance)
           {

            // reached the target how is the height
            if (moveQueue != null && moveQueueIndex == (moveQueue.Length) && this.transform.position.y > smoothDistance)
            {
                this.transform.position = Vector3.SmoothDamp(
                this.transform.position,
                new Vector3(this.transform.position.x, 0, this.transform.position.z),
                ref velocity,
                smoothTimeVertical);
            }
            else
            {
                //right position and rihht height, advance the queue
                AdvancedMoveQueue();
               
            }

        }
        // move the player up a bit before we move to new location
        else if (this.transform.position.y < (smoothHeight - smoothDistance))
        {
            this.transform.position = Vector3.SmoothDamp(
            this.transform.position,
            new Vector3(this.transform.position.x, smoothHeight, this.transform.position.z),
            ref velocity,
            smoothTimeVertical);

        }
        else
        {
            this.transform.position = Vector3.SmoothDamp(
            this.transform.position,
            new Vector3(targetPosition.x, smoothHeight, targetPosition.z),
            ref velocity,
            smoothTime);
        }
    }


    void AdvancedMoveQueue()
    {
        if (moveQueue != null && moveQueueIndex < moveQueue.Length)
        {
            Tile nextTile = moveQueue[moveQueueIndex];
            if (nextTile == null)
            {
                // we are being scored move player off board
                //TODO something for end game winner, maybe move to winner area ?
                SetNewTargetPosition(this.transform.position + Vector3.right * 1f);

                       }
            else
            {
              //  Debug.Log("nextTile  " + nextTile);
                if (nextTile.IsrightTurnSpace == true)
                {
                   
                  //  Rotating by 90 degree to go UP
                    transform.Rotate(0, 90, 0);
                }

                if (nextTile.IsleftTurnSpace == true)
                {
                 
                  //  Rotating by -90 degree to go Left");
                    transform.Rotate(0, -90, 0);
                }

                
                SetNewTargetPosition(nextTile.transform.position);
                // try to face char in directin on travel
            /*    float moveHorizontal = Input.GetAxisRaw("Horizontal");
                float moveVertical = Input.GetAxisRaw("Vertical");
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                transform.rotation = Quaternion.LookRotation(movement);
                transform.Translate(movement * 1f * Time.deltaTime, Space.World);
                
                


                // -----------------------

                */
                moveQueueIndex++;
            }
            
        }
        else
        {
           
            //the movement queue is empty, we are done moving
            Debug.Log("Done animating.");
            this.isAnimating = false;
            theStateManager.IsDoneAnimating = true;
            //   theDiceRoller.TurnEnded();

        if (currentTile !=null && currentTile.IsRollAgain)
            {
                theStateManager.RollAgain();
            }

            if (currentTile != null && currentTile.ChanceCard)
            {
                theStateManager.ChanceCard();
            }


        }
    }


 

    void SetNewTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        velocity = Vector3.zero;
    }


    void OnMouseUp()
    {
         MoveMe();

    }


    public void MoveMe()
    {
        


        if (theStateManager.CurrentPlayerId != PlayerId)
        {
            return;  // its not my turn

        }

       
        if (theStateManager.IsDoneRolling == false)
        {
            //we cant move yet

            // Have we rolled the dice ?

           

            //CHECK TO MAKE SURE THERE IS NO ui OBJECT IN THE WAY
            return;

        }
        if (theStateManager.IsDoneClicking == true)
        {
            // we have already done a move
            return;
        }


        int spacesToMove = theStateManager.DiceTotal;

   //removeing due to movie 7 1:27    
    if (spacesToMove == 0)
        {
            return;
        }

    

        moveQueue = GetTilesAhead(spacesToMove);
       Tile finalTile = moveQueue[moveQueue.Length - 1];

  
    
if (finalTile == null)
        {
            // we are scoring this player
            scoreMe = true;
        }
    else
        {
            if (CanLegallyMoveTo(finalTile) == false)
            {
                // not allowed
                finalTile = currentTile;
                moveQueue = null;
                return;            
            }

            /*    // If there is an enemy tile in our legal space, the we kick it out.
                if (finalTile.PlayerStone != null)
                {
                    //finalTile.PlayerStone.ReturnToStorage();
                    stoneToBop = finalTile.PlayerStone;
                    stoneToBop.CurrentTile.PlayerStone = null;
                    stoneToBop.CurrentTile = null;
                }
            */

            /*    this.transform.SetParent(null); // Become Batman      remove from tile so other player can use it

                // Remove ourselves from our old tile
                if (CurrentTile != null)
                {
                    CurrentTile.PlayerStone = null;
                }
            */

            // Even before the animation is done, set our current tile to the new tile
            currentTile = finalTile;
            if (finalTile.IsScoringSpace == false)   // "Scoring" tiles are always "empty"
            {
                finalTile.PlayerShips = this;
            }

/*    removed as I think he changed thios this to score better
            if (finalTile.PlayerShips != null)
            {
                finalTile.PlayerShips.ReturnToStorage();
            }
            */
        }

        /*  removed my code for his below
          moveQueueIndex = 0;
          currentTile = finalTile;
          theStateManager.IsDoneClicking = true;
          this.isAnimating = true;

      */
        moveQueueIndex = 0;

        theStateManager.IsDoneClicking = true;
        this.isAnimating = true;
     //   theStateManager.AnimationsPlaying++;

    }

    // return a list oif tiles ahead
    Tile[] GetTilesAhead(int spacesToMove)
    {
        if (spacesToMove == 0)
        {
            return null;
        }


        // Where should we end up?
        Tile[] listOfTiles = new Tile[spacesToMove];
        Tile finalTile = currentTile;



        for (int i = 0; i < spacesToMove; i++)
        {
            //  check if no tile? ie off board ?
            if (finalTile == null)
            {
                finalTile = startingTile;
            }
            else
            {

                if (finalTile.NextTiles == null || finalTile.NextTiles.Length == 0)
                {

                    //we are overshooting the victory tile 
                    // break and return the array which will have nulls at the end
                    break;
                }
                else if (finalTile.NextTiles.Length > 1)
                {
                    // branch based on player ID
                    finalTile = finalTile.NextTiles[PlayerId];
                }
                else
                {
                    finalTile = finalTile.NextTiles[0];
                }


            }
            listOfTiles[i] = finalTile;
        }

        return listOfTiles;
    }


    public Tile GetTileAhead()
    {
        return GetTileAhead(theStateManager.DiceTotal);
    }

    // return the final tile we have landed on
    Tile GetTileAhead(int spacesToMove)
    {
        Tile[] tiles = GetTilesAhead(spacesToMove);

        if (tiles == null)
        {
            // we are not moving at all retun the current tile ?
            return currentTile;
        }
        return tiles [ tiles.Length -1];
    }

    public bool CanLegallyMoveAhead(int spacesToMove)
    {
        if (currentTile != null && currentTile.IsScoringSpace)
        {
            // This stone is already on a scoring tile, so we can't move.
            return false;
        }

        Tile theTile = GetTileAhead(spacesToMove);

        return CanLegallyMoveTo(theTile);
    }

    bool CanLegallyMoveTo(Tile destinationTile)
    {

       
        // does the tilealready have a stone - not needed for me
        // is this one of our stones - not needed for me
        // is this a safe tile for enemy ?
        if (destinationTile == null)
        {
            // Note NULL tile means we are overshooting the victory roll and not legal
            return false;

            // We are trying to move off the board
          //  Debug.Log("We are trying to move off baord which is leagl.");
          //  return true;
        }
        //is the tile empty
        if (destinationTile.PlayerShips == null)
        {
            return true;
        }




        if (destinationTile.PlayerShips.PlayerId == this.PlayerId)
        {
            // we cant land on one of our stones
            return false;

        }
            // if this is an enemy stone is it in a safe square ?
            // TODO Safe Squares ?

        if (destinationTile.IsRollAgain == true)
        {
            return false;
        }
            /*   // If it's an enemy stone, is it in a safe square?
          if (destinationTile.IsRollAgain == true)
          {
              // Can't bop someone on a safe tile!
              return false;
          }

      */

            // if we have gotten here we can legaly land on the enmy stone and kick it off



       

        return true;
    }

    public void ReturnToStorage()
    {
        // if we had defined stonestorage this is where t wouldreturn it to storage
        Debug.Log("Return to storage was activated");


    /*    Debug.Log("ReturnToStorage");
        //currentTile.PlayerStone = null;
        //currentTile = null;

        this.isAnimating = true;
        theStateManager.AnimationsPlaying++;

        moveQueue = null;

        // Save our current position
        Vector3 savePosition = this.transform.position;

        MyStoneStorage.AddStoneToStorage(this.gameObject);

        // Set our new position to the animation target
        SetNewTargetPosition(this.transform.position);

        // Restore our saved position
        this.transform.position = savePosition;

    */
    }

 /*   int spacesToMoveBack =4;
    // Try find a way to go backwards
    Tile[] GetTilesbehind(int spacesToMoveBack)
    {
        if (spacesToMoveBack == 0)
        {
            return null;
        }


        // Where should we end up?
        Tile[] listOfTiles = new Tile[spacesToMoveBack];
        Tile finalTile = currentTile;


        for (int i = spacesToMoveBack; i < 0; i--)
       // for (int i = 0; i < spacesToMove; i++)
        {
            //  check if no tile? ie off board ?
            if (finalTile == null)
            {
                finalTile = startingTile;
            }
            else
            {

                if (finalTile.NextTiles == null || finalTile.NextTiles.Length == 0)
                {

                    //we are overshooting the victory tile 
                    // break and return the array which will have nulls at the end
                    break;
                }
                else if (finalTile.NextTiles.Length > 1)
                {
                    // branch based on player ID
                    finalTile = finalTile.NextTiles[PlayerId];
                }
                else
                {
                    finalTile = finalTile.NextTiles[0];
                }


            }
            listOfTiles[i] = finalTile;
        }

        return listOfTiles;
    }
    */
 
}
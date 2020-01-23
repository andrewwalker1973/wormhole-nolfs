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

    // try get wormhole to work
    public Tile wormholedest24;
    public Tile wormholedest48;
    public Tile wormholedest28;
    public Tile wormholedest52;

    //   Tile WormholeDest;

    public int PlayerId;


    public int wormholeenter = 0;




    // added into handle player moving off board
    bool scoreMe = false;

    // added in to keep record of tiles to move across
    Tile[] moveQueue;
    public int moveQueueIndex;

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
                

                moveQueueIndex++;
            }

        }
        else
        {

            //the movement queue is empty, we are done moving
            Debug.Log("Done animating.");
            this.isAnimating = false;
            theStateManager.AnimationsPlaying --;
            //   theDiceRoller.TurnEnded();

            if (currentTile != null && currentTile.IsRollAgain)
            {
                theStateManager.RollAgain();
            }

            if (currentTile != null && currentTile.ChanceCard)
            {
                theStateManager.ChanceCard();
            }

            if (currentTile != null && currentTile.Wormhole2)
            {

                // StartCoroutine(UIWormholeenter());
                //   wormholeenter = 1;
                // StartCoroutine(JustWaitUICoroutineWormhole());
                // while (wormholeenter != 0)
                // {
                wormholeenter = 1;
               // StartCoroutine(PauseGame(50f));    
              // wormholemove();
                theStateManager.AnimationsPlaying++;
                    Tile finalTile = moveQueue[moveQueue.Length - 1];
                    finalTile = wormholedest24;
                    this.transform.position = finalTile.transform.position;
                    this.targetPosition = finalTile.transform.position;     // this set the tagetfixd to new tile
                    currentTile = finalTile;
                    moveQueue = null;
                    moveQueueIndex = 0;
                    StartCoroutine(PauseGameexit(2f));
            //  }

            
                // add in wormhole dest in player script


            }

            if (currentTile != null && currentTile.Wormhole5)
            {
                Tile finalTile = moveQueue[moveQueue.Length - 1];
                finalTile = wormholedest48;


                
                
               /* this.transform.position = finalTile.transform.position;
                this.targetPosition = finalTile.transform.position;     // this set the tagetfixd to new tile
                currentTile = finalTile;
                moveQueue = null;
                moveQueueIndex = 0;
*/
                // add in wormhole dest in player script


            }

            if (currentTile != null && currentTile.Wormhole8)
            {

                Tile finalTile = moveQueue[moveQueue.Length - 1];
                finalTile = wormholedest28;
                this.transform.position = finalTile.transform.position;
                this.targetPosition = finalTile.transform.position;     // this set the tagetfixd to new tile
                currentTile = finalTile;
                moveQueue = null;
                moveQueueIndex = 0;

                // add in wormhole dest in player script


            }

            if (currentTile != null && currentTile.Wormhole11)
            {
                //StartCoroutine(UIWormholeenter());
                Tile finalTile = moveQueue[moveQueue.Length - 1];
                finalTile = wormholedest52;
                this.transform.position = finalTile.transform.position;
                this.targetPosition = finalTile.transform.position;     // this set the tagetfixd to new tile
                StartCoroutine(UIWormholeenter());
                currentTile = finalTile;
                moveQueue = null;
                moveQueueIndex = 0;

                // add in wormhole dest in player script


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



            // Even before the animation is done, set our current tile to the new tile
            currentTile = finalTile;
            if (finalTile.IsScoringSpace == false)   // "Scoring" tiles are always "empty"
            {
                finalTile.PlayerShips = this;
            }


        }


        moveQueueIndex = 0;

     

        theStateManager.IsDoneClicking = true;
        this.isAnimating = true;
        theStateManager.AnimationsPlaying++;

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
        return tiles[tiles.Length - 1];
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

    }

   
    IEnumerator UIWormholeenter()
    {

              
        theStateManager.UIWormhole.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        
        theStateManager.UIWormhole.SetActive(false);
        
        yield return new WaitForSecondsRealtime(6);
           
    }

    public IEnumerator PauseGame (float Pausetime)
    {
        Debug.Log("instide PauseGame into");
     //   theStateManager.UIWormhole.SetActive(true);
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + Pausetime;
     //   while (wormholeenter != 0)
    //    {
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
      //      wormholeenter = 0;
     //   }
        Time.timeScale = 1f;
        Debug.Log("Done with Pause into");
        wormholemove();
        // theStateManager.UIWormhole.SetActive(false);
        //  wormholeenter = 0;



    }

    public IEnumerator PauseGameexit(float Pausetimeexit)
    {
        Debug.Log("instide PauseGame exit");
        Time.timeScale = 0f;
        float pauseEndTimeexit = Time.realtimeSinceStartup + Pausetimeexit;
        while (Time.realtimeSinceStartup < pauseEndTimeexit)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        Debug.Log("Done with Pause eit");
        theStateManager.AnimationsPlaying--;

    }

    IEnumerator JustWaitUICoroutineWormhole()
    {

        // wait one sec
        Debug.Log("Just Wait)");


      //  Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(10);
      //  Time.timeScale = 1f;
     Debug.Log("end wormhole being waint");



    }

    void wormholemove()
    {
        theStateManager.AnimationsPlaying++;
        Tile finalTile = moveQueue[moveQueue.Length - 1];
        finalTile = wormholedest24;
        this.transform.position = finalTile.transform.position;
        this.targetPosition = finalTile.transform.position;     // this set the tagetfixd to new tile
        currentTile = finalTile;
        moveQueue = null;
        moveQueueIndex = 0;
        StartCoroutine(PauseGameexit(2f));
    }
}

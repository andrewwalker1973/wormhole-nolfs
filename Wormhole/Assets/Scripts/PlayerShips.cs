using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShips : MonoBehaviour
{


    StateManger theStateManager;
    PlayerWon thePlayerWon;
    //  ChanceCards theChanceCards;

    //CameraController theCameraController;

    //added in for smooth move
    Vector3 targetPosition;
    Vector3 velocity;
    float smoothTime = 0.15f;
    float smoothTimeVertical = 0.1f;
    float smoothDistance = 0.01f;
    float smoothHeight = 35f;
    private bool isPlayerWon;
    private bool Player1_won;
    private bool Player2_won;
    private bool Player3_won;
    private bool Player4_won;

    private bool Player1_finished;
    private bool Player2_finished;
    private bool Player3_finished;
    private bool Player4_finished;

    //private bool SomebodyWon;
    //private bool isWon;

    public Tile startingTile;
    
    Tile currentTile;

    // try get wormhole to work
    public Tile WormholeDest;
    public Tile spacegate24;
    public Tile spacegate48;
    public Tile spacegate28;
    public Tile spacegate52;
    public Tile spacegate65;
    public Tile spacegate72;
    public Tile spacegate93;
    public Tile spacegate78;

    public Tile wormhole7;
    public Tile wormhole9;
    public Tile wormhole17;
    public Tile wormhole22;
    public Tile wormhole35;
    public Tile wormhole45;
    public Tile wormhole68;
    public Tile wormhole79;
    


    //   Tile WormholeDest;

    public int PlayerId;




    public GameObject UIWormhole;   //game object for skip turn on screen
    public GameObject UIWormHoleVideo;
    public GameObject UISpaceGateenter;
    public GameObject UIPLayerScore;
    public GameObject UIResumeButton;

    public TMP_Text PlayerWon;
    public TMP_Text PlayerwonLeft;

    //public int NumberOfPlayersStillPlaying = 4; // used to count how many still on the board


    // added into handle player moving off board
    bool scoreMe = false;

    // added in to keep record of tiles to move across
    Tile[] moveQueue;
    public int moveQueueIndex;

    bool isAnimating = false;

    Player ThePlayers;
    private readonly object PlayerShipss;
    Tile theTiles;

    int NumberOfPlayersStillPlaying1 = StateManger.NumberOfPlayersStillPlaying;
    //  DiceRoller theDiceRoller;



    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManger>();
      //   StateManger Statemanger = theStateManager.GetComponent<StateManger>();
        thePlayerWon = GameObject.FindObjectOfType<PlayerWon>();
        //  theChanceCards = GameObject.FindObjectOfType<ChanceCards>();
        // if (Statemanger.NumberOfPlayersStillPlaying == 1)

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
            //if (nextTile == null)
            //  {
            // we are being scored move player off board
            //TODO something for end game winner, maybe move to winner area ?
            //    SetNewTargetPosition(this.transform.position + Vector3.right * 1f);

            // }
           // if (currentTile.IsScoringSpace == true)
            //{
            //    this.scoreMe = true;
            //    this.isAnimating = false;
            //    moveQueue = null;
            //    Debug.Log("this.scoreMe = true;");
           // }
            //else
            //{
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
            //}

        }
        else
        {

            //the movement queue is empty, we are done moving
          //  Debug.Log("Done animating.");
            
            this.isAnimating = false;
            theStateManager.AnimationsPlaying --;
            

            if (currentTile != null && currentTile.IsRollAgain)
            {
                theStateManager.RollAgain();
            }

            if (currentTile != null && currentTile.ChanceCard)
            {
               theStateManager.ChanceCard();
                
            }

            if (currentTile.IsScoringSpace == true)
                {

                //while (isWon == false)
                //  {
                // thePlayerWon.isPlayerWon = true;
                //  thePlayerWon.PlayerWon1();
                //  }
              //  UIPLayerScore.SetActive(true);
               // isPlayerWon = true;
                // actually pause the game
              //  Time.timeScale = 0f;
             //   this.isAnimating = false;
               //     moveQueue = null;
                this.scoreMe = true;
                Debug.Log("this.scoreMe = true;");
                //theStateManager.AnimationsPlaying++;
                //StartCoroutine(PLayerScore());
               // WonMenu();

                 }


                if (currentTile != null && currentTile.Spacegate2 || currentTile.Spacegate5 || currentTile.Spacegate8 || currentTile.Spacegate12 || currentTile.Spacegate38 || currentTile.Spacegate53 || currentTile.Spacegate55 || currentTile.Spacegate62)
              {

                //Debug.Log("Wopuld have done wormhole");            
               WormholeFunction();
                Debug.Log("Anim Count " + theStateManager.AnimationsPlaying);
               // StopAllCoroutines();

            }


            /*   StartCoroutine(EnterSpaceGate());
                  // Debug.Log("Finished wait");

                  WormholeDest = spacegate24;
                  theStateManager.AnimationsPlaying++;
*/

            /*  if (currentTile != null && currentTile.Spacegate5)
              {
                  StartCoroutine(EnterSpaceGate());
                  // Debug.Log("Finished wait");

                  WormholeDest = spacegate48;
                  theStateManager.AnimationsPlaying++;

              }

              if (currentTile != null && currentTile.Spacegate8)
              {

                  StartCoroutine(EnterSpaceGate());
                  // Debug.Log("Finished wait");

                  WormholeDest = spacegate28;
                  theStateManager.AnimationsPlaying++;


              }

              if (currentTile != null && currentTile.Spacegate12)
              {
                  StartCoroutine(EnterSpaceGate());
                  // Debug.Log("Finished wait");

                  WormholeDest = spacegate52;
                  theStateManager.AnimationsPlaying++;


              }

              if (currentTile != null && currentTile.Spacegate38)
              {
                  StartCoroutine(EnterSpaceGate());
                  // Debug.Log("Finished wait");

                  WormholeDest = spacegate65;
                  theStateManager.AnimationsPlaying++;


              }

              if (currentTile != null && currentTile.Spacegate53)
              {
                  StartCoroutine(EnterSpaceGate());
                  // Debug.Log("Finished wait");

                  WormholeDest = spacegate72;
                  theStateManager.AnimationsPlaying++;


              }
              if (currentTile != null && currentTile.Spacegate55)
              {
                  StartCoroutine(EnterSpaceGate());
                  // Debug.Log("Finished wait");

                  WormholeDest = spacegate93;
                  theStateManager.AnimationsPlaying++;


              }

              if (currentTile != null && currentTile.Spacegate62)
              {
                  StartCoroutine(EnterSpaceGate());
                  // Debug.Log("Finished wait");

                  WormholeDest = spacegate78;
                  theStateManager.AnimationsPlaying++;


              }
              */
            if (currentTile != null && currentTile.Wormhole33)
            {
                StartCoroutine(EnterWormHole());
                // Debug.Log("Finished wait");

                WormholeDest = wormhole7;
                theStateManager.AnimationsPlaying++;
                transform.Rotate(0, 180, 0);


            }
            if (currentTile != null && currentTile.Wormhole54)
            {
                StartCoroutine(EnterWormHole());
                // Debug.Log("Finished wait");

                WormholeDest = wormhole17;
                theStateManager.AnimationsPlaying++;


            }

            if (currentTile != null && currentTile.Wormhole63)
            {
                StartCoroutine(EnterWormHole());
                // Debug.Log("Finished wait");

                WormholeDest = wormhole35;
                theStateManager.AnimationsPlaying++;
                transform.Rotate(0, 180, 0);


            }

            if (currentTile != null && currentTile.Wormhole76)
            {
                StartCoroutine(EnterWormHole());
                // Debug.Log("Finished wait");

                WormholeDest = wormhole22;
                theStateManager.AnimationsPlaying++;
                transform.Rotate(0, 180, 0);


            }

            if (currentTile != null && currentTile.Wormhole84)
            {
                StartCoroutine(EnterWormHole());
                // Debug.Log("Finished wait");

                WormholeDest = wormhole45;
                theStateManager.AnimationsPlaying++;


            }

            if (currentTile != null && currentTile.Wormhole89)
            {
                StartCoroutine(EnterWormHole());
                // Debug.Log("Finished wait");

                WormholeDest = wormhole9;
                theStateManager.AnimationsPlaying++;


            }
            if (currentTile != null && currentTile.Wormhole95)
            {
                StartCoroutine(EnterWormHole());
                // Debug.Log("Finished wait");

                WormholeDest = wormhole68;
                theStateManager.AnimationsPlaying++;
                transform.Rotate(0, 180, 0);


            }
            if (currentTile != null && currentTile.Wormhole98)
            {
                StartCoroutine(EnterWormHole());
                // Debug.Log("Finished wait");

                WormholeDest = wormhole79;
                theStateManager.AnimationsPlaying++;
                transform.Rotate(0, 180, 0);


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
        Debug.Log("$$$$$$$$$$$$$$$$ startingTile Move me");
        

                    if (theStateManager.CurrentPlayerId != PlayerId)
                {
                    Debug.Log("Player ID mismatch");
                    return;  // its not my turn

                }


        if (theStateManager.IsDoneRolling == false)
        {
            //we cant move yet

            // Have we rolled the dice ?

       //     Debug.Log("We have not finshd rolling");

            //CHECK TO MAKE SURE THERE IS NO ui OBJECT IN THE WAY
            return;

        }
        if (theStateManager.IsDoneClicking == true)
        {
            // we have already done a move
       //     Debug.Log("Done cliking is true");
            return;
        }


        int spacesToMove = theStateManager.DiceTotal;
     //   Debug.Log("space to move " + spacesToMove);
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
         //       Debug.Log("IsScoring Move me");
                finalTile.PlayerShips = this;
            }

            if (finalTile.IsScoringSpace == true)   
            {
         //       Debug.Log("IsScoring true");
         //       Debug.Log("Setting player to skipp all turn");
                if (theStateManager.CurrentPlayerId == 0 )  // if player id and skip id match then skip the turn
                {
                    ThePlayers.player1_hum_comp = 2;
                    



                    //  UISkipTurnMessage.SetActive(false);
                }

                if (theStateManager.CurrentPlayerId == 1 )
                {

                    ThePlayers.player2_hum_comp = 2;



                    //   UISkipTurnMessage.SetActive(false);
                }

                if (theStateManager.CurrentPlayerId == 2 )
                {

                    ThePlayers.player3_hum_comp = 2;



                    //  UISkipTurnMessage.SetActive(false);
                }

                if (theStateManager.CurrentPlayerId == 3 )
                {

                    ThePlayers.player4_hum_comp = 2;



                    //  UISkipTurnMessage.SetActive(false);
                }


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
         //   Debug.Log("CanLegallyMoveAhead is scoroing");
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
            /// orig code return false;
            /// // force to true to fix bug
            Debug.Log("SHould be doing destinationTile.PlayerShips.PlayerId");
             return true;

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



        Debug.Log("destinationTile" + destinationTile);

        return true;
    }

    public void ReturnToStorage()
    {
        // if we had defined stonestorage this is where t wouldreturn it to storage
      //  Debug.Log("Return to storage was activated");

    }

   
    IEnumerator UIWormholeenter()
    {

              
        theStateManager.UIWormhole.SetActive(true);
        yield return new WaitForSeconds(2);
        
        theStateManager.UIWormhole.SetActive(false);
        theStateManager.AnimationsPlaying--;
        Debug.Log("UIWormholeenter AnimationsPlaying " + theStateManager.AnimationsPlaying);
        yield return new WaitForSeconds(2);
        

    }

    IEnumerator UISpaceGate()
    {


        theStateManager.UISpaceGateenter.SetActive(true);
        yield return new WaitForSeconds(2);

        theStateManager.UISpaceGateenter.SetActive(false);
        theStateManager.AnimationsPlaying--;
        Debug.Log("UIWormholeenter AnimationsPlaying " + theStateManager.AnimationsPlaying);
        yield return new WaitForSeconds(2);
   

    }
  /*  public IEnumerator PauseGame (float Pausetime)
    {
       // Debug.Log("instide PauseGame into");
        Time.timeScale = 0f;
       // wormholeenter = 1;


        float pauseEndTime = Time.realtimeSinceStartup + Pausetime;
     
    
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
     
        Time.timeScale = 1f;
      //  Debug.Log("Done with Pause into");
       // wormholeenter = 5;




    }
    */
  /*  public IEnumerator PauseGameexit(float Pausetimeexit)
    {
     //   Debug.Log("instide PauseGame exit");
        Time.timeScale = 0f;
        float pauseEndTimeexit = Time.realtimeSinceStartup + Pausetimeexit;
        while (Time.realtimeSinceStartup < pauseEndTimeexit)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
    //    Debug.Log("Done with Pause eit");
        theStateManager.AnimationsPlaying--;

    }
*/

    IEnumerator EnterWormHole()
    {
        //   Debug.Log("EnterWormHole function");
        theStateManager.AnimationsPlaying++;
        UIWormhole.SetActive(true);
        
        // wait one sec
        yield return new WaitForSeconds(1);
        UIWormhole.SetActive(false);
        UIWormHoleVideo.SetActive(true);
        yield return new WaitForSeconds(1);
        wormhometravel();
       StartCoroutine(JustWaitUICoroutine());
      //  StopCoroutine(JustWaitUICoroutine());
    }

    IEnumerator EnterSpaceGate()
    {
        //   Debug.Log("Spacegate function");
        theStateManager.AnimationsPlaying++;
        UISpaceGateenter.SetActive(true);

        // wait one sec
        yield return new WaitForSeconds(1);
        UISpaceGateenter.SetActive(false);
        UIWormHoleVideo.SetActive(true);
        yield return new WaitForSeconds(1);
        wormhometravel();
     //  theStateManager.AnimationsPlaying--;
      StartCoroutine(JustWaitUICoroutine());
        //  StopCoroutine(JustWaitUICoroutine());

    }

    public void wormhometravel()
    {
     //   Debug.Log("Wormhole function");
                Tile finalTile = moveQueue[moveQueue.Length - 1];
                    finalTile = WormholeDest;
        
        this.transform.position = finalTile.transform.position;
       // UIWormHoleVideo.SetActive(true);
        this.targetPosition = finalTile.transform.position;     // this set the tagetfixd to new tile
       
        currentTile = finalTile;
                    moveQueue = null;
                    moveQueueIndex = 0;
        theStateManager.AnimationsPlaying--;
        UIWormHoleVideo.SetActive(false);

    }

    IEnumerator JustWaitUICoroutine()
    {
        //    Debug.Log(" PLayer JustWaitUICoroutine");
        // wait one sec




        // yield return new WaitForSecondsRealtime(2);
        //yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(2);
        theStateManager.AnimationsPlaying--;
    //    Debug.Log("Just Wait playerships )");



    }

  //  IEnumerator JustWaitUICoroutine_noAnim()
  //  {
        //    Debug.Log(" PLayer JustWaitUICoroutine");
        // wait one sec




        // yield return new WaitForSecondsRealtime(2);
        //yield return new WaitForSeconds(2);
 //       yield return new WaitForSeconds(2);
        
        //    Debug.Log("Just Wait playerships )");



 //   }

    IEnumerator PLayerScore()
    {
        // display mesage
        UIPLayerScore.SetActive(true);
        // wait one sec
        yield return new WaitForSeconds(5f);
        UIPLayerScore.SetActive(false);
        StartCoroutine(JustWaitUICoroutine());
    }

    public void EndGame()
    {
       
        Debug.Log("Level complete");
       
        int NumberOfPlayersStillPlaying1 = StateManger.NumberOfPlayersStillPlaying;
        bool SomebodyWon1 = StateManger.SomebodyWon;

        // Decrease the player count as finished playing
        if (PlayerId == 0 )
        {
            if (SomebodyWon1 == false)
            {
                PlayerWon.text = "You have Won !! Congratulations. ";
            }
            else
            {
                PlayerWon.text = "Player1 has Finished ";
                
            }
            NumberOfPlayersStillPlaying1--;
            SomebodyWon1 = true;


        }

        if (PlayerId == 1)
        {
            if (SomebodyWon1 == false)
            {
                PlayerWon.text = "You have Won !! Congratulations. ";
            }
            else
            {
                
                PlayerWon.text = "Player2 has Finished ";
                
            }
            NumberOfPlayersStillPlaying1--;
            SomebodyWon1 = true;
        }

        if (PlayerId == 2)
        {
            if (SomebodyWon1 == false)
            {
                PlayerWon.text = "You have Won !! Congratulations. ";
            }
            else
            {
                PlayerWon.text = "Player3 has Finished ";
            }

                NumberOfPlayersStillPlaying1--;
            SomebodyWon1 = true;

            
        }

        if (PlayerId == 3)
        {

            if (SomebodyWon1 == false)
            {
                PlayerWon.text = "You have Won !! Congratulations. ";
            }
            else
            {
                PlayerWon.text = "Player4 has Finished ";
            }
            NumberOfPlayersStillPlaying1--;
            SomebodyWon1 = true;

            
        }

        StateManger.NumberOfPlayersStillPlaying = NumberOfPlayersStillPlaying1;
        StateManger.SomebodyWon = SomebodyWon1;

        // Who has won the race


        // Who is left still playing
        if (StateManger.NumberOfPlayersStillPlaying == 0)
        {
            PlayerwonLeft.text = "There are no more players playing";
            UIResumeButton.SetActive(false);
            //ebug.Log("Number of players" + theStateManager.NumberOfPlayersStillPlaying);
        }
           else
           if (StateManger.NumberOfPlayersStillPlaying == 1)
            {
            PlayerwonLeft.text = " There are 1 players still playing";
           // Debug.Log("Number of players" + theStateManager.NumberOfPlayersStillPlaying);
        }
            else
            if (StateManger.NumberOfPlayersStillPlaying == 2)
        {
            PlayerwonLeft.text = " There are 2 players still playing";
           // Debug.Log("Number of players" + theStateManager.NumberOfPlayersStillPlaying);
        }
        else
            if (StateManger.NumberOfPlayersStillPlaying == 3)
        {
            PlayerwonLeft.text = " There are 3 players still playing";
           // Debug.Log("Number of players" + theStateManager.NumberOfPlayersStillPlaying);
        }

        
        

        UIPLayerScore.SetActive(true);
      
        Time.timeScale = 0f;

    
        
    }

    void WormholeFunction()
    {


        if (currentTile.Spacegate2)
        {
            WormholeDest = spacegate24;
        }

        if (currentTile.Spacegate5)
        {
            WormholeDest = spacegate48;
        }

        if (currentTile.Spacegate8)
        {
            WormholeDest = spacegate28;
        }

        if (currentTile.Spacegate12)
        {
            WormholeDest = spacegate52;
        }

        if (currentTile.Spacegate38)
        {
            WormholeDest = spacegate65;
            transform.Rotate(0, 180, 0);
        }
        if (currentTile.Spacegate53)
        {
            WormholeDest = spacegate72;
        }
        if (currentTile.Spacegate55)
        {
            WormholeDest = spacegate93;
        }
        if (currentTile.Spacegate62)
        {
            WormholeDest = spacegate78;
            transform.Rotate(0, 180, 0);
        }
                                 
        StartCoroutine(EnterSpaceGate());
        



        theStateManager.AnimationsPlaying++;

    }


  
}

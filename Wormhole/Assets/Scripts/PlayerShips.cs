using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;

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

    bool playerturning = false;

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

    public Camera MainCamera;            //define the main camera
                                         //define the player follow cameras
    public Camera ThePlayer1_camera;
    public Camera ThePlayer2_camera;
    public Camera ThePlayer3_camera;
    public Camera ThePlayer4_camera;

    //   Tile WormholeDest;

    public int PlayerId;




    public GameObject UIWormhole;   //game object for skip turn on screen
    public GameObject UIWormHoleVideo;
    public GameObject UISpaceGateenter;
    public GameObject UIPLayerScore;
    public GameObject UIResumeButton;

    public TMP_Text PlayerWon;
    public TMP_Text PlayerwonLeft;


    // Fireworks
    public ParticleSystem Fireworks1;
    public ParticleSystem Fireworks2;
    public ParticleSystem Fireworks3;
    public ParticleSystem Fireworks4;

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

       // Fireworks1 = GetComponent<ParticleSystem>();

        //Fireworks1.Stop();
    //    Fireworks1.enabled = false;
        //  Fireworks1.emission == enabled;

        Fireworks1 = GetComponent<ParticleSystem>();
        Fireworks2 = GetComponent<ParticleSystem>();
        Fireworks3 = GetComponent<ParticleSystem>();
        Fireworks4 = GetComponent<ParticleSystem>();
        // Fireworks1.Stop();
        // particleAuraPlay();
    }




   // public void particleAuraPlay()
   // {
  //      if (!Fireworks1.isPlaying)
   //     {
   //         Fireworks1.Play();
   //     }
   // }



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
                theStateManager.AnimationsPlaying++;
                //theStateManager.AnimationsPlaying++;
                //StartCoroutine(PLayerScore());
                // WonMenu();
                StartCoroutine(RunAsyncFromCoroutineTest());
                // UIPLayerScore.SetActive(true);

                //   Time.timeScale = 0f;
                Debug.Log("Stopping Fireworks");
                StopFireworks();
            }


            if (currentTile != null && currentTile.Spacegate2 || currentTile.Spacegate5 || currentTile.Spacegate8 || currentTile.Spacegate12 || currentTile.Spacegate38 || currentTile.Spacegate53 || currentTile.Spacegate55 || currentTile.Spacegate62)
              {

                //Debug.Log("Wopuld have done wormhole"); 
               // this.
                WormholeFunction();
                Debug.Log("Anim Count " + theStateManager.AnimationsPlaying);
               // StopAllCoroutines();

            }


           if (currentTile != null && currentTile.Wormhole33 || currentTile.Wormhole54 || currentTile.Wormhole63 || currentTile.Wormhole76 || currentTile.Wormhole84 || currentTile.Wormhole89 || currentTile.Wormhole95 || currentTile.Wormhole98 )
           {
                Debug.Log("Wopuld have done sPACEGATE");
               SpacegateFunction();
               Debug.Log("Anim Count " + theStateManager.AnimationsPlaying);
           }

            /*
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
               


            }
           */


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
            Debug.Log("final tile is null");
            // we are scoring this player
            scoreMe = true;
        }
        else
        {
            if (CanLegallyMoveTo(finalTile) == false)
            {
                Debug.Log("setting move queue etc");
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

        Debug.Log("Playerships CanLegallyMoveTo");
        // does the tilealready have a stone - not needed for me
        // is this one of our stones - not needed for me
        // is this a safe tile for enemy ?
        if (destinationTile == null)
        {
            Debug.Log("We are trying to move off baord which is not leagl.");
            // Note NULL tile means we are overshooting the victory roll and not legal
            return false;

            
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
        yield return new WaitForSeconds(2);
        UIWormhole.SetActive(false);
        //  UIWormHoleVideo.SetActive(true);
        theStateManager.disableAllCamera();
        MainCamera.enabled = true;
        
        yield return new WaitForSeconds(2);
        wormhometravel();
        MainCamera.enabled = false;
        if (PlayerId == 0)
        {
            ThePlayer1_camera.enabled = true;
        }
        StartCoroutine(JustWaitUICoroutine());
       
        //  StopCoroutine(JustWaitUICoroutine());
    }

    IEnumerator EnterSpaceGate()
    {
          Debug.Log("Spacegate function");
        theStateManager.AnimationsPlaying++;
        UISpaceGateenter.SetActive(true);

        // wait one sec
        yield return new WaitForSeconds(2);
        UISpaceGateenter.SetActive(false);
        //    UIWormHoleVideo.SetActive(true);
        theStateManager.disableAllCamera();
        MainCamera.enabled = true;
        yield return new WaitForSeconds(2);
        spacegatetravel();
        MainCamera.enabled = false;

        //  theStateManager.AnimationsPlaying--;
        StartCoroutine(JustWaitUICoroutine());
        //  StopCoroutine(JustWaitUICoroutine());

    }


   

    public void wormhometravel()
    {


      //  if (PlayerId == 0)
      //  {
      //      Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();
//
      //      ThePlayer1_camera.enabled = false;
     //   }
        //   Debug.Log("Wormhole function");
        Tile finalTile = moveQueue[moveQueue.Length - 1];
                    finalTile = WormholeDest;
        
        this.transform.position = finalTile.transform.position;
       // UIWormHoleVideo.SetActive(true);
        this.targetPosition = finalTile.transform.position;     // this set the tagetfixd to new tile
       
        currentTile = finalTile;
                    moveQueue = null;
                    moveQueueIndex = 0;
        if (PlayerId == 0)
        {
            Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();
        
            ThePlayer1_camera.enabled = true;
        }

        if (PlayerId == 1)
        {
            
            Camera ThePlayer2_camera = GameObject.Find("Player2_Follow_Camera(Clone)").GetComponent<Camera>();
            
            ThePlayer2_camera.enabled = true;
        }

        if (PlayerId == 2)
        {

            Camera ThePlayer3_camera = GameObject.Find("Player3_Follow_Camera(Clone)").GetComponent<Camera>();

            ThePlayer3_camera.enabled = true;
        }

        if (PlayerId == 3)
        {

            Camera ThePlayer4_camera = GameObject.Find("Player4_Follow_Camera(Clone)").GetComponent<Camera>();

            ThePlayer4_camera.enabled = true;
        }


        if (playerturning == true)
        {
            transform.Rotate(0, 180, 0);
            playerturning = false;
        }

        theStateManager.AnimationsPlaying--;
      //  UIWormHoleVideo.SetActive(false);
       

    }


    public void spacegatetravel()
    {


        //  if (PlayerId == 0)
        //  {
        //      Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();
        //
        //      ThePlayer1_camera.enabled = false;
        //   }
        //   Debug.Log("Wormhole function");
        Tile finalTile = moveQueue[moveQueue.Length - 1];
        finalTile = WormholeDest;

        this.transform.position = finalTile.transform.position;
        // UIWormHoleVideo.SetActive(true);
        this.targetPosition = finalTile.transform.position;     // this set the tagetfixd to new tile

        currentTile = finalTile;
        moveQueue = null;
        moveQueueIndex = 0;
        if (PlayerId == 0)
        {
            Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();

            ThePlayer1_camera.enabled = true;
        }

        if (PlayerId == 1)
        {

            Camera ThePlayer2_camera = GameObject.Find("Player2_Follow_Camera(Clone)").GetComponent<Camera>();

            ThePlayer2_camera.enabled = true;
        }

        if (PlayerId == 2)
        {

            Camera ThePlayer3_camera = GameObject.Find("Player3_Follow_Camera(Clone)").GetComponent<Camera>();

            ThePlayer3_camera.enabled = true;
        }

        if (PlayerId == 3)
        {

            Camera ThePlayer4_camera = GameObject.Find("Player4_Follow_Camera(Clone)").GetComponent<Camera>();

            ThePlayer4_camera.enabled = true;
        }


        if (playerturning == true)
        {
            transform.Rotate(0, 180, 0);
            playerturning = false;
        }


        theStateManager.AnimationsPlaying--;
        //  UIWormHoleVideo.SetActive(false);


    }

    IEnumerator JustWaitUICoroutine()
    {
           Debug.Log(" PLayer JustWaitUICoroutine");
        // wait one sec




        // yield return new WaitForSecondsRealtime(2);
        //yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(2);
        theStateManager.AnimationsPlaying--;
    //    Debug.Log("Just Wait playerships )");



    }

    IEnumerator JustWaitUICoroutine2()
    {
        Debug.Log(" PLayer JustWaitUICoroutine");
        // wait one sec




        // yield return new WaitForSecondsRealtime(2);
        //yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(2);
       // theStateManager.AnimationsPlaying--;
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



    //    IEnumerator Start_Fireworks()
    // IEnumerator Start_Fireworks()
    //  {
    //      Debug.Log("Start_Fireworks Begin");
    //      //Fireworks1.Stop();
    //  Fireworks1.enabled = false;
    //      yield return new WaitForSeconds(5f);
    //      Debug.Log("Start_Fireworks end");

    // Fireworks1.GetComponent<ParticleSystem>().emission.enabled = false;

    //   }






    IEnumerator RunAsyncFromCoroutineTest()
    {
        Debug.Log("Waiting 1 second...");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Waiting 1 second again...");
        yield return RunAsyncFromCoroutineTest2().AsIEnumerator();
        Debug.Log("################################Done");

        if (StateManger.NumberOfPlayersStillPlaying == 0)
        {
            PlayerwonLeft.text = "There are no more players playing";
            theStateManager.AnimationsPlaying++;
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

        Debug.Log("anim Count " + theStateManager.AnimationsPlaying);
        UIPLayerScore.SetActive(true);

           Time.timeScale = 0f;
        theStateManager.AnimationsPlaying--;
        Debug.Log("anim Count end " + theStateManager.AnimationsPlaying);
        Debug.Log("next try firework strop");
        StopFireworks();
    }

    async Task RunAsyncFromCoroutineTest2()
    {
        await new WaitForSeconds(0.5f);
    }

    void StartFireworks()
    {
        Debug.Log("in fioreworks function");
       
         Fireworks1.Play();
        Fireworks2.Play();
        Fireworks3.Play();
        Fireworks4.Play();


    }

    void StopFireworks()
    {
        Debug.Log("stop in fioreworks function");

        Fireworks1.Stop();
        Fireworks2.Stop();
        Fireworks3.Stop();
        Fireworks4.Stop();
        



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
                StartFireworks();
                PlayerWon.text = "You have Won !! Congratulations. ";
                Debug.Log("Player text set");
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
                StartFireworks();
                PlayerWon.text = "You have Won !! Congratulations. ";
                Debug.Log("Player text set");


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
                StartFireworks();
                PlayerWon.text = "You have Won !! Congratulations. ";
                Debug.Log("Player text set");
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
                StartFireworks();
                PlayerWon.text = "You have Won !! Congratulations. ";
                Debug.Log("Player text set");
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

        Debug.Log("Determine who still playing");
        // Who is left still playing
        if (StateManger.NumberOfPlayersStillPlaying == 0)
        {
            PlayerwonLeft.text = "There are no more players playing";
            theStateManager.AnimationsPlaying++;
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

      //  Debug.Log("Setting move queue to be null -- TEMP");
    //    moveQueue = null;
        // StartFireworks();


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
            //  transform.Rotate(0, 180, 0);
            playerturning = true;
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
            playerturning = true;
           // transform.Rotate(0, 180, 0);
        }
                                 
        StartCoroutine(EnterSpaceGate());
       




        theStateManager.AnimationsPlaying++;

    }


    void SpacegateFunction()
    {

        if (currentTile.Wormhole33)
        {

            WormholeDest = wormhole7;

            //   theStateManager.AnimationsPlaying++;
            //  transform.Rotate(0, 180, 0);
            playerturning = true;


        }
        if (currentTile.Wormhole54)
        {
           
            WormholeDest = wormhole17;
         //   theStateManager.AnimationsPlaying++;


        }

        if (currentTile.Wormhole63)
        {
            

            WormholeDest = wormhole35;
            // theStateManager.AnimationsPlaying++;
            // transform.Rotate(0, 180, 0);
            playerturning = true;


        }

        if (currentTile.Wormhole76)
        {
            

            WormholeDest = wormhole22;
         //   theStateManager.AnimationsPlaying++;
           // transform.Rotate(0, 180, 0);
            playerturning = true;


        }

        if (currentTile.Wormhole84)
        {
           

            WormholeDest = wormhole45;
          //  theStateManager.AnimationsPlaying++;


        }

        if (currentTile.Wormhole89)
        {
            

            WormholeDest = wormhole9;
          //  theStateManager.AnimationsPlaying++;


        }
        if (currentTile.Wormhole95)
        {
            

            WormholeDest = wormhole68;
            //  theStateManager.AnimationsPlaying++;
            //  transform.Rotate(0, 180, 0);
            playerturning = true;


        }
        if (currentTile.Wormhole98)
        {
            
            WormholeDest = wormhole79;
           // theStateManager.AnimationsPlaying++;



        }

        StartCoroutine(EnterWormHole());
        theStateManager.AnimationsPlaying++;
    }



}

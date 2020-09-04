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
    public Camera Wormhole_camera;

    //   Tile WormholeDest;

    public int PlayerId;
    private AudioSource source;
    public AudioClip SpaceGateEnter;
    public AudioClip WormholeGateEnter;
    public AudioClip Wins;
    public AudioClip Player1_announce;
    public AudioClip Player2_announce;
    public AudioClip Player3_announce;
    public AudioClip Player4_announce;


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
    



    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManger>();
      
        thePlayerWon = GameObject.FindObjectOfType<PlayerWon>();
   
        source = GetComponent<AudioSource>();

        targetPosition = this.transform.position;

        ThePlayers = GameObject.FindObjectOfType<Player>();
        theTiles = GameObject.FindObjectOfType<Tile>();

  

        Fireworks1 = GetComponent<ParticleSystem>();
        Fireworks2 = GetComponent<ParticleSystem>();
        Fireworks3 = GetComponent<ParticleSystem>();
        Fireworks4 = GetComponent<ParticleSystem>();
   
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
        else
        {

            //the movement queue is empty, we are done moving
          
            
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

        
                this.scoreMe = true;
                
                theStateManager.AnimationsPlaying++;
               
                StartCoroutine(RunAsyncFromCoroutineTest());
                

                //   Time.timeScale = 0f;
                ;
                StopFireworks();
            }


            if (currentTile != null && currentTile.Spacegate2 || currentTile.Spacegate5 || currentTile.Spacegate8 || currentTile.Spacegate12 || currentTile.Spacegate38 || currentTile.Spacegate53 || currentTile.Spacegate55 || currentTile.Spacegate62)
              {

                
             
                WormholeFunction();
                ;
               

            }


           if (currentTile != null && currentTile.Wormhole33 || currentTile.Wormhole54 || currentTile.Wormhole63 || currentTile.Wormhole76 || currentTile.Wormhole84 || currentTile.Wormhole89 || currentTile.Wormhole95 || currentTile.Wormhole98 )
           {
                
               SpacegateFunction();
               
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
          

            //CHECK TO MAKE SURE THERE IS NO ui OBJECT IN THE WAY
            return;

        }
        if (theStateManager.IsDoneClicking == true)
        {
            // we have already done a move
       
            return;
        }


        int spacesToMove = theStateManager.DiceTotal;
   
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

            if (finalTile.IsScoringSpace == true)   
            {
     
                if (theStateManager.CurrentPlayerId == 0 )  // if player id and skip id match then skip the turn
                {
                    ThePlayers.player1_hum_comp = 2;
                    



                    
                }

                if (theStateManager.CurrentPlayerId == 1 )
                {

                    ThePlayers.player2_hum_comp = 2;



                    
                }

                if (theStateManager.CurrentPlayerId == 2 )
                {

                    ThePlayers.player3_hum_comp = 2;



                    
                }

                if (theStateManager.CurrentPlayerId == 3 )
                {

                    ThePlayers.player4_hum_comp = 2;



                   
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
         
            return false;
        }

        Tile theTile = GetTileAhead(spacesToMove);

        return CanLegallyMoveTo(theTile);
    }

    bool CanLegallyMoveTo(Tile destinationTile)
    {


        if (destinationTile == null)
        {
           
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
            
             return true;

        }
        // if this is an enemy stone is it in a safe square ?
        // TODO Safe Squares ?

        if (destinationTile.IsRollAgain == true)
        {
            return false;
        }
        

        // if we have gotten here we can legaly land on the enmy stone and kick it off



        

        return true;
    }

    public void ReturnToStorage()
    {
        // if we had defined stonestorage this is where t wouldreturn it to storage
      

    }

   
    IEnumerator UIWormholeenter()
    {

              
        theStateManager.UIWormhole.SetActive(true);
        yield return new WaitForSeconds(2);
        
        theStateManager.UIWormhole.SetActive(false);
        theStateManager.AnimationsPlaying--;
        ;
        yield return new WaitForSeconds(2);
        

    }

    IEnumerator UISpaceGate()
    {


        theStateManager.UISpaceGateenter.SetActive(true);
        yield return new WaitForSeconds(2);

        theStateManager.UISpaceGateenter.SetActive(false);
        theStateManager.AnimationsPlaying--;
        
        yield return new WaitForSeconds(2);
   

    }
  
 

    IEnumerator EnterWormHole()
    {
        
        theStateManager.AnimationsPlaying++;
        UIWormhole.SetActive(true);
        
        // wait one sec
        yield return new WaitForSeconds(4);
        UIWormhole.SetActive(false);
      
        theStateManager.disableAllCamera();
        Wormhole_camera.enabled = true;
        source.PlayOneShot(WormholeGateEnter);
       
        yield return new WaitForSeconds(4);
        wormhometravel();
        Wormhole_camera.enabled = false;
        if (PlayerId == 0)
        {
            ThePlayer1_camera.enabled = true;
        }
        StartCoroutine(JustWaitUICoroutine());
       
        
    }

    IEnumerator EnterSpaceGate()
    {
        

       
        theStateManager.AnimationsPlaying++;
        UISpaceGateenter.SetActive(true);

        // wait one sec
        yield return new WaitForSeconds(2);
        UISpaceGateenter.SetActive(false);
        
        theStateManager.disableAllCamera();
        MainCamera.enabled = true;
        
        source.PlayOneShot(SpaceGateEnter);
        yield return new WaitForSeconds(2);
       
        spacegatetravel();
        MainCamera.enabled = false;

        
        StartCoroutine(JustWaitUICoroutine());
       

        
    }


   

    public void wormhometravel()
    {


     
        Tile finalTile = moveQueue[moveQueue.Length - 1];
                    finalTile = WormholeDest;
        
        this.transform.position = finalTile.transform.position;
       
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
      
       

    }


    public void spacegatetravel()
    {

        
        

        Tile finalTile = moveQueue[moveQueue.Length - 1];
        finalTile = WormholeDest;

        this.transform.position = finalTile.transform.position;
        
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
        
        


    }

    IEnumerator JustWaitUICoroutine()
    {
           
        // wait one sec




        yield return new WaitForSeconds(2);
        theStateManager.AnimationsPlaying--;
    



    }

    IEnumerator JustWaitUICoroutine2()
    {
        
        // wait one sec




        
        yield return new WaitForSeconds(2);
     



    }
 


  





    IEnumerator RunAsyncFromCoroutineTest()
    {
        
        yield return new WaitForSeconds(1.0f);
       
        yield return RunAsyncFromCoroutineTest2().AsIEnumerator();
        

        if (StateManger.NumberOfPlayersStillPlaying == 0)
        {
            PlayerwonLeft.text = "There are no more players playing";
            theStateManager.AnimationsPlaying++;
            UIResumeButton.SetActive(false);

            
        }
        else
           if (StateManger.NumberOfPlayersStillPlaying == 1)
        {
            PlayerwonLeft.text = " There are 1 players still playing";
          
        }
        else
            if (StateManger.NumberOfPlayersStillPlaying == 2)
        {
            PlayerwonLeft.text = " There are 2 players still playing";
            
        }
        else
            if (StateManger.NumberOfPlayersStillPlaying == 3)
        {
            PlayerwonLeft.text = " There are 3 players still playing";
           
        }

        
        UIPLayerScore.SetActive(true);

           Time.timeScale = 0f;
        theStateManager.AnimationsPlaying--;
       
        StopFireworks();
    }

    async Task RunAsyncFromCoroutineTest2()
    {
        await new WaitForSeconds(0.5f);
    }

    void StartFireworks()
    {
        
        
        Fireworks1.Play();
        Fireworks2.Play();
        Fireworks3.Play();
        Fireworks4.Play();
        

    }

    void StopFireworks()
    {
        

        Fireworks1.Stop();
        Fireworks2.Stop();
        Fireworks3.Stop();
        Fireworks4.Stop();
        



    }


    public void EndGame()
    {
       
 

        int NumberOfPlayersStillPlaying1 = StateManger.NumberOfPlayersStillPlaying;
        bool SomebodyWon1 = StateManger.SomebodyWon;

        // Decrease the player count as finished playing
        if (PlayerId == 0 )
        {
            if (SomebodyWon1 == false)
            {
                StartFireworks();
            
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
                StartFireworks();
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
                StartFireworks();
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
                StartFireworks();
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
            theStateManager.AnimationsPlaying++;
            UIResumeButton.SetActive(false);
            
            
        }
           else
           if (StateManger.NumberOfPlayersStillPlaying == 1)
            {
            PlayerwonLeft.text = " There are 1 players still playing";
           
        }
            else
            if (StateManger.NumberOfPlayersStillPlaying == 2)
        {
            PlayerwonLeft.text = " There are 2 players still playing";
           
        }
        else
            if (StateManger.NumberOfPlayersStillPlaying == 3)
        {
            PlayerwonLeft.text = " There are 3 players still playing";
           
        }

        


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
            ;
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
           
        }


       
        StartCoroutine(EnterSpaceGate());
        




        theStateManager.AnimationsPlaying++;

    }


    void SpacegateFunction()
    {

        if (currentTile.Wormhole33)
        {

            WormholeDest = wormhole7;

         
            playerturning = true;


        }
        if (currentTile.Wormhole54)
        {
           
            WormholeDest = wormhole17;
        


        }

        if (currentTile.Wormhole63)
        {
            

            WormholeDest = wormhole35;
           
            playerturning = true;


        }

        if (currentTile.Wormhole76)
        {
            

            WormholeDest = wormhole22;
       
            playerturning = true;


        }

        if (currentTile.Wormhole84)
        {
           

            WormholeDest = wormhole45;
          


        }

        if (currentTile.Wormhole89)
        {
            

            WormholeDest = wormhole9;
         


        }
        if (currentTile.Wormhole95)
        {
            

            WormholeDest = wormhole68;
           
            playerturning = true;


        }
        if (currentTile.Wormhole98)
        {
            
            WormholeDest = wormhole79;
          



        }

        StartCoroutine(EnterWormHole());
        theStateManager.AnimationsPlaying++;
    }



}

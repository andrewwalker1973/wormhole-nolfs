using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StateManger : MonoBehaviour
{


    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
        ThePlayers = GameObject.FindObjectOfType<Player>();
        ThePlayerships = GameObject.FindObjectOfType<PlayerShips>();
        TheDiceRoller = GameObject.FindObjectOfType<DiceRoller>();
        TheChanceOptions = GameObject.FindObjectOfType<ChanceOptions>();
       
        Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer2_camera = GameObject.Find("Player2_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer3_camera = GameObject.Find("Player3_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer4_camera = GameObject.Find("Player4_Follow_Camera(Clone)").GetComponent<Camera>();

        

        source = GetComponent<AudioSource>();





        PlayerAIs = new AIPlayer[NumberOfPlayers];
        //code added to force player
        



        setUpPlayers();   // run the function to determine who is human or computer
        notplaying();
        


    }

   

    Player ThePlayers;                  // gain access to player scrip vars
    DiceRoller TheDiceRoller;           // gain access to diceroller scrip vars
   
    public int NumberOfPlayers = 4; //max number of players playing
  public static int NumberOfPlayersStillPlaying = 4; // used to count how many still on the board
    public int CurrentPlayerId = 0;  //set current playerid = 0 Ie - Player1
    public int DiceTotal;           // Total all dice rolls

    AIPlayer[] PlayerAIs;           // define the array for the players
  


    public bool IsDoneRolling = false;  // is done rolling false - have we finshed rollling
    public bool IsDoneClicking = false; //have we finished clicking
  
    public bool IsDoneRERoll = false;  // Variable to monitor the reroll function
    public int AnimationsPlaying = 0; // control Animations playing sequence

    // below to manage the skip roll process 
    public bool IsSkipRoll1 = false;  // variable for player1 skip roll
    public bool IsSkipRoll2 = false;    // variable for player2 skip roll
    public bool IsSkipRoll3 = false;    // variable for player3 skip roll
    public bool IsSkipRoll4 = false;    // variable for player4 skip roll

    public int player1_skipping = 0;    // is player 1 skipping  0 = no 1 = yes
    public int player2_skipping = 0;    // is player 2 skipping  0 = no 1 = yes
    public int player3_skipping = 0;    // is player 3 skipping  0 = no 1 = yes
    public int player4_skipping = 0;    // is player 4 skipping  0 = no 1 = yes

    public static bool SomebodyWon;



    public GameObject NoLegalMovesPopup;   //game object for no legal moves on screen
    public GameObject UIRollAgainPopup;   //game object for roll again on screen
    public GameObject UISkipTurnMessage;   //game object for skip turn on screen
    public GameObject UIMoveAhead3;   //game object for skip turn on screen
    public GameObject UIMoveAhead6;   //game object for skip turn on screen
    public GameObject UIWormhole;   //game object for skip turn on screen
    public GameObject UISpaceGateenter;
   

    PlayerShips ThePlayerships;         // gain access to the playerships vars
    ChanceOptions TheChanceOptions;

   

    public TextMeshProUGUI PlayerSKipMessage;
 

    Player ThePlayer;
    Player ThePlayer1;

    //Code to allow for Camera Selection 
    public Camera MainCamera;            //define the main camera
    public Camera Wormhole_camera;


    //define the player follow cameras
    public Camera ThePlayer1_camera;
 public   Camera ThePlayer2_camera;
    public Camera ThePlayer3_camera;
    public Camera ThePlayer4_camera;

    private AudioSource source;
    public AudioClip Player1announce;
    public AudioClip Player2announce;
    public AudioClip Player3announce;
    public AudioClip Player4announce;
    public AudioClip PlayerRollagain;
    public AudioClip PlayerSkipTurn;
    public AudioClip PlayerMoveAhead3;
    public AudioClip PlayerMoveAhead5;
    

    //Create the string array for the chance card system
    public string[] ChanceCards1 =
   {
        "Roll Again",       // variable1  roll again
        "Skip Turn"     // Variable 2 skip turn        
    };

    //Setup the randomizer for the array
    Random rand = new Random();


    // Function to setup the players as human or computer
    void setUpPlayers()
    {

      //  ThePlayers.player1_hum_comp = 1;
      //  ThePlayers.player2_hum_comp = 1;
      //  ThePlayers.player3_hum_comp = 2;
      //  ThePlayers.player4_hum_comp = 2;

        //Is a human player value is  null 
        //  if  new AIPlayer(); is an AI PLAYER
        if (ThePlayers.player1_hum_comp == 0)
        {
            PlayerAIs[0] = null;
        }
        else
            if (ThePlayers.player1_hum_comp == 1 || ThePlayers.player1_hum_comp == 2)
        {
            PlayerAIs[0] = new AIPlayer();
        }
      
        if (ThePlayers.player2_hum_comp == 0)
        {
            PlayerAIs[1] = null;
        }
        else
            if (ThePlayers.player2_hum_comp == 1 || ThePlayers.player2_hum_comp == 2)
        {
            PlayerAIs[1] = new AIPlayer();
        }
      
        if (ThePlayers.player3_hum_comp == 0)
        {
            PlayerAIs[2] = null;
        }
        else
            if (ThePlayers.player3_hum_comp == 1 || ThePlayers.player3_hum_comp == 2)
        {
            PlayerAIs[2] = new AIPlayer();
        }
      
        if (ThePlayers.player4_hum_comp == 0)
        {
            PlayerAIs[3] = null;
        }
        else
            if (ThePlayers.player4_hum_comp == 1 || ThePlayers.player4_hum_comp == 2)
        {
            PlayerAIs[3] = new AIPlayer();
        }
     
        if (ThePlayers.player1_hum_comp == 2)
        {
            NumberOfPlayersStillPlaying--;
        }
        if (ThePlayers.player2_hum_comp == 2)
        {
            NumberOfPlayersStillPlaying--;
        }
        if (ThePlayers.player3_hum_comp == 2)
        {
            NumberOfPlayersStillPlaying--;
        }
        if (ThePlayers.player4_hum_comp == 2)
        {
            NumberOfPlayersStillPlaying--;
        }

        
    }


    //function to do a roll with noclick
    virtual protected void DoRoll()
    {
        // roll without clicking the button

        TheDiceRoller.RollTheDice();     // find the script diceroller and run the function "rollthedice
        UIRollAgainPopup.SetActive(false);

    }


    // fucntion for new turn of player
    public void NewTurn()
    {
      
        
       
            IsDoneRolling = false;
            IsDoneClicking = false;
        

            if (IsDoneRERoll == true)
            {
                UIRollAgainPopup.SetActive(false);
                IsDoneRERoll = false;
            }

           


            // TODO move to next player
            CurrentPlayerId = (CurrentPlayerId + 1) % NumberOfPlayers;   // rotate through the players
                                                                         //  Debug.Log("at increment of player ID");


          
          Camera_controls();      // Funtion to set the follow camera to follow playing player

        // Check if the Player is skipping a Turn


        notplaying();  // check if player is not playing

        if ((IsSkipRoll1 == true ) || (IsSkipRoll2 == true) || (IsSkipRoll3 == true) || (IsSkipRoll4 == true)) // if any are equal to true then player is skipping the turn
        {
            check_skip_roll();      // run the function to check and run the skip function

        }

        
        if (CurrentPlayerId == 0)
        {
            if (ThePlayers.player1_hum_comp == 0)
            {
                source.PlayOneShot(Player1announce);
                
            }
        }
        if (CurrentPlayerId == 1)
        {
            if (ThePlayers.player2_hum_comp == 0)
            {
                source.PlayOneShot(Player2announce);
               
            }
        }
        if (CurrentPlayerId == 2)
        {
            if (ThePlayers.player3_hum_comp == 0)
            {
                source.PlayOneShot(Player3announce);
               
            }
        }
        if (CurrentPlayerId == 3)
        {
            if (ThePlayers.player4_hum_comp == 0)
            {
                source.PlayOneShot(Player4announce);
                
            }
        }


    }


    // Function to check if player is skipping the turn and then skip the turn
   void check_skip_roll()
    {

       // TODO Find a way to show the player is skipping the turn
        if (CurrentPlayerId == 0 && player1_skipping == 1)  // if player id and skip id match then skip the turn
        {
            
            SkipTurn();                 // skip the turn
            player1_skipping = 0;       // set back to no skip setting
            IsSkipRoll1 = false;        // set back to no skip setting
            

          
        }

        if (CurrentPlayerId == 1 && player2_skipping == 1)
        {
            
            SkipTurn();
            player2_skipping = 0;
            IsSkipRoll2 = false;
            

            
        }

        if (CurrentPlayerId == 2 && player3_skipping == 1)
        {
            
            SkipTurn();
            player3_skipping = 0;
            IsSkipRoll3 = false;
            

           
        }

        if (CurrentPlayerId == 3 && player4_skipping == 1)
        {
            
            SkipTurn();
            player4_skipping = 0;
            IsSkipRoll4 = false;
            

            
        }

    }



    void notplaying()
    {

        // TODO Find a way to show the player is skipping the turn
        if (CurrentPlayerId == 0 && ThePlayers.player1_hum_comp == 2)  // if player id and skip id match then skip the turn
        {

            SkipTurn();                 // skip the turn
           


            
        }

        if (CurrentPlayerId == 1 && ThePlayers.player2_hum_comp == 2)
        {

            SkipTurn();
            


            
        }

        if (CurrentPlayerId == 2 && ThePlayers.player3_hum_comp == 2)
        {

            SkipTurn();
            


           
        }

        if (CurrentPlayerId == 3 && ThePlayers.player4_hum_comp == 2)
        {

            SkipTurn();
            


            
        }

    }
    // fucntion to skip the player turn
    void SkipTurn()
    {
       // set all the varibale equal to values when turn is done
        IsDoneRolling = true;
        IsDoneClicking = true;
    
      
    }

    // Update is called once per frame
    void Update()
    {
        // is the tunrn done ?
        if (IsDoneRolling && IsDoneClicking && AnimationsPlaying == 0)
        {

            NewTurn();          // run the new turn fucntion
            return;
        }


            if (PlayerAIs[CurrentPlayerId] != null)   // Process to run the AI script for AI players
            {
            
                PlayerAIs[CurrentPlayerId].DoAI();
            }
        

    }


    // Function to check that the move is legal
    public void CheckLegalMoves()
    {


        

        
            // if we roll a 0 we have no legal moves
            if (DiceTotal == 0)
            {
                
                StartCoroutine(NoLegalMovesCoroutine());
                return;

            }
            // Loop through all of a player's stones
            PlayerShips[] pss2 = GameObject.FindObjectsOfType<PlayerShips>();
            bool hasLegalMove = false;
            foreach (PlayerShips ps2 in pss2)
            {
                if (ps2.PlayerId == CurrentPlayerId)
                {
                    if (ps2.CanLegallyMoveAhead(DiceTotal))
                    {
                        // TODO: Highlight stones that can be legally moved
                        hasLegalMove = true;
                    }
                }
            }

            // If no legal moves are possible, wait a sec then move to next player (probably give message)
            if (hasLegalMove == false)
            {
            
                StartCoroutine(NoLegalMovesCoroutine());
                return;
            }


            //Highlight the ones that can be legal moved
            // if no logal moves wait a second then move to next player with message
            
      
    }

            // Function to display a UI message if there are no Legal moves available
    IEnumerator NoLegalMovesCoroutine()
        {
            // display mesage
            NoLegalMovesPopup.SetActive(true);
            // wait one sec
            yield return new WaitForSeconds(1f);
            NoLegalMovesPopup.SetActive(false);
            NewTurn();

        
    }

    IEnumerator SkipRollUICoroutine()
    {


       
        
        UISkipTurnMessage.SetActive(true);
        yield return new WaitForSeconds(2);
        UISkipTurnMessage.SetActive(false);
        yield return new WaitForSeconds(2);
        AnimationsPlaying--;

    }

    
        IEnumerator MoveAhead3RollUICoroutine()
    {


        
       UIMoveAhead3.SetActive(true);
      
        // wait one sec
        yield return new WaitForSeconds(2);
       UIMoveAhead3.SetActive(false);
        yield return new WaitForSeconds(2);
        DoChanceClick();
         StartCoroutine(JustWaitUICoroutine());

    }


    IEnumerator MoveAhead6RollUICoroutine()
    {


       
     UIMoveAhead6.SetActive(true);
    
        // wait one sec
        yield return new WaitForSeconds(2);
        UIMoveAhead6.SetActive(false);
        yield return new WaitForSeconds(2);
        DoChanceClick();
        StartCoroutine(JustWaitUICoroutine());

    }
    IEnumerator JustWaitUICoroutine()
    {

        // wait one sec
        
        
        

        yield return new WaitForSeconds(2);
      



    }


        public void disableAllCamera()
    {
        Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer2_camera = GameObject.Find("Player2_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer3_camera = GameObject.Find("Player3_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer4_camera = GameObject.Find("Player4_Follow_Camera(Clone)").GetComponent<Camera>();
      
        MainCamera.enabled = false;
        Wormhole_camera.enabled = false; 
    ThePlayer1_camera.enabled = false;
        ThePlayer2_camera.enabled = false;
        ThePlayer3_camera.enabled = false;
        ThePlayer4_camera.enabled = false;
     

    }


    


    //fucntion for the roll again fucntion
    public void RollAgain()
    {
        
        IsDoneRolling = false;
        IsDoneClicking = false;
       
        IsDoneRERoll = true;
        
    }


    // Function for the Chnace card system
    public void ChanceCard()
    {
      
        string chanceCards = ChanceCards1[Random.Range(0, ChanceCards1.Length)];
     
        // check what the value is and perform the fucntioin based on the string output
        if (chanceCards == "Roll Again")        // roll again chance selected
        {
            source.PlayOneShot(PlayerRollagain);
            UIRollAgainPopup.SetActive(true);       // dispaly roll again on UI
            IsDoneRERoll = true;            // Set the value for roll gain to be true so that the roll again functon willreset values
            
            RollAgain();                // run the roll again fucntion
           

        }

       
        //Skip Turn
        if (chanceCards == "Skip Turn")         // Chance cards selected
        {
            source.PlayOneShot(PlayerSkipTurn);
            AnimationsPlaying++;
            // based on the current player ID set the variables to skipp the next turn
            switch (CurrentPlayerId)
            {
                case 0:         // player id  is 0

                 
                    player1_skipping = 1;       // set to enable skip turn
                    IsSkipRoll1 = true;         // set to enable skip turn
                    PlayerSKipMessage.text = "Player1 will Skip Next Turn";
                    PlayerSKipMessage.enabled = true;


                    StartCoroutine(SkipRollUICoroutine());
                    StartCoroutine(JustWaitUICoroutine());



                    break;
                case 1:     // Player id = 1

                    
                    player2_skipping = 1;
                    IsSkipRoll2 = true;
                    PlayerSKipMessage.text = "Player2 will Skip Next Turn";
                    
                  StartCoroutine(SkipRollUICoroutine());
                    StartCoroutine(JustWaitUICoroutine());


                    break;
                case 2:

                    
                    player3_skipping = 1;
                    IsSkipRoll3 = true;
                    PlayerSKipMessage.text = "Player3 will Skip Next Turn";
                    
                    StartCoroutine(SkipRollUICoroutine());
                    StartCoroutine(JustWaitUICoroutine());


                    break;
                case 3:

                    player4_skipping = 1;
                    IsSkipRoll4 = true;
                    PlayerSKipMessage.text = "Player4 will Skip Next Turn";
                    
                   StartCoroutine(SkipRollUICoroutine());
                    StartCoroutine(JustWaitUICoroutine());


                    break;
                
            }

            StartCoroutine(JustWaitUICoroutine());
            

        }
        if (chanceCards == "MoveAhead3")
        {
            
            source.PlayOneShot(PlayerMoveAhead3);
            IsDoneRolling = true;
            IsDoneClicking = false;
          
            IsDoneRERoll = true;
            DiceTotal = 3;

            StartCoroutine(MoveAhead3RollUICoroutine());

     

        }

        if (chanceCards == "MoveAhead6")
        {
           

            // Set the variables to be the same a new roll start
            source.PlayOneShot(PlayerMoveAhead5);
            IsDoneRolling = true;
            IsDoneClicking = false;
          
            IsDoneRERoll = true;
            DiceTotal = 5;
      
           StartCoroutine(MoveAhead6RollUICoroutine());
        

        }


    }


    //Function to set the right follow camera to the righ player
    public void Camera_controls()
    {
        switch (CurrentPlayerId)
        {
            case 0:
                Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();   //find the follow camera for player1
                disableAllCamera();                 // disable all the cameras
               
                if (ThePlayerships.Player1_finished ==  false || IsSkipRoll1 == false)
                {
                    ThePlayer1_camera.enabled = true;
                }
                else
                {
                    ThePlayer1_camera.enabled = false;
                }

                ThePlayer1_camera.enabled = true;       // enable only player1 follow camera
                break;
            case 1:
                Camera ThePlayer2_camera = GameObject.Find("Player2_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();

                if (ThePlayerships.Player2_finished == false || IsSkipRoll2 == false)
                {
                    ThePlayer2_camera.enabled = true;
                }
                else
                {
                    ThePlayer2_camera.enabled = false;
                }
                           
                break;
            case 2:
                Camera ThePlayer3_camera = GameObject.Find("Player3_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();

                if (ThePlayerships.Player3_finished == false || IsSkipRoll3 == false)
                {
                    ThePlayer3_camera.enabled = true;
                }
                else
                {
                    ThePlayer3_camera.enabled = false;
                }

                break;
            case 3:
                Camera ThePlayer4_camera = GameObject.Find("Player4_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();
                
                if (ThePlayerships.Player4_finished == false || IsSkipRoll4 == false)
                {
                    ThePlayer4_camera.enabled = true;
                }
                else
                {
                    ThePlayer4_camera.enabled = false;
                }

                break;
        }
    }





    ///////////Chance Code
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

                        PlayerShips chancepickedStone = PickChanceStoneToMove(ChancelegalStones);
      
        chancepickedStone.MoveMe();            // run the moveme function in playerships script

                    }

  public   PlayerShips[] GetChanceLegalMoves()
                            {

                                List<PlayerShips> ChancelegalStones = new List<PlayerShips>();

                                if (DiceTotal == 0)   // make sure we dont roll a 0
                                {
                                    return ChancelegalStones.ToArray();
                                }

  




        PlayerShips[] pss1 = GameObject.FindObjectsOfType<PlayerShips>();

                                foreach (PlayerShips ps1 in pss1)             //check on every ship found to see if it can move
                                {
                                    if (ps1.PlayerId == CurrentPlayerId)
                                    {
                                  
               


                if (ps1.CanLegallyMoveAhead(DiceTotal))  // check if ship can move based on dice total display
                                        {
                                   
                                                ChancelegalStones.Add(ps1);   // send back values to array
                                        }
                else
                {
                    Debug.Log(" ");
                }
                                    }
                                }

                                return ChancelegalStones.ToArray();
                            }


  virtual protected PlayerShips PickChanceStoneToMove(PlayerShips[] ChancelegalStones)
            {

                return ChancelegalStones[Random.Range(0, 1)];


            }


   
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StateManger : MonoBehaviour
{
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



        PlayerAIs = new AIPlayer[NumberOfPlayers];

        
        //Code Added to force the required players
       ThePlayers.player1_hum_comp = 0;
        ThePlayers.player2_hum_comp = 0;
       ThePlayers.player3_hum_comp = 0;
        ThePlayers.player4_hum_comp = 0;


        setUpPlayers();   // run the function to determine who is human or computer

      

    }


    Player ThePlayers;                  // gain access to player scrip vars
    DiceRoller TheDiceRoller;           // gain access to diceroller scrip vars
   
    public int NumberOfPlayers = 4; //max number of players playing
    public int CurrentPlayerId = 0;  //set current playerid = 0 Ie - Player1
    public int DiceTotal;           // Total all dice rolls

    AIPlayer[] PlayerAIs;           // define the array for the players
  


    public bool IsDoneRolling = false;  // is done rolling false - have we finshed rollling
    public bool IsDoneClicking = false; //have we finished clicking
  //  public bool IsDoneAnimating = false; // Have we finshed moving
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



    public GameObject NoLegalMovesPopup;   //game object for no legal moves on screen
    public GameObject UIRollAgainPopup;   //game object for roll again on screen
    public GameObject UISkipTurnMessage;   //game object for skip turn on screen
    public GameObject UIMoveAhead3;   //game object for skip turn on screen
    public GameObject UIMoveAhead6;   //game object for skip turn on screen
    public GameObject UIWormhole;   //game object for skip turn on screen

    PlayerShips ThePlayerships;         // gain access to the playerships vars
    ChanceOptions TheChanceOptions;

    public TextMeshProUGUI PlayerSKipMessage;
   // public TextMeshProUGUI PlayerMoveAhead3;
  //  public TextMeshProUGUI PlayerMoveAhead6;

    Player ThePlayer;
    Player ThePlayer1;

    //Code to allow for Camera Selection 
    public Camera MainCamera;            //define the main camera
  

    //define the player follow cameras
 public Camera ThePlayer1_camera;
 public   Camera ThePlayer2_camera;
    public Camera ThePlayer3_camera;
    public Camera ThePlayer4_camera;

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


        //Is a human player value is  null 
        //  if  new AIPlayer(); is an AI PLAYER
        if (ThePlayers.player1_hum_comp == 0)
        {
            PlayerAIs[0] = null;
        }
        else
            if (ThePlayers.player1_hum_comp == 1)
        {
            PlayerAIs[0] = new AIPlayer(); 
        }


        if (ThePlayers.player2_hum_comp == 0)
        {
            PlayerAIs[1] = null;
        }
        else
            if (ThePlayers.player2_hum_comp == 1)
        {
            PlayerAIs[1] = new AIPlayer();
        }


        if (ThePlayers.player3_hum_comp == 0)
        {
            PlayerAIs[2] = null;
        }
        else
            if (ThePlayers.player3_hum_comp == 1)
        {
            PlayerAIs[2] = new AIPlayer();
        }


        if (ThePlayers.player4_hum_comp == 0)
        {
            PlayerAIs[3] = null;
        }
        else
            if (ThePlayers.player4_hum_comp == 1)
        {
            PlayerAIs[3] = new AIPlayer();
        }

    }


    //function to do a roll with noclick
    virtual protected void DoRoll()
    {
        // roll without clicking the button
        TheDiceRoller.RollTheDice();     // find the script diceroller and run the function "rollthedice

    }


    // fucntion for new turn of player
    public void NewTurn()
    {
      

            //start of a players turn
            IsDoneRolling = false;
            IsDoneClicking = false;
        //    IsDoneAnimating = false;

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

        if ((IsSkipRoll1 == true ) || (IsSkipRoll2 == true) || (IsSkipRoll3 == true) || (IsSkipRoll4 == true)) // if any are equal to true then player is skipping the turn
        {
            check_skip_roll();      // run the function to check and run the skip function

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
            

          //  UISkipTurnMessage.SetActive(false);
        }

        if (CurrentPlayerId == 1 && player2_skipping == 1)
        {
            
            SkipTurn();
            player2_skipping = 0;
            IsSkipRoll2 = false;
            

            //   UISkipTurnMessage.SetActive(false);
        }

        if (CurrentPlayerId == 2 && player3_skipping == 1)
        {
            
            SkipTurn();
            player3_skipping = 0;
            IsSkipRoll3 = false;
            

            //  UISkipTurnMessage.SetActive(false);
        }

        if (CurrentPlayerId == 3 && player4_skipping == 1)
        {
            
            SkipTurn();
            player4_skipping = 0;
            IsSkipRoll4 = false;
            

            //  UISkipTurnMessage.SetActive(false);
        }

    }

    // fucntion to skip the player turn
    void SkipTurn()
    {
       // set all the varibale equal to values when turn is done
        IsDoneRolling = true;
        IsDoneClicking = true;
     //   IsDoneAnimating = true;
      
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
            StartCoroutine( NoLegalMovesCoroutine() );
            return;

        }
        //loop through all the player stones
      //  bool hasLegalMove = false;

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


        //  GetComponent<TMP_Text>().text = "Hello";
        //  UISkipTurnMessage.SetActive(true);
        // wait one sec\
        Debug.Log("Skipping tunr co-routine");
        UISkipTurnMessage.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        UISkipTurnMessage.SetActive(false);
        yield return new WaitForSecondsRealtime(3);

    }

    
        IEnumerator MoveAhead3RollUICoroutine()
    {


        //  GetComponent<TMP_Text>().text = "Hello";
       UIMoveAhead3.SetActive(true);
       // PlayerMoveAhead3.enabled = true;
        // wait one sec
        yield return new WaitForSecondsRealtime(2);
       UIMoveAhead3.SetActive(false);
        yield return new WaitForSecondsRealtime(2);
        DoChanceClick();
         StartCoroutine(JustWaitUICoroutine());

    }


    IEnumerator MoveAhead6RollUICoroutine()
    {


        //  GetComponent<TMP_Text>().text = "Hello";
     UIMoveAhead6.SetActive(true);
    //    PlayerMoveAhead6.enabled = true;
        // wait one sec
        yield return new WaitForSecondsRealtime(2);
        UIMoveAhead6.SetActive(false);
        yield return new WaitForSecondsRealtime(2);
        DoChanceClick();
        StartCoroutine(JustWaitUICoroutine());

    }
    IEnumerator JustWaitUICoroutine()
    {

        // wait one sec
        Debug.Log("Just Wait)");
        
        

        yield return new WaitForSecondsRealtime(1);
        


    }


        public void disableAllCamera()
    {
        Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer2_camera = GameObject.Find("Player2_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer3_camera = GameObject.Find("Player3_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer4_camera = GameObject.Find("Player4_Follow_Camera(Clone)").GetComponent<Camera>();
        MainCamera.enabled = false;
        ThePlayer1_camera.enabled = false;
        ThePlayer2_camera.enabled = false;
        ThePlayer3_camera.enabled = false;
        ThePlayer4_camera.enabled = false;
     

    }


    


    //fucntion for the roll again fucntion
    public void RollAgain()
    {

        // Reset all the variables back to begin of turn setting
        //start of a players turn
        IsDoneRolling = false;
        IsDoneClicking = false;
       // IsDoneAnimating = false;
        IsDoneRERoll = true;
    }


    // Function for the Chnace card system
    public void ChanceCard()
    {
    
            // randomize through the chance string and select a value
        string chanceCards = ChanceCards1[Random.Range(0, ChanceCards1.Length)];
     
        // check what the value is and perform the fucntioin based on the string output
        if (chanceCards == "Roll Again")        // roll again chance selected
        {
                     
            UIRollAgainPopup.SetActive(true);       // dispaly roll again on UI
            IsDoneRERoll = true;            // Set the value for roll gain to be true so that the roll again functon willreset values
            
            RollAgain();                // run the roll again fucntion
            
        }

       
        //Skip Turn
        if (chanceCards == "Skip Turn")         // Chance cards selected
        {

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



        }
        if (chanceCards == "MoveAhead3")
        {
            Debug.Log("Move ahead 3 spaces");
          //  UIMoveAhead3.SetActive(true);
            // Set the variables to be the same a new roll start
           // UIMoveAhead3.SetActive(true);
         // UIRollAgainPopup.SetActive(true);
            // StartCoroutine(MoveAhead3RollUICoroutine());
            Debug.Log("move 3 enableade");
            IsDoneRolling = true;
            IsDoneClicking = false;
          //  IsDoneAnimating = false;
            IsDoneRERoll = true;
            DiceTotal = 3;
      //      PlayerMoveAhead3.text = "Player1 Move Ahead 3 Spaces";
        //   PlayerMoveAhead3.enabled = true;
        //    UIMoveAhead3.SetActive(true);
         //   UIRollAgainPopup.SetActive(true);
            StartCoroutine(MoveAhead3RollUICoroutine());

         //   DoChanceClick();
        //    StartCoroutine(JustWaitUICoroutine());
            //   UIMoveAhead3.SetActive(false);

        }

        if (chanceCards == "MoveAhead6")
        {
            Debug.Log("Move ahead 6 spaces");
           // UIRollAgainPopup.SetActive(true);
            //     PlayerMoveAhead6.enabled = true;
         //   UIMoveAhead6.SetActive(true);
          //  UIRoll.SetActive(true); 
            //    StartCoroutine(MoveAhead6RollUICoroutine());

            Debug.Log("move 6 enableade");

            // Set the variables to be the same a new roll start
            IsDoneRolling = true;
            IsDoneClicking = false;
          //  IsDoneAnimating = false;
            IsDoneRERoll = true;
            DiceTotal = 6;
       //     PlayerMoveAhead6.text = "Player1 Move Ahead 6 Spaces";
      //      PlayerMoveAhead6.enabled = true;
          //  UIMoveAhead6.SetActive(true);
            
          //  UIRollAgainPopup.SetActive(true);
           StartCoroutine(MoveAhead6RollUICoroutine());
         //   DoChanceClick();
         //   StartCoroutine(JustWaitUICoroutine());
            //  UIMoveAhead6.SetActive(false);


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
                ThePlayer1_camera.enabled = true;       // enable only player1 follow camera
                break;
            case 1:
                Camera ThePlayer2_camera = GameObject.Find("Player2_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();
                ThePlayer2_camera.enabled = true;           
                break;
            case 2:
                Camera ThePlayer3_camera = GameObject.Find("Player3_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();
                ThePlayer3_camera.enabled = true;
                break;
            case 3:
                Camera ThePlayer4_camera = GameObject.Find("Player4_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();
                ThePlayer4_camera.enabled = true;                
                break;
        }
    }





    ///////////Chance Cod
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
                        Debug.Log("Picked Stone " + pickedStone);
      //  UIMoveAhead3.SetActive(false);
     //   UIMoveAhead6.SetActive(false);
        pickedStone.MoveMe();            // run the moveme function in playerships script

                    }

  public   PlayerShips[] GetChanceLegalMoves()
                            {

                                List<PlayerShips> ChancelegalStones = new List<PlayerShips>();

                                if (DiceTotal == 0)   // make sure we dont roll a 0
                                {
                                    return ChancelegalStones.ToArray();
                                }

        // Loop through all of a player's stones
        if (CurrentPlayerId == 0)
        {
            GameObject PLayer1_ship;

            ThePlayer1 = GameObject.FindObjectOfType<Player>();
            PLayer1_ship = GameObject.Find("PLAYER1");
            
        }



      //  PlayerShips1[] pss = GameObject.FindGameObjectsWithTag("Andrew1");

        PlayerShips[] pss = GameObject.FindObjectsOfType<PlayerShips>();

                                foreach (PlayerShips ps in pss)             //check on every ship found to see if it can move
                                {
                                    if (ps.PlayerId == CurrentPlayerId)
                                    {
                                         Debug.Log("ps.PlayerId " + ps.PlayerId);
               


                if (ps.CanLegallyMoveAhead(DiceTotal))  // check if ship can move based on dice total display
                                        {
                                            Debug.Log("ps to array" + ps);
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

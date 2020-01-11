using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ThePlayers = GameObject.FindObjectOfType<Player>();
       ThePlayerships = GameObject.FindObjectOfType<PlayerShips>();
        TheDiceRoller = GameObject.FindObjectOfType<DiceRoller>();
      //  theChanceCards = GameObject.FindObjectOfType<ChanceCards>();
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

        /*    string[] ChanceCards =
            {
                 "Roll Again",
                 "Skip Turn",
                 "Skip Turn",
                 "Not Used"
             };
             */

        
        Debug.Log("ChanceCards Start " + ChanceCards1.Length);
        

    }


    Player ThePlayers;                  // gain access to player scrip vars
    DiceRoller TheDiceRoller;           // gain access to diceroller scrip vars
   // ChanceCards theChanceCards;

    public int NumberOfPlayers = 4; //max number of players playing
    public int CurrentPlayerId = 0;  //set current playerid = 0 Ie - Player1
    public int DiceTotal;           // Total all dice rolls

    AIPlayer[] PlayerAIs;           // define the array for the players
  


    public bool IsDoneRolling = false;  // is done rolling false - have we finshed rollling
    public bool IsDoneClicking = false; //have we finished clicking
    public bool IsDoneAnimating = false; // Have we finshed moving

    public GameObject NoLegalMovesPopup;   //game object for test on screen
 

    PlayerShips ThePlayerships;         // gain access to the playerships vars



        //Code to allow for Camera Selection 
       public Camera MainCamera;            //define the main camera
  

    //define the player follow cameras
 public Camera ThePlayer1_camera;
 public   Camera ThePlayer2_camera;
    public Camera ThePlayer3_camera;
    public Camera ThePlayer4_camera;


    public string[] ChanceCards1 =
   {
        "Roll Again",
        "Skip Turn"
        

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
        IsDoneAnimating = false;


       
    // TODO move to next player
    CurrentPlayerId = (CurrentPlayerId + 1) % NumberOfPlayers;   // rotate through the players
        Camera_controls();      // Funtion to set the follow camera to follow playing player


    }

    // Update is called once per frame
    void Update()
    {          
        // is the tunrn done ?
        if (IsDoneRolling && IsDoneClicking && IsDoneAnimating)
        {
         //   Debug.Log ("Turn is done");
            NewTurn();
            return;
        }

        if (PlayerAIs[CurrentPlayerId] != null)   // Process to run the AI script for AI players
        {
          //  Debug.Log("CurrentPlayerId for AI" + CurrentPlayerId);
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


    //function to disable all the cmaera to make sure the right camera is active as necessary
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

    public void RollAgain()
    {

        Debug.Log("Roll Again");
        //start of a players turn
        IsDoneRolling = false;
        IsDoneClicking = false;
        IsDoneAnimating = false;
    }


    public void ChanceCard()
    {
    
        Debug.Log("cHANCEcARD");
        Debug.Log("ChanceCards Start function " + ChanceCards1.Length);
        string chanceCards = ChanceCards1[Random.Range(0, ChanceCards1.Length)];
        Debug.Log("ChanceCards.Length" + ChanceCards1.Length);
        Debug.Log("chanceCards selected" + chanceCards);
        if (chanceCards == "Roll Again")
        {
            Debug.Log("rolling again");
            RollAgain();
        }
        //Skip Turn
        if (chanceCards == "Skip Turn")
        {
            Debug.Log("Skipping next turn");
            
        }

    }
    //Function to set the right follow camera to the righ player
    public void Camera_controls()
    {
        switch (CurrentPlayerId)
        {
            case 0:
                Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();
                ThePlayer1_camera.enabled = true;
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
    
}

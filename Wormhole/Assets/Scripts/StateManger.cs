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
        //    theCameraController = GameObject.FindObjectOfType<CameraController>();

        // who is actually playing ?

        // 0 = human
        //1 = computer
        //2 = not playing

  /*   ThePlayerships.PlayerId = -1; // try set it to set player 1 as playerid 0 
      //  CurrentPlayerId = 0; 


        if (ThePlayers.player1_hum_comp == 0 || ThePlayers.player1_hum_comp == 1)
        {
            countOfPlayersActuallyPlaying++;
            //        ThePlayerships.PlayerId ++;
            
           


            Debug.Log(" player1");
            Debug.Log(" player1 - countOfPlayersActuallyPlaying " + countOfPlayersActuallyPlaying);
            Debug.Log(" player1 - ThePlayerships.PlayerId " + ThePlayerships.PlayerId);

        }

        if (ThePlayers.player2_hum_comp == 0 || ThePlayers.player2_hum_comp == 1)
        {
            countOfPlayersActuallyPlaying++;
            //         ThePlayerships.PlayerId++;
         //   ThePlayerships.PlayerId = 11;

            Debug.Log(" player1");
            Debug.Log(" player1 - countOfPlayersActuallyPlaying " + countOfPlayersActuallyPlaying);
            Debug.Log(" player1 - ThePlayerships.PlayerId " + ThePlayerships.PlayerId);

        }

        if (ThePlayers.player3_hum_comp == 0 || ThePlayers.player3_hum_comp == 1)
        {
            countOfPlayersActuallyPlaying++;
     //       ThePlayerships.PlayerId++;

            Debug.Log(" player1");
            Debug.Log(" player1 - countOfPlayersActuallyPlaying " + countOfPlayersActuallyPlaying);
            Debug.Log(" player1 - ThePlayerships.PlayerId " + ThePlayerships.PlayerId);
        }

        if (ThePlayers.player4_hum_comp == 0 || ThePlayers.player4_hum_comp == 1)
        {
            countOfPlayersActuallyPlaying++;
     //       ThePlayerships.PlayerId++;
            Debug.Log(" player1");
            Debug.Log(" player1 - countOfPlayersActuallyPlaying " + countOfPlayersActuallyPlaying);
            Debug.Log(" player1 - ThePlayerships.PlayerId " + ThePlayerships.PlayerId);
        }

        NumberOfPlayers = countOfPlayersActuallyPlaying;
        Debug.Log("NumberOfPlayers" + NumberOfPlayers);

       // CurrentPlayerId = 0;
       */
    }


    Player ThePlayers;

    public int NumberOfPlayers = 2; //max number of players playing
    public int CurrentPlayerId = 0;  //set current playerid = 0 Ie - Player1
    public int DiceTotal;           // Total all dice rolls

    public bool IsDoneRolling = false;  // is done rolling false - have we finshed rollling
    public bool IsDoneClicking = false; //have we finished clicking
    public bool IsDoneAnimating = false; // Have we finshed moving

    public GameObject NoLegalMovesPopup;   //game object for test on screen

   

  //  int countOfPlayersActuallyPlaying = 0;

    PlayerShips ThePlayerships;
  //  CameraController theCameraController;


    public void NewTurn()
    {

        

        //start of a players turn
        IsDoneRolling = false;
        IsDoneClicking = false;
        IsDoneAnimating = false;


     
        // TODO move to next player
        CurrentPlayerId = (CurrentPlayerId + 1) % NumberOfPlayers;   // episode 6 20:56   trys to make sure on the number of players

              


    }

    // Update is called once per frame
    void Update()
    {
        // is the tunrn done ?
        if (IsDoneRolling && IsDoneClicking && IsDoneAnimating)
        {
            Debug.Log ("Turn is done");
            NewTurn();
        }
    }

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


    IEnumerator NoLegalMovesCoroutine()
    {
        // display mesage
        NoLegalMovesPopup.SetActive(true);
        // wait one sec
        yield return new WaitForSeconds(1f);
        NoLegalMovesPopup.SetActive(false);
        NewTurn();
        

    }
}

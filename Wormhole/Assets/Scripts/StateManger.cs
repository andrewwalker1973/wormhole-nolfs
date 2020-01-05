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
        Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer2_camera = GameObject.Find("Player2_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer3_camera = GameObject.Find("Player3_Follow_Camera(Clone)").GetComponent<Camera>();
        Camera ThePlayer4_camera = GameObject.Find("Player4_Follow_Camera(Clone)").GetComponent<Camera>();

        // ThePlayer1_camera = Camera.FindObjectOfType<Camera>();

        //   Player1_follow_Camera = Player2_Follow_Camera(Clone)
        //    theCameraController = GameObject.FindObjectOfType<CameraController>();

        //   Player1_follow_Camera = GameObject.FindObjectOfType<Camera>();
        //    Player1_follow_Camera1 = Camera.f("Player1_follow_Camera (Clone)");

        //    Player1_follow_cam = GameObject.FindWithTag("Player1_follow_camera").GetComponent<Camera>();
        /*     disableAllCamera();
             Player1_follow_Camera.enabled = true;
             Player1_follow_Camera.depth = 1;
             Player1_follow_Camera_Object.SetActive(true);
             Debug.Log("set camera1");
     */
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
 //   Camera Player1_follow_cam;


  //  int countOfPlayersActuallyPlaying = 0;

    PlayerShips ThePlayerships;
  //  CameraController theCameraController;


        //Code to allow for Camera Selection 
       public Camera MainCamera;
  //  public Camera Player1_top_Camera;
//   public Camera Player1_follow_Camera;
//    Camera Player1_follow_Camera1;
//    public Camera Player2_top_Camera;
  //  public Camera Player2_follow_Camera;
 //   public Camera Player3_top_Camera;
 //   public Camera Player3_follow_Camera;
//    public Camera Player4_top_Camera;
//    public Camera Player4_follow_Camera;

 //   public GameObject MainCamera_Object;
 //   public GameObject Player1_top_Camera_Object;
 //  public GameObject Player1_follow_Camera_Object;
 //   public GameObject Player2_top_Camera_Object;
  //public GameObject Player2_follow_Camera_Object;
    //   public GameObject Player3_top_Camera_Object;
    //   public GameObject Player3_follow_Camera_Object;
    //   public GameObject Player4_top_Camera_Object;
    //   public GameObject Player4_follow_Camera_Object;


 public Camera ThePlayer1_camera;
 public   Camera ThePlayer2_camera;
    public Camera ThePlayer3_camera;
    public Camera ThePlayer4_camera;


    public void NewTurn()
    {

        

        //start of a players turn
        IsDoneRolling = false;
        IsDoneClicking = false;
        IsDoneAnimating = false;


       
    // TODO move to next player
    CurrentPlayerId = (CurrentPlayerId + 1) % NumberOfPlayers;   // episode 6 20:56   trys to make sure on the number of players
        Camera_controls();


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
      //  Debug.Log("Disabled all Cameras");

    }
    public void Camera_controls()
    {
        switch (CurrentPlayerId)
        {
            case 0:
                Camera ThePlayer1_camera = GameObject.Find("Player1_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();
                ThePlayer1_camera.enabled = true;
           //   Player1_follow_Camera_Object.SetActive(true);
             // Player1_follow_Camera.depth = 1;
             //   Debug.Log("set camera1");
                

                break;
            case 1:
                Camera ThePlayer2_camera = GameObject.Find("Player2_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();
                ThePlayer2_camera.enabled = true;
            // Player2_follow_Camera_Object.SetActive(true);
             // Player2_follow_Camera.depth = 1;
             //   Debug.Log("set camera2");
                break;
            case 2:
                Camera ThePlayer3_camera = GameObject.Find("Player3_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();
                ThePlayer3_camera.enabled = true;
     //           Player3_follow_Camera_Object.SetActive(true);
             //   Debug.Log("set camera3");

                break;
            case 3:
                Camera ThePlayer4_camera = GameObject.Find("Player4_Follow_Camera(Clone)").GetComponent<Camera>();
                disableAllCamera();
                ThePlayer4_camera.enabled = true;
                //            Player4_follow_Camera_Object.SetActive(true);
                //           Debug.Log("set camera4");
                break;
        }
    }
    
}

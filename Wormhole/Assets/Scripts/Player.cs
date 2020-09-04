using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
        
    // set a variable to cater for ship number and player number 
    public  float player1_hum_comp;
    float player1_ship;
    bool player1_onboard;
  


    public float player2_hum_comp;
    float player2_ship;
    bool player2_onboard;
   


    public float player3_hum_comp;
    float player3_ship;
    bool player3_onboard;
    

    public float player4_hum_comp;
    float player4_ship;
    bool player4_onboard;
    


    //define spawn points for player ships
    public Transform Player1_SpawnPoint;
    public Transform Player2_SpawnPoint;
    public Transform Player3_SpawnPoint;
    public Transform Player4_SpawnPoint;

    //define game objects to refer to player ships
    public GameObject Ship1;
    public GameObject Ship2;
    public GameObject Ship3;
    public GameObject Ship4;

    GameObject player1_ships;
    GameObject player2_ships;
    GameObject player3_ships;
    GameObject player4_ships;

  
    public GameObject Player1_Follow_Camera;
    public GameObject Player2_Follow_Camera;
    public GameObject Player3_Follow_Camera;
    public GameObject Player4_Follow_Camera;

    public Camera Camera_Player1_follow;
    public Camera Camera_Player2_follow;
    public Camera Camera_Player3_follow;
    public Camera Camera_Player4_follow;

    //CompleteCameraController TheCompleteCameraController;

    GameObject player1_ships_camera;
    GameObject player2_ships_camera;
    GameObject player3_ships_camera;
    GameObject player4_ships_camera;


    void Start()
    {

        // code to pull the data from player prefs
     

        player1_hum_comp = PlayerPrefs.GetFloat("player1_playing_input");
        player1_ship = PlayerPrefs.GetFloat("player1_ship");

        player2_hum_comp = PlayerPrefs.GetFloat("player2_playing_input");
        player2_ship = PlayerPrefs.GetFloat("player2_ship");

        player3_hum_comp = PlayerPrefs.GetFloat("player3_playing_input");
        player3_ship = PlayerPrefs.GetFloat("player3_ship");

        player4_hum_comp = PlayerPrefs.GetFloat("player4_playing_input");
        player4_ship = PlayerPrefs.GetFloat("player4_ship");


       

        if (this.gameObject.name == "Player1" && player1_onboard == false)
            {
           
                    switch (player1_hum_comp)
                    {
                        case 0:
                           
                            check_player1_ship();
                            player1_onboard = true;
                            break;
                        case 1:
                            
                            check_player1_ship();
                            player1_onboard = true;
                            break;
                        case 2:

                    // add in code not to show a ship
                    check_player1_ship();
                    player1_onboard = true;
                    
                            break;
                    }
         
                player1_onboard = true;
            player1_ships_camera.SetActive(true);
        }

        if (this.gameObject.name == "Player2" && player2_onboard == false)
        {
          
            switch (player2_hum_comp)
            {
                case 0:
                   
                    check_player2_ship();
                    player2_onboard = true;
                    break;
                case 1:
                    
                    check_player2_ship();
                    player2_onboard = true;
                    break;
                case 2:

                    // add in code not to show a ship
                    check_player2_ship();
                    player2_onboard = true;
                  
                    break;
            }
            //check_player1_ship();
            player2_onboard = true;
        }


        if (this.gameObject.name == "Player3" && player3_onboard == false)
        {
            
            switch (player3_hum_comp)
            {
                case 0:
                    
                    check_player3_ship();
                    player3_onboard = true;
                    break;
                case 1:
                    
                    check_player3_ship();
                    player3_onboard = true;
                    break;
                case 2:

                    // add in code not to show a ship
                    check_player3_ship();
                    player3_onboard = true;
                   
                    break;
            }
            
            player3_onboard = true;
        }

        if (this.gameObject.name == "Player4" && player4_onboard == false)
        {
            
            switch (player4_hum_comp)
            {
                case 0:
                    
                    check_player4_ship();
                    player4_onboard = true;
                    break;
                case 1:
                    
                    check_player4_ship();
                    player4_onboard = true;
                    break;
                case 2:

                    // add in code not to show a ship
                    check_player4_ship();
                    player4_onboard = true;
                  
                    break;
            }
            
            player4_onboard = true;
        }
    }

        // Update is called once per frame
        void Update()
        {

        }

        void check_player1_ship()
        {

            // Code to instantiate player 1 ship type
            switch (player1_ship)
            {
                case 0:
             player1_ships = Instantiate(Ship1, Player1_SpawnPoint.transform.position, Player1_SpawnPoint.transform.rotation);
              
                player1_ships.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                
                player1_ships.transform.position = new Vector3(-5, 5, -167);
                player1_ships.SetActive(true);
                player1_ships.tag = "Game_player_1";
                    player1_ships.name = "PLAYER1";

                player1_ships_camera = Instantiate(Player1_Follow_Camera, Player1_SpawnPoint.transform.position, Player1_SpawnPoint.transform.rotation);
               player1_ships_camera.SetActive(false);
                player1_ships_camera.transform.parent = player1_ships.transform;






                break;
                case 1:
                    player1_ships = Instantiate(Ship2, Player1_SpawnPoint.transform.position, Player1_SpawnPoint.transform.rotation);
                    player1_ships.transform.localScale = new Vector3(0.2f, 0.4f, 0.2f);
                     player1_ships.transform.position = new Vector3(23, 15, -130);
                    player1_ships.SetActive(true);
                    player1_ships.name = "PLAYER1";
                    break;
                case 2:
                    player1_ships =  Instantiate(Ship3, Player1_SpawnPoint.transform.position, Player1_SpawnPoint.transform.rotation);
                    player1_ships.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    player1_ships.SetActive(true);
                    player1_ships.name = "PLAYER1";
                    break;
                case 3:
                player1_ships = Instantiate(Ship4, Player1_SpawnPoint.transform.position, Player1_SpawnPoint.transform.rotation);
                player1_ships.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                player1_ships.SetActive(true);
                player1_ships.name = "PLAYER1";
                break;
            }
        }
    


   void check_player2_ship()
    {

        // Code to instantiate player 2 ship type
        switch (player2_ship)
        {
            case 0:
                player2_ships = Instantiate(Ship1, Player2_SpawnPoint.transform.position, Player2_SpawnPoint.transform.rotation);
                player2_ships.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                player1_ships.transform.position = new Vector3(-5, 15, -167);
                player2_ships.SetActive(true);
                player2_ships.name = "PLAYER2";
                player2_ships.tag = "Game_player_2";
               
                player2_ships_camera = Instantiate(Player2_Follow_Camera, Player2_SpawnPoint.transform.position, Player2_SpawnPoint.transform.rotation);
                player2_ships_camera.SetActive(true);
                break;
            case 1:
                player2_ships = Instantiate(Ship2, Player2_SpawnPoint.transform.position, Player2_SpawnPoint.transform.rotation);
                player2_ships.transform.localScale = new Vector3(0.2f, 0.4f, 0.2f);
                player2_ships.transform.position = new Vector3(-5, 2, -171);
               player2_ships.SetActive(true);
                                player2_ships.name = "PLAYER2";
                player2_ships.tag = "Game_player_2";
                Camera_Player2_follow.enabled = false;

                player2_ships_camera = Instantiate(Player2_Follow_Camera, Player2_SpawnPoint.transform.position, Player2_SpawnPoint.transform.rotation);
                player2_ships_camera.SetActive(true);
                player2_ships_camera.transform.parent = player2_ships.transform;
                break;
            case 2:
                player2_ships = Instantiate(Ship3, Player2_SpawnPoint.transform.position, Player2_SpawnPoint.transform.rotation);
                player2_ships.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                player2_ships.SetActive(true);
                player2_ships.name = "PLAYER2";
                break;
            case 3:
                player2_ships = Instantiate(Ship4, Player2_SpawnPoint.transform.position, Player2_SpawnPoint.transform.rotation);
                player2_ships.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                player2_ships.SetActive(true);
                player2_ships.name = "PLAYER2";
                break;
        }
    }


    void check_player3_ship()
    {

        // Code to instantiate player 3 ship type
        switch (player3_ship)
        {
            case 0:
                player3_ships = Instantiate(Ship1, Player3_SpawnPoint.transform.position, Player3_SpawnPoint.transform.rotation);
                player3_ships.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                player1_ships.transform.position = new Vector3(-5, 15, -134);
                player3_ships.SetActive(true);
                player3_ships.name = "PLAYER3";
                break;
            case 1:
                player3_ships = Instantiate(Ship2, Player3_SpawnPoint.transform.position, Player3_SpawnPoint.transform.rotation);
                player3_ships.transform.localScale = new Vector3(0.2f, 0.4f, 0.2f);
                player3_ships.transform.position = new Vector3(23, 15, -130);
                player3_ships.SetActive(true);
                player3_ships.name = "PLAYER3";
                break;
            case 2:
                player3_ships = Instantiate(Ship3, Player3_SpawnPoint.transform.position, Player3_SpawnPoint.transform.rotation);
                player3_ships.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                player3_ships.transform.position = new Vector3(0.5f, 1, -160);
                player3_ships.SetActive(true);
                player3_ships.name = "PLAYER3";

                Camera_Player3_follow.enabled = false;

                player3_ships_camera = Instantiate(Player3_Follow_Camera, Player3_SpawnPoint.transform.position, Player3_SpawnPoint.transform.rotation);
                player3_ships_camera.SetActive(true);
                player3_ships_camera.transform.parent = player3_ships.transform;
                break;
            case 3:
                player3_ships = Instantiate(Ship4, Player3_SpawnPoint.transform.position, Player3_SpawnPoint.transform.rotation);
                player3_ships.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                player3_ships.SetActive(true);
                player3_ships.name = "PLAYER3";
                break;
        }
    }

    void check_player4_ship()
    {

        // Code to instantiate player 4 ship type
        switch (player4_ship)
        {
            case 0:
                player4_ships = Instantiate(Ship1, Player4_SpawnPoint.transform.position, Player4_SpawnPoint.transform.rotation);
                player4_ships.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                player1_ships.transform.position = new Vector3(-5, 15, -100);
                player4_ships.SetActive(true);
                player4_ships.name = "PLAYER4";
                break;
            case 1:
                player4_ships = Instantiate(Ship2, Player4_SpawnPoint.transform.position, Player4_SpawnPoint.transform.rotation);
                player4_ships.transform.localScale = new Vector3(0.2f, 0.4f, 0.2f);
                player4_ships.transform.position = new Vector3(23, 15, -130);
                player4_ships.SetActive(true);
                player4_ships.name = "PLAYER4";
                break;
            case 2:
                player4_ships = Instantiate(Ship3, Player4_SpawnPoint.transform.position, Player4_SpawnPoint.transform.rotation);
                player4_ships.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                player4_ships.SetActive(true);
                player4_ships.name = "PLAYER4";
                break;
            case 3:
                player4_ships = Instantiate(Ship4, Player4_SpawnPoint.transform.position, Player4_SpawnPoint.transform.rotation);
                player4_ships.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                player4_ships.transform.position = new Vector3(-4,4, -161);
                player4_ships.SetActive(true);
                player4_ships.name = "PLAYER4";

                Camera_Player4_follow.enabled = false;

                player4_ships_camera = Instantiate(Player4_Follow_Camera, Player4_SpawnPoint.transform.position, Player4_SpawnPoint.transform.rotation);
                player4_ships_camera.SetActive(true);
                player4_ships_camera.transform.parent = player4_ships.transform;
                break;
        }
    }
}






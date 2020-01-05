using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

   

    // Start is called before the first frame update
    void Start()
    {
       
        // ThePlayerships = GameObject.FindObjectOfType<PlayerShips>();
    }

    public Tile[] NextTiles;
    public PlayerShips PlayerShips;
    public bool IsScoringSpace;
    public bool IsrightTurnSpace;
  //  public bool TurnRight = false;
  //  PlayerShips ThePlayerships;


    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Turn right  Tile Script ?" + IsrightTurnSpace);
        //Try tunr the charater
    //    Debug.Log("IsrightTurnSpace  " + IsrightTurnSpace);
   //     if (IsrightTurnSpace == true)
   //     {
   //         TurnRight = true;
   //         Debug.Log("Turning right");
   //     }
        // --------------------

    }
}



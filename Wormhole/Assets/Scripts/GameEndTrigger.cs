
using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    

   public  PlayerShips ThePlayerships1;
    public PlayerShips ThePlayerships2;
    public PlayerShips ThePlayerships3;
    public PlayerShips ThePlayerships4;


  

        private void OnTriggerEnter(Collider theCollision)
    {

     
        if (theCollision.gameObject.tag == "Game_player_1")
        // By using {}, the condition apply to that entire scope, instead of the next line.
        {
           
            ThePlayerships1.EndGame();
        }

        if (theCollision.gameObject.tag == "Game_player_2")
        // By using {}, the condition apply to that entire scope, instead of the next line.
        {
            
            ThePlayerships2.EndGame();
        }

        if (theCollision.gameObject.tag == "Game_player_3")
        // By using {}, the condition apply to that entire scope, instead of the next line.
        {
          
            ThePlayerships3.EndGame();
        }

        if (theCollision.gameObject.tag == "Game_player_4")
        // By using {}, the condition apply to that entire scope, instead of the next line.
        {
            
            ThePlayerships4.EndGame();
        }
       
    }
}

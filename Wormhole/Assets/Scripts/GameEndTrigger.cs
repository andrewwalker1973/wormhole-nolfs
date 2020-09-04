
using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    

   public  PlayerShips ThePlayerships1;
    public PlayerShips ThePlayerships2;
    public PlayerShips ThePlayerships3;
    public PlayerShips ThePlayerships4;

    private AudioSource source;
   public AudioClip Wins;
    public AudioClip Player1_announce;
    public AudioClip Player2_announce;
    public AudioClip Player3_announce;
    public AudioClip Player4_announce;

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider theCollision)
    {
        bool SomebodyWon1 = StateManger.SomebodyWon;
        if (theCollision.gameObject.tag == "Game_player_1" && SomebodyWon1 == false)
        {
            
            source.PlayOneShot(Player1_announce);
          
        }


        if (theCollision.gameObject.tag == "Game_player_2" && SomebodyWon1 == false)
        {
            
            source.PlayOneShot(Player2_announce);
         
        }


        if (theCollision.gameObject.tag == "Game_player_3" && SomebodyWon1 == false)
        {
            
            source.PlayOneShot(Player3_announce);
            
        }

        if (theCollision.gameObject.tag == "Game_player_4" && SomebodyWon1 == false)
        {
            
            source.PlayOneShot(Player4_announce);
            
        }






        if (theCollision.gameObject.tag == "Game_player_1" && SomebodyWon1 == false)
        {
            
            source.PlayOneShot(Wins);
        }


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

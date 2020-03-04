
using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    // public PlayerShips ThePlayerships;         // gain access to the playerships vars

   public  PlayerShips ThePlayerships1;
    public PlayerShips ThePlayerships2;
    public PlayerShips ThePlayerships3;
    public PlayerShips ThePlayerships4;


   // public ParticleSystem Fireworks1;

    void Start()
    {

        // ThePlayerships = GameObject.FindObjectOfType<PlayerShips>();
       // Fireworks1 = GetComponent<ParticleSystem>();
    }

        private void OnTriggerEnter(Collider theCollision)
    {

       // Fireworks1.Play();
        if (theCollision.gameObject.tag == "Game_player_1")
        // By using {}, the condition apply to that entire scope, instead of the next line.
        {
            Debug.Log("Player1 collisin");
            ThePlayerships1.EndGame();
        }

        if (theCollision.gameObject.tag == "Game_player_2")
        // By using {}, the condition apply to that entire scope, instead of the next line.
        {
            Debug.Log("Player2 collisin");
            ThePlayerships2.EndGame();
        }

        if (theCollision.gameObject.tag == "Game_player_3")
        // By using {}, the condition apply to that entire scope, instead of the next line.
        {
            Debug.Log("Player3 collisin");
            ThePlayerships3.EndGame();
        }

        if (theCollision.gameObject.tag == "Game_player_4")
        // By using {}, the condition apply to that entire scope, instead of the next line.
        {
            Debug.Log("Player4 collisin");
            ThePlayerships4.EndGame();
        }
       // ThePlayerships.EndGame();
    }
}

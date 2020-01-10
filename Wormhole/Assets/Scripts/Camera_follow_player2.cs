using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow_player2 : MonoBehaviour
{
    Transform target2;
    GameObject PLayer2_ship;
    public float smoothspeed = 4f;
    public Vector3 offset;

    Player ThePlayer;

    void Start()
    {
        Player ThePlayer2;
        ThePlayer2 = GameObject.FindObjectOfType<Player>();
        PLayer2_ship = GameObject.Find("PLAYER2");
               target2 = PLayer2_ship.transform;
      

    }

    private void Update()
    {
        transform.LookAt(target2, Vector3.up);
    }


    void LateUpdate()
    {
        
                Vector3 desiredposition = target2.position + offset;
             Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed);
        transform.position = smoothedposition;
       

    }
}

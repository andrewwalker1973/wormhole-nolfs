using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow_player3 : MonoBehaviour
{
    Transform target3;
    GameObject PLayer3_ship;
    public float smoothspeed = 4f;
    public Vector3 offset;

    Player ThePlayer;

    void Start()
    {
        Player ThePlayer3;
        ThePlayer3 = GameObject.FindObjectOfType<Player>();
        PLayer3_ship = GameObject.Find("PLAYER3");
        target3 = PLayer3_ship.transform;


    }

    private void Update()
    {
        transform.LookAt(target3, Vector3.up);
    }

    void LateUpdate()
    {

        Vector3 desiredposition = target3.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed);
        transform.position = smoothedposition;


    }
}

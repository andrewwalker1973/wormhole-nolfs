using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow_player4 : MonoBehaviour
{
    Transform target4;
    GameObject PLayer4_ship;
    public float smoothspeed = 0.125f;
    public Vector3 offset;

    Player ThePlayer;

    void Start()
    {
        Player ThePlayer4;
        ThePlayer4 = GameObject.FindObjectOfType<Player>();
        PLayer4_ship = GameObject.Find("PLAYER4");
        target4 = PLayer4_ship.transform;


    }

    private void Update()
    {
        transform.LookAt(target4, Vector3.up);
    }

    void LateUpdate()
    {

        Vector3 desiredposition = target4.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed);
        transform.position = smoothedposition;


    }
}

using UnityEngine;

public class Camera_follow_player1 : MonoBehaviour
{
    Transform target1;
    GameObject PLayer1_ship;
    public float smoothspeed = 4f;
    public Vector3 offset;

    Player ThePlayer;

    void Start()
    {
        Player ThePlayer1;
        ThePlayer1 = GameObject.FindObjectOfType<Player>();
        PLayer1_ship = GameObject.Find("PLAYER1");
        target1 = PLayer1_ship.transform;
       

    }

    private void Update()
    {
        transform.LookAt(target1,Vector3.up);
    }

    void LateUpdate()
    {
        Vector3 desiredposition = target1.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed);
        transform.position = smoothedposition;
      
        

    }
}

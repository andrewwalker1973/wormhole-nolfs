using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodRotate : MonoBehaviour
{
    public float Xspeed = 200f; //you can change this value in the inspector
    public float Yspeed = 200f;
    public float Zspeed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime * Xspeed, Time.deltaTime * Yspeed, Time.deltaTime * Zspeed);
    }
}

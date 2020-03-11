using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotate2 : MonoBehaviour
{
    public float speed = 200f; //you can change this value in the inspector
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * speed);
    }
}

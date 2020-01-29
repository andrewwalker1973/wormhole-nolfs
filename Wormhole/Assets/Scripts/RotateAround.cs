using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{


    public float speed = 7.0f; //you can change this value in the inspector

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
   
        void Update()
        {
            transform.Rotate(Time.deltaTime * speed, 0, 0);
        }
}


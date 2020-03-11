using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSRotate : MonoBehaviour
{

    public float XRotateSpeed;
    public float YRotateSpeed;
    public float ZRotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(XRotateSpeed, YRotateSpeed, ZRotateSpeed);
    }
}

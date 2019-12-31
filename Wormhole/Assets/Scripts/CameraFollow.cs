using UnityEngine;
using System.Collections;


public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    public float smoothSpeed = 0.125f;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;


    private void LateUpdate()
    {
        Refresh();
                     
    }


    public void Refresh ()
    {
        if (Target == null )
        {
            return;
        }

        // computer positions
        if ( offsetPositionSpace == Space.Self)
        {
            transform.position = Target.TransformPoint(offsetPosition);

        }
        else
        {
            transform.position = Target.position + offsetPosition;
        }

        //Compute Rotation
        if (lookAt)
        {
            transform.LookAt(Target);

        }
        else
        {
            transform.rotation = Target.rotation;
        }

    }
}

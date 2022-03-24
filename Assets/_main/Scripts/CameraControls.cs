using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform TargetObject;

    public enum CameraMoveMode { stay, follow, lerp };
    public enum CameraRotateMode { none, withObject, independent };

    [Header("Camera Controls")]
    public CameraMoveMode MoveMode = CameraMoveMode.follow;
    public CameraRotateMode RotateMode = CameraRotateMode.withObject;
    private Vector3 target_Offset;

    public float lerpT = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        target_Offset = transform.position - TargetObject.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TargetObject == null) return;

        switch (MoveMode)
        {
            case CameraMoveMode.stay:
                break;
            case CameraMoveMode.follow:
                CameraFollow();
                break;
            case CameraMoveMode.lerp:
                CameraLerp();
                break;
        }

        switch(RotateMode)
        {
            case CameraRotateMode.none:
                break;
            case CameraRotateMode.withObject:
                RotateWithObject();
                break;
            case CameraRotateMode.independent:
                RotateIndependent();
                break;
        }
    }

    //Lerp the camera towards the target object
    void CameraLerp()
    {
        transform.position = Vector3.Lerp(transform.position, TargetObject.position + target_Offset, lerpT);
    }

    //Stay on the target object's position
    void CameraFollow()
    {
        transform.position = TargetObject.position;
    }

    //use the target's forward vector to dictate rotation
    void RotateWithObject()
    {
        transform.forward = TargetObject.transform.forward;
    }

    //use mouse controls to rotate camera
    void RotateIndependent()
    {
        Debug.LogWarning("Rotate Independent is not yet implemented");      //TODO
    }
}

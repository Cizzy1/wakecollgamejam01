using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    public Vector3 Left;
    //float Lock = 0f;
    private float smoothspeed = 0.04f;

    void LateUpdate()
    {
        Vector3 desiredPos = Player.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothspeed);
        transform.position = smoothedPos;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 distance;
    public float camSpeed;
    private void FixedUpdate()
    {
        Vector3 positionToGo = player.position + distance;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, positionToGo, camSpeed);
        transform.position = smoothPosition;
    }
}

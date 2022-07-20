using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject _object;
    public Vector3 distance;
    public float camSpeed;
    private void FixedUpdate()
    {
        Vector3 positionToGo = _object.transform.position + distance;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, positionToGo, camSpeed);
        transform.position = smoothPosition;
    }
}

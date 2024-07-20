using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    private float initialz;

    void Start()
    {
        initialz = transform.position.z;
    }
    
    void FixedUpdate()
    {
        Vector3 desiredPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, initialz);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPosition;
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}

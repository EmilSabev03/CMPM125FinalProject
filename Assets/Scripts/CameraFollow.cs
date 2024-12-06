//This script is attached to the main camera and is used to follow the player's car. 
//There are offset values for x, y, and z to change the rotation and angle.
//The variables translateSpeed and deltaTime are used to make the camera rotation not locked.

//This script was created with the help of this youtube video: https://www.youtube.com/watch?v=Z4HA8zJhGEk

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
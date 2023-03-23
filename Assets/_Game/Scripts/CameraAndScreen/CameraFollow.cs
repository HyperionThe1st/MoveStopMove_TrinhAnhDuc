using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 camera_offset;
    //[SerializeField] private Vector3 camera_rotation;
    //[SerializeField] private float camera_speed;

    private void Start()
    {
    }

    private void LateUpdate()
    {
        Camera_Follow();
    }

    private void Camera_Follow()
    {
        transform.position = target.transform.position + camera_offset;
    }
}

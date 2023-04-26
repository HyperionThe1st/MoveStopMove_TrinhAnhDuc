using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variable;

public class CameraFollow : MonoBehaviour
{
    [System.Serializable]
    public struct Camera
    {
        public Vector3 offsetStruct;
        public Vector3 eulerAngleStruct;
    }

    //Component:
    [SerializeField] private Transform target;
    [SerializeField] private PlayerMovement player;
    //[SerializeField] private Vector3 camera_offset;
    private float lerpTime = 10f;


    //Data:
    [SerializeField] private Camera mainMenu;
    [SerializeField] private Camera shop;
    [SerializeField] private Camera play;

    //Characteristics: 
    private CameraState c_state;
    private Vector3 targetOffset;
    private Vector3 targetEulerAngle;
    private Vector3 cameraOffset;
    private Vector3 cameraEulerAngle;

    private void Start()
    {
        ChangeStateCamera(CameraState.MainMenu);
        cameraOffset = targetOffset;
        cameraEulerAngle = targetEulerAngle;
    }

    private void LateUpdate()
    {
        Camera_Follow();
    }

    private void Camera_Follow()
    {
        if (c_state == CameraState.Play && targetOffset != play.offsetStruct * player.ScaleSize())
        {
            targetOffset = play.offsetStruct * player.ScaleSize();
        }
        cameraOffset = Vector3.Lerp(cameraOffset, targetOffset, lerpTime * Time.deltaTime);
        transform.position = player.transform.position + cameraOffset;
        cameraEulerAngle = Vector3.Lerp(cameraEulerAngle, targetEulerAngle, lerpTime * Time.deltaTime);
        transform.rotation = Quaternion.Euler(cameraEulerAngle);
    }

    public void ChangeStateCamera(CameraState state)
    {
        this.c_state = state;
        switch (this.c_state)
        {
            case CameraState.MainMenu:
                targetOffset = mainMenu.offsetStruct;
                targetEulerAngle = mainMenu.eulerAngleStruct;
                break;
            case CameraState.Play:
                targetOffset = play.offsetStruct;
                targetEulerAngle = play.eulerAngleStruct;
                break;
            case CameraState.Shop:
                targetOffset = shop.offsetStruct;
                targetEulerAngle = shop.eulerAngleStruct;
                break;
        }

    }

}

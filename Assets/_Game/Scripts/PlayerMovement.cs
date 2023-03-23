using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
    //Component:
    //[SerializeField] private CharacterController _charController;

    [SerializeField] private Rigidbody _rb;



    [SerializeField] private Joystick _mngrJoystick;
    [SerializeField] private GameObject tempPlayer;

    //Transform:
    private Transform meshPlayer;


    //Movement:
    private float inputX;
    private float inputZ;
    private Vector3 v_movement;
    private Vector2 PlayerMovementInput;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravity;

    // Start is called before the first frame update
    void Start()
    {

        //_charController = tempPlayer.GetComponent<CharacterController>();
        meshPlayer = tempPlayer.transform.GetChild(0);
        _mngrJoystick = GameObject.Find(Variable.IMGJOYSTICKBACKGROUND).GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = _mngrJoystick.InputHorizontal();
        inputZ = _mngrJoystick.InputVertical();
        PlayerMovementInput = new Vector2(inputX, inputZ);
        if (inputX == 0 && inputZ == 0)
        {
            changeAnim("idle");
        }
        else
        {
            changeAnim("run");
        }


    }

    private void FixedUpdate()
    {
        //gravity:
        float tempY;

        ////if (_charController.isGrounded)
        ////{
        ////    v_movement.y = 0f;
        ////}
        ////else
        ////{
        ////    v_movement.y -= gravity * Time.fixedDeltaTime;
        ////}
        tempY = v_movement.y;
        //movement:



        if (movementSpeed > 0)
        {
            v_movement = new Vector3(inputX * movementSpeed, tempY, inputZ * movementSpeed);
        }
        while (movementSpeed == 0)
        {
            if (inputZ > 0)
            {
                Stop();
                v_movement = new Vector3(inputX * movementSpeed, tempY, inputZ * movementSpeed);
                break;
            }
            else
            {
                NotStop();
                v_movement = new Vector3(inputX * movementSpeed, tempY, inputZ * movementSpeed);
                break;
            }
        }

        //_charController.Move(v_movement);

        Vector3 moveVector = transform.TransformDirection(v_movement) * movementSpeed;
        _rb.velocity = new Vector3(moveVector.x,_rb.velocity.y,moveVector.z);


        //mesh rotate:
        if (inputX != 0 || inputZ != 0)
        {
            Vector3 lookDir = new Vector3(v_movement.x, 0, v_movement.z);
            meshPlayer.rotation = Quaternion.LookRotation(lookDir);
            //transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    public void Stop()
    {
        movementSpeed = 0;
    }
    public void NotStop()
    {
        //movementSpeed = Variable.MOVEMENTSPEED;
    }
}

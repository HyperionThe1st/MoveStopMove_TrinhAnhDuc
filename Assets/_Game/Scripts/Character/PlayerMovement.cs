using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
    //Component:
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Joystick _mngrJoystick;
    [SerializeField] private GameObject tempPlayer;
    //Transform:
    
    [SerializeField] private LayerMask groundLayer;

    //Movement:
    private float inputX;
    private float inputZ;
    private Vector3 v_movement;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravity;

    //Attack:
    private int timer;

    // Start is called before the first frame update
    void Start()
    {
        //meshPlayer = tempPlayer.transform.GetChild(0);
        _mngrJoystick = GameObject.Find(Variable.IMGJOYSTICKBACKGROUND).GetComponent<Joystick>();


        listTargets = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckExist();
        inputX = _mngrJoystick.InputHorizontal();
        inputZ = _mngrJoystick.InputVertical();
        if (inputX == 0 && inputZ == 0 && listTargets.Count == 0)
        {
            ChangeAnim(Variable.IDLE);
            SetRunning(false);

        }
        else if (inputX != 0 && inputZ != 0)
        {
            ChangeAnim(Variable.RUN);
            SetRunning(true);
            SetTimer(1);
            
        }
        else if (inputX == 0 && inputZ == 0 && listTargets.Count != 0)
        {
            if (timer == 1)
            {
                SetRunning(false);
                RotateToEnemy();
                ChangeAnim(Variable.ATTACK);
                Attack();
                timer = 0;
                ChangeAnim(Variable.IDLE);
            }      
        }

    }

    private void FixedUpdate()
    {
        //gravity:
        float tempY;
        if (isGround())
        {
            v_movement.y = 0f;
        }
        else
        {
            v_movement.y -= gravity * Time.fixedDeltaTime;
        }
        tempY = v_movement.y;

        //movement:
        //v_movement = new Vector3(inputX, tempY, inputZ);
        //Vector3 moveVector = transform.TransformDirection(v_movement) * movementSpeed;
        //_rb.velocity = new Vector3(moveVector.x, _rb.velocity.y, moveVector.z);


        //C2: 
        v_movement = new Vector3(inputX * movementSpeed, tempY, inputZ * movementSpeed);
        _rb.velocity = new Vector3(v_movement.x, _rb.velocity.y, v_movement.z);

        //mesh rotate:
        if (inputX != 0 || inputZ != 0)
        {
            Vector3 lookDir = new Vector3(v_movement.x, 0, v_movement.z);
            meshPlayer.rotation = Quaternion.LookRotation(lookDir);
            //transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
    
    public bool isGround()
    {
        bool hit = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
        return hit;
    }

    
    public void SetTimer(int a)
    {
        timer = a;
    }
    public void ResetTimer()
    {
        timer = 0;
    }
}

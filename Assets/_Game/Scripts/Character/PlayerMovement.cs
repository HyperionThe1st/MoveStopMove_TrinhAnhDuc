using System;
using System.Collections;
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
        listTargets = new List<GameUnit>();
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

        //move
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
    public override void OnInit()
    {
        base.OnInit();
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
    public override void SpawnWeapon()
    {
        base.SpawnWeapon();
    }

    public override void Attack()
    {
        GameUnit closestEnemy = GetClosetObject();
        if (closestEnemy != null)
        {
            RotateToEnemy();
            ChangeAnim(Variable.ATTACK);
            ShowUnderline(closestEnemy);
        }
        StartCoroutine(DoSpawnWeapon());
        
    }

    IEnumerator DoSpawnWeapon()
    {
        float timerate = 0.4f;
        float _time_2 = 0;

        while (_time_2 < timerate)
        {
            _time_2 += Time.deltaTime;
            yield return null;
            if (Input.GetMouseButton(0))
            {
                goto Lable;
            }
        }
        SpawnWeapon();
    Lable:
        yield return null;
    }
    public override void OnHit()
    {
        base.OnHit();
    }

}

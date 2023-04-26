using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable : MonoBehaviour
{
    //Object:
    public const string IMGJOYSTICKBACKGROUND = "imgJoystickBg";
    public const string TARGET = "Target";
    public const float WEAPONDEFAULTSPEED = 5.0f;
    public const float TARGETUNDERLINEROTATESPEED = 0.5f;
    public const string BLOCK = "Block";

    //Anim:
    public const string IDLE = "idle";
    public const string RUN = "run";
    public const string DIE = "die";
    public const string DANCE = "dance";
    public const string ATTACK = "attack";
    public const string WIN = "win";

    //Time:
    public const float WEAPONLIFETIME = 2.5f;
    public const float ATTACKDELAY = 1.0f;

    //enum:
    public enum PoolType
    {
        Bot,

        Knife,
        Axe,
        Bomerang
    }

    public enum CameraState
    {
        MainMenu = 0,
        Play = 1,
        Shop = 2,
    }

    //DistanceAndAmount:
    public const float SPAWNDISTANCE = 7.0f;
    public const int MAXBOTONSCREEN = 9;
    public const int MAXBOT = 50;


    //Skin:
    public const string SKIN_ID = "skin_id";
    public const string COST = "cost";
}

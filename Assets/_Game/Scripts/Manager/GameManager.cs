using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CameraFollow camf;

    protected void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }
    private void Start()
    {
        
    }

    public void StandBy()
    {
      //  UIManager.Ins.OpenUI<>();
    }
}

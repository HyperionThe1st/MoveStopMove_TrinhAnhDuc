using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tar : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Rotate(0, 0, Variable.TARGETUNDERLINEROTATESPEED);
        }

    }




}

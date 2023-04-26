using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomerang : Weapon
{
    private Vector3 target;
    public Transform pivotPoint;
    public float rotationSpeed = 700f;

    public enum State { Forward, Backward, Stop }

    private State state;

    private void Start()
    {
        pivotPoint = transform;
    }

    private void Update()
    {
        
    }
}
